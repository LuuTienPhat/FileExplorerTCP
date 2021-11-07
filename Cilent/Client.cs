using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using SharedClass;
using System.Threading;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.Utils;

namespace Cilent
{
    public partial class Client : DevExpress.XtraEditors.XtraForm
    {
        private static string host;
        private static int port;
        private static TcpClient client;
        private const int BUFFER_SIZE = 999999999;
        public static DirectoryView directoryCollection = new DirectoryView();

        private static List<string> soundExtensions = new List<string>(new string[] { "mp3", "m4p", "m4a", "flac" });
        private static List<string> videoExtensions = new List<string>(new string[] { "mp4", "mkv", "webm", "flv" });
        private static List<string> textExtensions = new List<string>(new string[] { "txt", "doc", "docx" });
        private static List<string> imageExtensions = new List<string>(new string[] { "jpg", "jpeg", "png", "bmp" });
        private static List<string> compressedExtensions = new List<string>(new string[] { "7z", "rar", "zip" });

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

        private void connectSuccessfully()
        {
            btnConnect.Enabled = false;
            btnDisconnect.Enabled = btnReconnect.Enabled = btnShow.Enabled = true;
            txtHost.Enabled = txtPort.Enabled = false;
            lbStatus.Text = "Connected";
            lbDetail.Caption = "Connected to " + client.Client.RemoteEndPoint;
            resultPanel.Enabled = true;
            progressBar.EditValue = 0;
        }

        private void connectFailed()
        {
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = btnReconnect.Enabled = btnShow.Enabled = false;
            txtHost.Enabled = txtPort.Enabled = true;
            resultPanel.Enabled = false;
            lbStatus.Text = "Not Connected";
            lbDetail.Caption = "";
            progressBar.EditValue = 0;
            directoryView.Nodes.Clear();
        }


        #region Directory Handle

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

        #endregion

        #region Butttons

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
                client.ReceiveTimeout = 10000;

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
                    TreeListNode tds = directoryView.AppendNode(new object[] { "Directory not found on Server!" }, null);
                    tds.Tag = "Directory not found on Server!";
                    tds.StateImageIndex = 7;
                }
                else LoadDirectory(directoryCollection);

                // 4. Close
                stream.Close();
                client.Close();
                GC.Collect();

                // 5. Reconnect
                //ConnectToServer();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), this.Name);
                connectFailed();
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
                connectFailed();

