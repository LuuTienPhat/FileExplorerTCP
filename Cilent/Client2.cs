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
using SharedClass;
using System.Runtime.Serialization.Formatters.Binary;

namespace Cilent
{
    public partial class Client2 : DevExpress.XtraEditors.XtraForm
    {
        private static string host;
        private static int port;
        private static TcpClient client;
        private static string directory;

        private const int BUFFER_SIZE = 1024;

        public Client2()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                host = txtHost.Text;
                port = int.Parse(txtPort.Text);

                // 1. Connect to server
                client = new TcpClient();
                client.Connect(host, port);

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
                client.Dispose();
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
            //string[] subdirectoryEntries = Directory.GetDirectories(parrentDirectory);
            
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
            //string[] Files = Directory.GetFiles(dir, "*.*");

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
    }


}