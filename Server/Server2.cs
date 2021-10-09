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
using MySharedClass;

namespace Server
{
    public partial class Server2 : DevExpress.XtraEditors.XtraForm
    {
        //public static ManualResetEvent allDone = new ManualResetEvent(false);

        private static int port;
        private static string address;
        private static TcpListener server;
        private const int BUFFER_SIZE = 1024;

        public Server2()
        {
            InitializeComponent();
        }
        private static byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        private void StartServer()
        {
            try
            {
                IPAddress host = IPAddress.Parse(address);
                server = new TcpListener(host, port);


                // 1. listen
                server.Start();
                lbStatus.Text = "Activated";
                lbDetail.Caption = "Server started on " + server.LocalEndpoint;

                while (true)
                {
                    Socket socket = server.AcceptSocket();

                    // 2. receive
                    byte[] data = new byte[BUFFER_SIZE];
                    socket.Receive(data);

                    // 3. handle
                    string directory = Encoding.ASCII.GetString(data);
                    //string dir2 = "C:\\Users\\Phat\\Documents\\Visual Studio 2019\\TCP";
                    MessageBox.Show(directory,this.Name);
                    Dir directoryCollection = LoadDirectory(directory);
                    byte[] sendData = ObjectToByteArray(directoryCollection);

                    // 4. send
                    socket.Send(sendData);

                    // 5. close
                    socket.Shutdown(SocketShutdown.Both);
                    //server.Stop();
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.ToString(), "Server2");
            }
        }

        public Dir LoadDirectory(string receiveDirectory)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(receiveDirectory);

            Dir currentDir = new Dir(directoryInfo.Name, directoryInfo.FullName);

            //Load tất cả các file bên trong đường dẫn cha
            LoadFiles(receiveDirectory, currentDir);

            //Load tất cả các thư mục con bên trong đường dẫn cha
            LoadSubDirectories(receiveDirectory, currentDir);

            return currentDir;
        }

        private void LoadSubDirectories(string parentDirectory, Dir directory)
        {
            // Lấy tất cả các thư mục con trong đường dẫn cha  
            string[] subdirectoryEntries = Directory.GetDirectories(parentDirectory);

            // Lặp qua tất cả các đường dẫn đó
            foreach (string subdirectory in subdirectoryEntries)
            {
                DirectoryInfo di = new DirectoryInfo(subdirectory);
                Dir currentDir = new Dir(di.Name, di.FullName);
                directory.SubDirectories.Add(currentDir);

                LoadFiles(subdirectory, currentDir);
                LoadSubDirectories(subdirectory, currentDir);
            }
        }

        private void LoadFiles(string parentDirectory, Dir directory)
        {
            string[] Files = Directory.GetFiles(parentDirectory);

            // Lặp qua các file trong thư mục 
            foreach (string file in Files)
            {
                FileInfo fi = new FileInfo(file);
                FileDir fileDir = new FileDir(fi.Name, fi.FullName);
                directory.SubFiles.Add(fileDir);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            
            address = txtAddress.Text;
            port = int.Parse(txtPort.Text);

            Thread theard = new Thread(StartServer);
            theard.Start();
        }

        private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
            Environment.Exit(Environment.ExitCode);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            server.Stop();
            lbStatus.Text = "Not Activated";
            lbDetail.Caption = "Server stopped";
        }

        private void btnRestart_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            server.Stop();
            server.Start();
            Socket socket = server.AcceptSocket();
        }
    }
}