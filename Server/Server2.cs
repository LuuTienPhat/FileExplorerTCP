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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Server2 : DevExpress.XtraEditors.XtraForm
    {
        private static int port;
        private static string address;
        private TcpListener server;
        static ASCIIEncoding encoding = new ASCIIEncoding();
        private const int BUFFER_SIZE = 1024;

        public Server2()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            address = txtAddress.Text;
            port = int.Parse(txtPort.Text);

            IPAddress host = IPAddress.Parse(address);

            server = new TcpListener(host, port);

            
            // 1. listen
            server.Start();
            lbStatus.Text = "Activated";
            lbDetail.Caption = "Server started on " + server.LocalEndpoint;


            Socket socket = server.AcceptSocket();
            Console.WriteLine("Connection received from " + socket.RemoteEndPoint);

            //// 2. receive
            //byte[] data = new byte[BUFFER_SIZE];
            //socket.Receive(data);

            //string str = encoding.GetString(data);

            //// 3. send
            //socket.Send(encoding.GetBytes("Hello " + str));

            //// 4. close
            //socket.Close();
            //server.Stop();
        }

        private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Dispose();
            Environment.Exit(Environment.ExitCode);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            server.Stop();
        }
    }
}