                client = new TcpClient();
                client.Connect(host, port);
                connectSuccessfully();
                client.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Name);
                connectFailed();
            }
        }

        //btnConnect
        private void btnConnect_Click(object sender, EventArgs e)
        {
            host = txtHost.Text;
            port = int.Parse(txtPort.Text);
            ConnectToServer();
        }

        //btnDisconnect
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

        //btnViewInfo
        private void btnViewInfo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode node = this.directoryView.FocusedNode;
            if (isDirectory(node.Tag.ToString()))
            {
                DirectoryView directoryView = null;
                searchDirectoryView(directoryCollection, ref directoryView, node.Tag.ToString());
                new DirectoryInfo(directoryView).Show();
            }
            else
            {
                FileView fileView = null;
                searchFileView(directoryCollection, ref fileView, node.Tag.ToString());
                new FileInfo(fileView).Show();
            }
        }

        //btnDownload
        private void btnDownload_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            progressBar.EditValue = 0;
            try
            {
                TreeListNode node = this.directoryView.FocusedNode;
                if (!isDirectory(node.Tag.ToString()))
                {
                    saveFileDialog.FileName = this.directoryView.GetFocusedDisplayText();
                    DialogResult dialogResult = saveFileDialog.ShowDialog();

                    if (dialogResult == DialogResult.OK)
                    {
                        string saveFilePath = saveFileDialog.FileName;
                        Thread t = new Thread(() =>
                        {
                            // 2. send
                            client = new TcpClient();
                            client.Connect(host, port);
                            NetworkStream stream = client.GetStream();
                            byte[] data = Encoding.ASCII.GetBytes("download*" + node.Tag.ToString());
                            stream.Write(data, 0, data.Length);

                            byte[] _buffer = new byte[99999];
                            int length = stream.Read(_buffer, 0, _buffer.Length);
                            long fileLength = long.Parse(Encoding.UTF8.GetString(_buffer, 0, length));

                            // 3. receive
                            ReceiveFileFromServer(stream, saveFilePath, fileLength);

                            // 4. Close
                            stream.Close();
                            client.Close();

                            this.Invoke((MethodInvoker)delegate
                            {
                                client = new TcpClient();
                                client.Connect(host, port);
                                lbDetail.Caption = "Connected to " + client.Client.RemoteEndPoint;
                                client.Close();
                            });

                        });
                        t.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), this.Name);
            }
        }

        //btnClearConsole
        private void btnClearConsole_Click(object sender, EventArgs e)
        {
            this.directoryView.Nodes.Clear();
        }

        //btnRefreshConsole
        private void btnRefreshConsole_Click(object sender, EventArgs e)
        {
            this.directoryView.Nodes.Clear();
            LoadDirectory(directoryCollection);
        }

        #endregion

        #region Utilities

        private object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            object obj = (object)binForm.Deserialize(memStream);
            return obj;
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
       
        private bool isDirectory(string path)
        {
            if (Path.HasExtension(path)) return false;
            return true;
        }

        #endregion

        #region Directory View

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
                if (collection.subDirectories.Count != 0)
                {
                    foreach (DirectoryView directory in collection.subDirectories)
                    {
                        searchDirectoryView(directory, ref directoryView, path);
                    }
                }
            }
        }

        private void directoryView_RowClick(object sender, RowClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (isDirectory(e.Node.Tag.ToString()))
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
            this.directoryView.SetFocusedNode(theNode);
            //if (theNode != null && theNode.Tag != null)
            //{
            //    if (theNode.Tag.ToString() != toolTip.GetToolTip(this.directoryView))
            //    {
            //        toolTip.SetToolTip(this.directoryView, theNode.Tag.ToString());
            //    }
            //}
            //else
            //{
            //    toolTip.SetToolTip(this.directoryView, "");
            //}
        }

        private void toolTip_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl.Name == "directoryView")
            {
                DevExpress.Utils.SuperToolTip superToolTip = new DevExpress.Utils.SuperToolTip();
                DevExpress.Utils.ToolTipItem toolTipItem = new DevExpress.Utils.ToolTipItem();
                superToolTip.Items.Add(toolTipItem);
                ToolTipControlInfo myInfo = new DevExpress.Utils.ToolTipControlInfo();
                TreeListHitInfo hi = directoryView.CalcHitInfo(e.ControlMousePosition);
                if (hi.Node != null && hi.Column != null)
                {
                    string value = hi.Node.Id.ToString() + hi.Column.FieldName;
                    myInfo.Text = hi.Node.Tag.ToString();
                    myInfo.Object = value;
                }

                e.Info = myInfo;
            }
        }

        #endregion

        #region Download File Function

        public void ReceiveFileFromServer(NetworkStream ns, string saveFilePath, long fileLength)
        {
            FileStream fs = null;
            long current_file_pointer = 0;
            Boolean loop_break = false;
            while (true)
            {
                if (ns.ReadByte() == 2)
                {
                    byte[] cmd_buff = new byte[3];
                    ns.Read(cmd_buff, 0, cmd_buff.Length);
                    byte[] recv_data = ReadStream(ns);
                    switch (Convert.ToInt32(Encoding.UTF8.GetString(cmd_buff)))
                    {
                        case 125:
                            {
                                //fs = new FileStream(@"C:\test2\" + Encoding.UTF8.GetString(recv_data), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                                fs = new FileStream(saveFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                                byte[] data_to_send = CreateDataPacket(Encoding.UTF8.GetBytes("126"), Encoding.UTF8.GetBytes(Convert.ToString(current_file_pointer)));
                                ns.Write(data_to_send, 0, data_to_send.Length);
                                ns.Flush();
                            }
                            break;
                        case 127:
                            {
                                fs.Seek(current_file_pointer, SeekOrigin.Begin);
                                fs.Write(recv_data, 0, recv_data.Length);
                                current_file_pointer = fs.Position;
                                byte[] data_to_send = CreateDataPacket(Encoding.UTF8.GetBytes("126"), Encoding.UTF8.GetBytes(Convert.ToString(current_file_pointer)));
                                ns.Write(data_to_send, 0, data_to_send.Length);
                                ns.Flush();
                                this.Invoke((MethodInvoker)delegate
                                {
                                    lbDetail.Caption = "Downloading: " + (int)Math.Ceiling((double)current_file_pointer / (double)fileLength * 100) + " %";
                                    progressBar.EditValue = (int)Math.Ceiling((double)current_file_pointer / (double)fileLength * 100);
                                });
                            }
                            break;
                        case 128:
                            {
                                fs.Close();
                                loop_break = true;
                            }
                            break;
                        default:
                            break;
                    }
                }
                if (loop_break == true)
                {
                    ns.Close();
                    break;
                }
            }
        }

        public byte[] ReadStream(NetworkStream ns)
        {
            byte[] data_buff = null;

            int b = 0;
            String buff_length = "";
            while ((b = ns.ReadByte()) != 4)
            {
                buff_length += (char)b;
            }
            int data_length = Convert.ToInt32(buff_length);
            data_buff = new byte[data_length];
            int byte_read = 0;
            int byte_offset = 0;
            while (byte_offset < data_length)
            {
                byte_read = ns.Read(data_buff, byte_offset, data_length - byte_offset);
                byte_offset += byte_read;
            }

            return data_buff;
        }

        private byte[] CreateDataPacket(byte[] cmd, byte[] data)
        {
            byte[] initialize = new byte[1];
            initialize[0] = 2;
            byte[] separator = new byte[1];
            separator[0] = 4;
            byte[] datalength = Encoding.UTF8.GetBytes(Convert.ToString(data.Length));
            MemoryStream ms = new MemoryStream();
            ms.Write(initialize, 0, initialize.Length);
            ms.Write(cmd, 0, cmd.Length);
            ms.Write(datalength, 0, datalength.Length);
            ms.Write(separator, 0, separator.Length);
            ms.Write(data, 0, data.Length);
            return ms.ToArray();
        }

        #endregion
       
    }
}
