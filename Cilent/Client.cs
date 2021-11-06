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
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.ViewInfo;

namespace Cilent
{
    public partial class Client : DevExpress.XtraEditors.XtraForm
    {
        private static String host;
        private static int port;
        private static TcpClient client;
        private const int BUFFER_SIZE = 999999999;
        public static DirectoryView directoryCollection = new DirectoryView();

        public Client()
        {
            InitializeComponent();
            connectFailed();
        }

        private void ConnectToServer()
        {
            try
            {
                // 1. Connect to server
                client = new TcpClient();
                client.Connect(host, port);
                connectSuccessfully();
                client.Close();
            }

            catch (Exception ex)
            {
                connectFailed();
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

        public void LoadDirectory(DirectoryView directoryCollection)
        {   
            TreeListNode tds = directoryView.AppendNode(new object[] { directoryCollection.directoryInfo.Name }, null);
            tds.Tag = directoryCollection.directoryInfo.FullName;
            tds.StateImageIndex = 0;
            LoadFiles(directoryCollection, tds);
            LoadSubDirectories(directoryCollection, tds);
        }

        private void LoadSubDirectories(DirectoryView parrentDirectory, TreeListNode td)
        {
            foreach (DirectoryView subdirectory in parrentDirectory.subDirectories)
            {
                TreeListNode tds = td.Nodes.Add(new object[] { subdirectory.directoryInfo.Name });
                tds.Tag = subdirectory.directoryInfo.FullName;
                tds.StateImageIndex = 0;
                LoadFiles(subdirectory, tds);
                LoadSubDirectories(subdirectory, tds);
            }
        }

        private void LoadFiles(DirectoryView dir, TreeListNode td)
        {
            foreach (FileView file in dir.subFiles)
            {
                TreeListNode tds = td.Nodes.Add(new object[] { file.fileInfo.Name });
                tds.Tag = file.fileInfo.FullName;
                assignIconToFile(file.fileInfo.FullName, tds);
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
            this.directoryView.Nodes.Clear();

            string directory = txtDirectory.Text;
            if (directory.Length == 0)
            {
                MessageBox.Show("Please enter directory!", this.Name);
                return;
            }
            else if (!isDirectory(directory))
            {
                MessageBox.Show("The path is not directory!", this.Name);
                return;
            }
            try
            {
                //1.Connect to server
                client = new TcpClient();
                client.Connect(host, port);
                Stream stream = client.GetStream();

                // 2. send
                byte[] data = Encoding.ASCII.GetBytes(getFilter() + directory);
                stream.Write(data, 0, data.Length);

                // 3. receive
                byte[] receiveDataByte = new byte[BUFFER_SIZE];
                int length = stream.Read(receiveDataByte, 0, BUFFER_SIZE);
                Array.Copy(receiveDataByte, receiveDataByte, length);
                directoryCollection = (DirectoryView)ByteArrayToObject(receiveDataByte);

                if (isCollectionEmpty(directoryCollection))
                {
                    this.directoryView.Nodes.Add("Not Found");
                }
                else LoadDirectory(directoryCollection);

                // 4. Close
                stream.Close();
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
            btnConnect.Enabled = false;
            btnDisconnect.Enabled = btnReconnect.Enabled = btnShow.Enabled = true;
            txtHost.Enabled = txtPort.Enabled = false;
            lbStatus.Text = "Connected";
            lbDetail.Caption = "Connected to " + client.Client.RemoteEndPoint;
            resultPanel.Enabled = true;
        }

        private void connectFailed()
        {
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = btnReconnect.Enabled = btnShow.Enabled = false;
            txtHost.Enabled = txtPort.Enabled = true;
            resultPanel.Enabled = false;
            lbStatus.Text = "Not Connected";
            lbDetail.Caption = "";
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            host = txtHost.Text;
            port = int.Parse(txtPort.Text);
            ConnectToServer();
        }

        private bool isCollectionEmpty(DirectoryView directoryCollection)
        {
            if (directoryCollection.directoryInfo == null) return true;
            return false;
        }

        private string getFilter()
        {
            List<object> filters = cbxFilter.Properties.GetItems().GetCheckedValues();
            string requestWithFileTypeFilter = "filtered";

            foreach (string filter in filters)
            {
                if (filter.Contains("all"))
                {
                    requestWithFileTypeFilter = requestWithFileTypeFilter + "*" + "all";
                    break;
                }
                else
                {
                    if (filter.Contains("folder"))
                    {
                        requestWithFileTypeFilter = requestWithFileTypeFilter + "*" + "folder";
                        break;
                    }

                    if (filter.Contains("sound"))
                    {
                        requestWithFileTypeFilter = requestWithFileTypeFilter + "*" + "sound";
                    }
                    if (filter.Contains("video"))
                    {
                        requestWithFileTypeFilter = requestWithFileTypeFilter + "*" + "video";
                    }
                    if (filter.Contains("text"))
                    {
                        requestWithFileTypeFilter = requestWithFileTypeFilter + "*" + "text";
                    }
                    if (filter.Contains("image"))
                    {
                        requestWithFileTypeFilter = requestWithFileTypeFilter + "*" + "image";
                    }
                    if (filter.Contains("compressed"))
                    {
                        requestWithFileTypeFilter = requestWithFileTypeFilter + "*" + "compressed";
                    }
                }
            }
            return requestWithFileTypeFilter + "*";
        }

        private void btnClearConsole_Click(object sender, EventArgs e)
        {
            this.directoryView.Nodes.Clear();
        }

        private void btnRefreshConsole_Click(object sender, EventArgs e)
        {
            this.directoryView.Nodes.Clear();
            LoadDirectory(directoryCollection);
        }

        private bool isDirectory(string path)
        {
            if (Path.HasExtension(path)) return false;
            return true;
        }

        private void searchFileView(DirectoryView collection, ref FileView fileView, string path)
        {
            if (collection.subFiles.Count() != 0)
            {
                foreach (FileView file in collection.subFiles)
                {
                    if (file.fileInfo.FullName.Equals(path))
                    {
                        fileView = file;
                    }
                }
            }
            if (collection.subDirectories.Count != 0)
            {
                foreach (DirectoryView directory in collection.subDirectories)
                {
                    searchFileView(directory, ref fileView, path);
                }
            }
        }

        private void searchDirectoryView(DirectoryView collection, ref DirectoryView directoryView, string path)
        {
            if (collection.directoryInfo.FullName.Equals(path)) directoryView = collection;
            else
            {
                if(collection.subDirectories.Count != 0)
                {
                    foreach (DirectoryView directory in collection.subDirectories)
                    {
                        searchDirectoryView(directory, ref directoryView, path);
                    }
                }
            }
        }

        private void btnViewInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode node = this.directoryView.FocusedNode;
            if (isDirectory(node.Tag.ToString()))
            {
                DirectoryView directoryView = null;
                searchDirectoryView(directoryCollection, ref directoryView, node.Tag.ToString());
                new FormDirectoryInfo(directoryView).Show();
            }
            else
            {
                FileView fileView = null;
                searchFileView(directoryCollection, ref fileView, node.Tag.ToString());
                new FormFileInfo(fileView).Show();
            }
        }
        

        private void btnDownload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                TreeListNode node = this.directoryView.FocusedNode;
                client = new TcpClient();
                client.Connect(host, port);
                Stream stream = client.GetStream();

                // 2. send
                byte[] data = Encoding.ASCII.GetBytes("download*" + node.Tag.ToString());
                stream.Write(data, 0, data.Length);

                // 3. receive
                byte[] receiveDataByte = new byte[BUFFER_SIZE];
                int length = stream.Read(receiveDataByte, 0, BUFFER_SIZE);

                saveFileDialog.ShowDialog();
                MessageBox.Show(saveFileDialog.FileName);

                if (saveFileDialog.FileName.Length != 0)
                {
                    using (FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.CreateNew, FileAccess.Write))
                    {
                        fs.Write(receiveDataByte, 0, length);
                    }
                }

                // 4. Close
                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), this.Name);
            }

        }

        private static List<string> soundExtensions = new List<string>(new string[] { "mp3", "m4p", "m4a", "flac" });
        private static List<string> videoExtensions = new List<string>(new string[] { "mp4", "mkv", "webm", "flv" });
        private static List<string> textExtensions = new List<string>(new string[] { "txt", "doc", "docx" });
        private static List<string> imageExtensions = new List<string>(new string[] { "jpg", "jpeg", "png", "bmp" });
        private static List<string> compressedExtensions = new List<string>(new string[] { "7z", "rar", "zip" });

        private void assignIconToFile(string path, TreeListNode tds)
        {
            string fileType = path.Substring(path.LastIndexOf(".") + 1).ToLower();

            if (textExtensions.Contains(fileType))
            {
                tds.StateImageIndex = 2;
                return;
            }
            else if (soundExtensions.Contains(fileType))
            {
                tds.StateImageIndex = 4;
                return;
            }
            else if (imageExtensions.Contains(fileType))
            {
                tds.StateImageIndex = 3;
                return;
            }
            else if (videoExtensions.Contains(fileType))
            {
                tds.StateImageIndex = 5;
                return;
            }

            else if (compressedExtensions.Contains(fileType))
            {
                tds.StateImageIndex = 6;
                return;
            }
            else
            {
                tds.StateImageIndex = 1;
                return;
            }

        }

        private void directoryView_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if(isDirectory(e.Node.Tag.ToString()))
                {
                    btnDownload.Enabled = false;
                }
                else
                {
                    btnDownload.Enabled = true;
                }
                popupMenu.ShowPopup(Cursor.Position);
                
            }
        }

        private void directoryView_MouseMove(object sender, MouseEventArgs e)
        {
            TreeListNode theNode = this.directoryView.GetNodeAt(e.X, e.Y);
            if (theNode != null && theNode.Tag != null)
            {
                if (theNode.Tag.ToString() != toolTip.GetToolTip(this.directoryView))
                {
                    toolTip.SetToolTip(this.directoryView, theNode.Tag.ToString());
                }
            }
            else
            {
                toolTip.SetToolTip(this.directoryView, "");
            }
        }
    }
}
