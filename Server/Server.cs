using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;
using SharedClass;
using System.Net.NetworkInformation;

namespace Server
{
    public partial class Server : DevExpress.XtraEditors.XtraForm
    {
        //public static ManualResetEvent allDone = new ManualResetEvent(false);

        private static int port;
        private static String address;
        private static TcpListener server;
        private const int BUFFER_SIZE = 999999999;
        private static Thread serverTheard;
        private static IPAddress host;

        public Server()
        {
            InitializeComponent();
            btnRestart.Enabled = btnStop.Enabled = clientPanel.Enabled = false;
        }

        private static byte[] ObjectToByteArray(object obj)
        {
            if (obj == null) return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        private void StartServer()
        {
            while (true)
            {
                // 1. accept
                TcpClient client = server.AcceptTcpClient();
                this.Invoke((MethodInvoker)delegate
                {
                    clientList.Items.Add(client.Client.RemoteEndPoint);
                });

                Stream stream = client.GetStream();

                // 2. receive
                byte[] receivedDataByte = new byte[BUFFER_SIZE];
                int length = stream.Read(receivedDataByte, 0, BUFFER_SIZE);

                // 3. handle
                if (length != 0)
                {
                    string receivedData = Encoding.ASCII.GetString(receivedDataByte, 0, length);
                    
                    string[] requestSplit = receivedData.Split('*');

                    if(requestSplit[0].Equals("download"))
                    {

                        byte[] sendData = FileToByteArray(requestSplit[requestSplit.Length - 1]);

                        // 4. send
                        stream.Write(sendData, 0, sendData.Length);
                    }
                    else
                    {
                        List<string> filters = new List<string>();

                        for (int i = 1; i < requestSplit.Length - 1; i++)
                        {
                            filters.Add(requestSplit[i]);
                        }

                        DirectoryView directoryCollection = LoadDirectory(requestSplit[requestSplit.Length - 1], filters);
                        byte[] sendData = ObjectToByteArray(directoryCollection);

                        // 4. send
                        stream.Write(sendData, 0, sendData.Length);
                    }

                    // 5. close
                    stream.Close();
                    client.Close();
                }
            }
        }

        public DirectoryView LoadDirectory(String receiveDirectory, List<string> filters)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(receiveDirectory);
                DirectoryView directoryCollection = new DirectoryView(directoryInfo);

                if (!filters.Contains("folder"))
                {
                    LoadFiles(receiveDirectory, directoryCollection, filters);
                }  
                LoadSubDirectories(receiveDirectory, directoryCollection, filters);
                return directoryCollection;
            }
            catch (Exception ex)
            {
                return new DirectoryView();
            }
        }

        private void LoadSubDirectories(String parentDirectory, DirectoryView directoryCollection, List<string> filters)
        {
            String[] subdirectoryEntries = Directory.GetDirectories(parentDirectory);
            foreach (String subdirectory in subdirectoryEntries)
            {
                DirectoryInfo di = new DirectoryInfo(subdirectory);
                DirectoryView currentDir = new DirectoryView(di);
                directoryCollection.subDirectories.Add(currentDir);

                if (!filters.Contains("folder"))
                {
                    LoadFiles(subdirectory, currentDir, filters);
                }
                LoadSubDirectories(subdirectory, currentDir, filters);
            }
        }

        private void LoadFiles(String parentDirectory, DirectoryView directoryCollection, List<string> filters)
        {
            String[] Files = Directory.GetFiles(parentDirectory);
            foreach (String file in Files)
            {
                FileInfo fi = new FileInfo(file);
                if (filters.Contains("all"))
                {
                    FileView fileDir = new FileView(fi, "");
                    directoryCollection.subFiles.Add(fileDir);
                }
                else
                {
                    for (int i = 0; i < filters.Count; i++)
                    {
                        if (isEndWith(filters[i], fi.Extension))
                        {
                            FileView fileDir = new FileView(fi, "");
                            directoryCollection.subFiles.Add(fileDir);
                        }
                            
                    }
                }
            }
        }


