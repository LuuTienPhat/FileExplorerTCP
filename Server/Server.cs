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
            ServerStopped();
            GC.Collect();
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
                try
                {
                    // 1. accept
                    TcpClient client = server.AcceptTcpClient();
                    this.Invoke((MethodInvoker)delegate
                    {
                        clientList.Items.Add(client.Client.RemoteEndPoint);
                    });

                    NetworkStream stream = client.GetStream();

                    // 2. receive
                    byte[] receivedDataByte = new byte[BUFFER_SIZE];
                    int length = stream.Read(receivedDataByte, 0, BUFFER_SIZE);

                    // 3. handle
                    if (length != 0)
                    {
                        string receivedData = Encoding.ASCII.GetString(receivedDataByte, 0, length);

                        string[] requestSplit = receivedData.Split('*');

                        if (requestSplit[0].Equals("download"))
                        {
                            FileInfo fi = new FileInfo(requestSplit[requestSplit.Length - 1]);
                            FileStream fs = new FileStream(fi.FullName, FileMode.Open, FileAccess.Read, FileShare.Read);

                            //4. send
                            byte[] fileLength = Encoding.UTF8.GetBytes(fs.Length.ToString());
                            stream.Write(fileLength, 0, fileLength.Length);
                            stream.Flush();

                            progressBar.EditValue = 0;
                            sendFile(stream, fs, fi);
                            lbDetail.Caption = "Listening on " + server.LocalEndpoint;
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
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString(), this.Name);
                }
                GC.Collect();
            }

        }

        public void ServerStopped()
        {
            lbStatus.Text = "Not Activated";
            lbDetail.Caption = "Server stopped!";
            txtAddress.Enabled = txtPort.Enabled = acceptAll.Enabled = btnStart.Enabled = true;
            btnRestart.Enabled = btnStop.Enabled = clientPanel.Enabled = false;

            progressBar.EditValue = 0;
            clientPanel.Enabled = false;

            if (acceptAll.Checked) txtAddress.Enabled = false;
            else txtAddress.Enabled = true;

            clientList.Items.Clear();
        }

        public void ServerStarted()
        {
            lbStatus.Text = "Activated";
            lbDetail.Caption = "Listening on " + server.LocalEndpoint;
            txtAddress.Enabled = txtPort.Enabled = acceptAll.Enabled = btnStart.Enabled = false;
            btnRestart.Enabled = btnStop.Enabled = clientPanel.Enabled = true;

            progressBar.EditValue = 0;
            clientPanel.Enabled = true;
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
                ServerStarted();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), this.Name);
                ServerStopped();
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
            try
            {
                if (serverTheard.IsAlive)
                {
                    serverTheard.Suspend();
                }
                server.Stop();
                ServerStopped();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        [Obsolete]
        private void btnRestart_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {

                if (serverTheard.IsAlive)
                {
                    serverTheard.Suspend();
                }
                server.Stop();
                ServerStopped();


                server = new TcpListener(host, port);
                // 1. listen
                server.Start();
                ServerStarted();

                serverTheard = new Thread(StartServer);
                serverTheard.Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), this.Name);
                ServerStopped();
            }

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

        public void sendFile(NetworkStream ns, FileStream fs, FileInfo fi)
        {
            //FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            //TcpClient client = new TcpClient("127.0.0.1", 6868);
            //NetworkStream ns = client.GetStream();
            byte[] data_tosend = CreateDataPacket(Encoding.UTF8.GetBytes("125"), Encoding.UTF8.GetBytes(fi.Name));
            ns.Write(data_tosend, 0, data_tosend.Length);
            ns.Flush();
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
                        case 126:
                            long recv_file_pointer = long.Parse(Encoding.UTF8.GetString(recv_data));
                            if (recv_file_pointer != fs.Length)
                            {
                                fs.Seek(recv_file_pointer, SeekOrigin.Begin);
                                int temp_buff_length = (int)(fs.Length - recv_file_pointer < 20000 ? fs.Length - recv_file_pointer : 20000);
                                byte[] temp_buff = new byte[temp_buff_length];
                                fs.Read(temp_buff, 0, temp_buff.Length);
                                byte[] data_to_send = CreateDataPacket(Encoding.UTF8.GetBytes("127"), temp_buff);
                                ns.Write(data_to_send, 0, data_to_send.Length);
                                ns.Flush();
                                this.Invoke((MethodInvoker)delegate
                                {
                                    lbDetail.Caption = "Uploading: " + (int)Math.Ceiling((double)recv_file_pointer / (double)fs.Length * 100) + " %";
                                    progressBar.EditValue = (int)Math.Ceiling((double)recv_file_pointer / (double)fs.Length * 100);
                                });
                            }
                            else
                            {
                                byte[] data_to_send = CreateDataPacket(Encoding.UTF8.GetBytes("128"), Encoding.UTF8.GetBytes("Close"));
                                ns.Write(data_to_send, 0, data_to_send.Length);
                                ns.Flush();
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
    }
}