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
using SharedClass;
using System.Threading;

namespace Cilent
{
    public partial class Client : DevExpress.XtraEditors.XtraForm
    {
        private static String host;
        private static int port;
        private static TcpClient client;
        private const int BUFFER_SIZE = 999999999;

        public Client()
        {
            InitializeComponent();
            btnDisconnect.Enabled = btnReconnect.Enabled = resultPanel.Enabled = false;
        }

        private void ConnectToServer()
        {
            try
            {
                // 1. Connect to server
                client = new TcpClient();
                client.Connect(host, port);

                btnDisconnect.Enabled = btnReconnect.Enabled = resultPanel.Enabled = true;
                txtHost.Enabled = txtPort.Enabled = btnConnect.Enabled = false;

                lbStatus.Text = "Connected";
                lbDetail.Caption = "Connected to " + client.Client.RemoteEndPoint;

                client.Close();
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
                connectFailed();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                connectSuccessfully();
            }

        }

        public void LoadDirectory(Dir directoryCollection)
        {
            //DirectoryInfo di = new DirectoryInfo(Dir);
            TreeNode tds = this.directoryView.Nodes.Add(directoryCollection.Name);
            //Directory.CreateDirectory("C:\\Test\\" + directoryCollection.Name);
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
                //Byte[] bytes = Convert.FromBase64String(file.Data);
                //ToString("C:\\Test\\" + file.Name, file.Data);
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
            String directory = txtDirectory.Text;
            if (directory.Length == 0)
            {
                MessageBox.Show("Please type directory", this.Name);
                return;
            }
            try
            {
                //1.Connect to server
                client = new TcpClient();
                client.Connect(host, port);
                Stream stream = client.GetStream();

                // 2. send
                //byte[] dataSize = Encoding.ASCII.GetBytes(directory.Length.ToString());
                //stream.Write(dataSize, 0, dataSize.Length);

                byte[] data = Encoding.ASCII.GetBytes(directory);
                stream.Write(data, 0, data.Length);

                // 3. receive
                //byte[] receiveDataByteSize = new byte[BUFFER_SIZE];
                byte[] receiveDataByte = new byte[BUFFER_SIZE];
                // stream.Read(receiveDataByteSize, 0, BUFFER_SIZE);
                stream.Read(receiveDataByte, 0, BUFFER_SIZE);

                // Show Directory
                //int dataSize = int.Parse(Encoding.ASCII.GetString(receiveDataByteSize));
                //byte[] directoryCollectionByte = new byte[dataSize];
                //Array.Copy(receiveDataByte, directoryCollectionByte, dataSize);

                Dir directoryCollection = (Dir)ByteArrayToObject(receiveDataByte);
                if (isCollectionEmpty(directoryCollection)) this.directoryView.Nodes.Add("Not Found");
                else LoadDirectory(directoryCollection);

                // 4. Close
                //stream.Close();
                client.Close();

                // 5. Reconnect
                //ConnectToServer();
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
            try
            {
                client.Close();
                client = new TcpClient();
                client.Connect(host, port);

                Thread t = new Thread(connectSuccessfully);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), this.Name);
                connectFailed();
            }
        }

        private void connectSuccessfully()
        {
            btnDisconnect.Enabled = btnReconnect.Enabled = btnShow.Enabled = true;
            txtHost.Enabled = txtPort.Enabled = false;
            txtDirectory.Enabled = directoryView.Enabled = true;
            lbStatus.Text = "Connected";
            lbDetail.Caption = "Connected to " + client.Client.RemoteEndPoint;
        }

        private void connectFailed()
        {
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = btnReconnect.Enabled = btnShow.Enabled = false;
            txtHost.Enabled = txtPort.Enabled = false;
            txtDirectory.Enabled = directoryView.Enabled = false;
            lbStatus.Text = "Not Connected";
            lbDetail.Caption = "";
        }



        private void btnConnect_Click(object sender, EventArgs e)
        {
            host = txtHost.Text;
            port = int.Parse(txtPort.Text);

            ConnectToServer();
        }

        private bool isCollectionEmpty(Dir directoryCollection)
        {
            if (directoryCollection.Name == null) return true;
            if (directoryCollection.Path == null) return true;
            return false;
        }

        private void directoryView_MouseMove(object sender, MouseEventArgs e)
        {
            // Get the node at the current mouse pointer location.  
            TreeNode theNode = this.directoryView.GetNodeAt(e.X, e.Y);

            // Set a ToolTip only if the mouse pointer is actually paused on a node.  
            if (theNode != null && theNode.Tag != null)
            {
                // Change the ToolTip only if the pointer moved to a new node.  
                if (theNode.Tag.ToString() != toolTip.GetToolTip(this.directoryView))
                    toolTip.SetToolTip(this.directoryView, theNode.Tag.ToString());

            }
            else     // Pointer is not over a node so clear the ToolTip.  
            {
                toolTip.SetToolTip(this.directoryView, "");
            }
        }

        private void directoryView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode theNode = this.directoryView.GetNodeAt(e.X, e.Y);
                popupMenu.ShowPopup(Cursor.Position);


            }

        }
    }
}