        private byte[] FileToByteArray(string path)
        {
            try
            {
                Stream fileStream = File.OpenRead(path);
                byte[] fileBuffer = new byte[fileStream.Length];
                fileStream.Read(fileBuffer, 0, (int)fileStream.Length);
                return fileBuffer;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), this.Name);
                return new byte[BUFFER_SIZE];
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (acceptAll.Checked)
            {
                host = IPAddress.Any;
            }
            else
            {
                address = txtAddress.Text;
                host = IPAddress.Parse(address);
            }
            port = int.Parse(txtPort.Text);

            try
            {
                server = new TcpListener(host, port);

                // 1. listen
                server.Start();
                lbStatus.Text = "Activated";
                lbDetail.Caption = "Server started on " + server.LocalEndpoint;

                txtAddress.Enabled = txtPort.Enabled = acceptAll.Enabled = btnStart.Enabled = false;
                btnRestart.Enabled = btnStop.Enabled = clientPanel.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), this.Name);
            }


            serverTheard = new Thread(StartServer);
            serverTheard.Start();
        }

        private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
            Environment.Exit(Environment.ExitCode);
        }

        [Obsolete]
        private void btnStop_Click(object sender, EventArgs e)
        {
            serverTheard.Suspend();
            server.Stop();
            lbStatus.Text = "Not Activated";
            lbDetail.Caption = "Server stopped";

            btnRestart.Enabled = btnStop.Enabled = false;
            acceptAll.Enabled = txtPort.Enabled = btnStart.Enabled = true;
            if (acceptAll.Checked) txtAddress.Enabled = false;
            else txtAddress.Enabled = true;
        }

        [Obsolete]
        private void btnRestart_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                serverTheard.Suspend();
                server.Stop();

                lbStatus.Text = "Not Activated";
                lbDetail.Caption = "Server stopped";

                server = new TcpListener(host, port);

                // 1. listen
                server.Start();
                lbStatus.Text = "Activated";
                lbDetail.Caption = "Server started on " + server.LocalEndpoint;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), this.Name);
            }


            serverTheard = new Thread(StartServer);
            serverTheard.Start();
        }

        private void acceptAll_CheckedChanged(object sender, EventArgs e)
        {
            switch (acceptAll.Checked)
            {
                case true:
                    txtAddress.Enabled = false;
                    break;
                case false:
                    txtAddress.Enabled = true;
                    break;
            }
        }

        private static List<string> soundExtensions = new List<string>(new string[] { ".mp3", ".m4p", ".m4a", ".flac" });
        private static List<string> videoExtensions = new List<string>(new string[] { ".mp4", ".mkv", ".webm", ".flv" });
        private static List<string> textExtensions = new List<string>(new string[] { ".txt", ".doc", ".docx" });
        private static List<string> imageExtensions = new List<string>(new string[] { ".jpg", ".jpeg", ".png", ".bmp" });
        private static List<string> compressedExtensions = new List<string>(new string[] { ".7z", ".rar", ".zip" });
        private static List<string> programExtensions = new List<string>(new string[] { ".exe", ".msi" });

        public bool isEndWith(string fileType, string fileExtension)
        {
            switch (fileType)
            {
                case "sound":
                    if (soundExtensions.Contains(fileExtension.ToLower()))
                    {
                        return true;
                    }
                    break;
                case "video":
                    if (videoExtensions.Contains(fileExtension.ToLower()))
                    {
                        return true;
                    }
                    break;
                case "text":
                    if (textExtensions.Contains(fileExtension.ToLower()))
                    {
                        return true;
                    }
                    break;
                case "image":
                    if (imageExtensions.Contains(fileExtension.ToLower()))
                    {
                        return true;
                    }
                    break;
                case "compressed":
                    if (compressedExtensions.Contains(fileExtension.ToLower()))
                    {
                        return true;
                    }
                    break;
                case "program":
                    if (programExtensions.Contains(fileExtension.ToLower()))
                    {
                        return true;
                    }
                    break;
                case "all":
                    return true;
                default:
                    if (fileExtension.Substring(fileExtension.LastIndexOf(".") + 1).ToLower().Equals(fileType.ToLower()))
                    {
                        return true;
                    }
                    break;
            }

            return false;
        }
    }
}