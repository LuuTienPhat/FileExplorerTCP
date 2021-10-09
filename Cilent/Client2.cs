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

namespace Cilent
{
    public partial class Client2 : DevExpress.XtraEditors.XtraForm
    {
        private static string host;
        private static int port;
        private TcpClient client;
        private static string directory;

        private const int BUFFER_SIZE = 1024;
        static ASCIIEncoding encoding = new ASCIIEncoding();

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
                Stream stream = client.GetStream();

                // 2. send
                string str = "Phat";
                byte[] data = encoding.GetBytes(str);

                stream.Write(data, 0, data.Length);

                // 3. receive
                data = new byte[BUFFER_SIZE];
                stream.Read(data, 0, BUFFER_SIZE);

                MessageBox.Show(encoding.GetString(data), this.Name);
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
    }

    [Serializable]
    public class FileDir
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public FileDir(String name, String path)
        {
            this.Name = name;
            this.Path = path;
        }

        public FileDir() { }
    }

    [Serializable]
    public class Dir
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public List<Dir> SubDirectories { get; set; }
        public List<FileDir> SubFiles { get; set; }

        public Dir(String name, String path)
        {
            this.Name = name;
            this.Path = path;
            SubFiles = new List<FileDir>();
            SubDirectories = new List<Dir>();
        }

        public Dir() { }
    }
}