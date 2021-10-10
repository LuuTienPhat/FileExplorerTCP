using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using MySharedClass;

namespace Cilent
{
    public partial class Client2 : DevExpress.XtraEditors.XtraForm
    {
        private static String host;
        private static int port;
        private static TcpClient client;
        private static String directory;

        private const int BUFFER_SIZE = 999999999;

        public Client2()
        {
            InitializeComponent();
            btnDisconnect.Enabled = btnReconnect.Enabled = dirPanel.Enabled = false;
        }

        private void ConnectToServer()
        {
            try
            {
                // 1. Connect to server
                client = new TcpClient();
                client.Connect(host, port);

                btnDisconnect.Enabled = btnReconnect.Enabled = dirPanel.Enabled = true;
                txtHost.Enabled = txtPort.Enabled = btnConnect.Enabled = false;

                lbStatus.Text = "Connected";
                lbDetail.Caption = "Connected to " + client.Client.RemoteEndPoint;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Name);
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                client.Close();
                //client.Dispose();

                lbStatus.Text = "Not Connected";
                lbDetail.Caption = "Disconnected";
                btnDisconnect.Enabled = btnReconnect.Enabled = dirPanel.Enabled = false;
                txtHost.Enabled = txtPort.Enabled = btnConnect.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void LoadDirectory(Dir directoryCollection)
        {
            //DirectoryInfo di = new DirectoryInfo(Dir);
            TreeNode tds = directoryView.Nodes.Add(directoryCollection.Name);
            tds.Tag = directoryCollection.Path;
            //tds.StateImageIndex = 0;

            //Load tất cả các file bên trong đường dẫn cha
            LoadFiles(directoryCollection, tds);

            //Load tất cả các thư mục con bên trong đường dẫn cha
            LoadSubDirectories(directoryCollection, tds);
        }

        private void LoadSubDirectories(Dir parrentDirectory, TreeNode td)
        {
            // Lấy tất cả các thư mục con trong đường dẫn cha  
            //String[] subdirectoryEntries = Directory.GetDirectories(parrentDirectory);

            // Lặp qua tất cả các đường dẫn đó
            foreach (Dir subdirectory in parrentDirectory.SubDirectories)
            {

                //DirectoryInfo di = new DirectoryInfo(subdirectory);
                TreeNode tds = td.Nodes.Add(subdirectory.Name);
                //tds.StateImageIndex = 0;
                tds.Tag = subdirectory.Path;
                LoadFiles(subdirectory, tds);
                LoadSubDirectories(subdirectory, tds);
            }
        }

        private void LoadFiles(Dir dir, TreeNode td)
        {
            //String[] Files = Directory.GetFiles(dir, "*.*");

            // Lặp qua các file trong thư mục 
            foreach (FileDir file in dir.SubFiles)
            {
                //FileInfo fi = new FileInfo(file);
                TreeNode tds = td.Nodes.Add(file.Name);
                tds.Tag = file.Path;
                //tds.StateImageIndex = 1;
                //UpdateProgress();

            }
        }

        private object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            object obj = (object)binForm.Deserialize(memStream);
            return obj;
        }

        //btnShow
        private void btnShow_Click(object sender, EventArgs e)
        {
            directory = txtDirectory.Text;
            try
            {
                Stream stream = client.GetStream();

                // 2. send
                String dir = txtDirectory.Text;
                byte[] dataSize = Encoding.ASCII.GetBytes(dir.Length.ToString());
                stream.Write(dataSize, 0, dataSize.Length);

                byte[] data = Encoding.ASCII.GetBytes(dir);
                stream.Write(data, 0, data.Length);

                // 3. receive
                data = new byte[BUFFER_SIZE];
                stream.Read(data, 0, BUFFER_SIZE);

                Dir directoryCollection = (Dir)ByteArrayToObject(data);
                LoadDirectory(directoryCollection);

                //MessageBox.Show(Encoding.ASCII.GetString(data), this.Name);

                // 4. Close
                stream.Close();
                client.Close();

                // 5. Reconnect
                ConnectToServer();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), this.Name);
            }
        }

        //btnExit
        private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
            Environment.Exit(Environment.ExitCode);
        }

        //btnReconnect
        private void btnReconnect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            host = txtHost.Text;
            port = int.Parse(txtPort.Text);

            ConnectToServer();
        }
    }
}