using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    public partial class Server : Form
    {
        IPEndPoint iPEndPoint;
        Socket server;
        public Server()
        {
            InitializeComponent();

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (txtHost.Text == "")
            {
                MessageBox.Show("Please enter host", "Warning", MessageBoxButtons.OK);
                txtHost.Focus();
                return;
            }

            if (txtPort.Text == "")
            {
                MessageBox.Show("Please enter port", "Warning", MessageBoxButtons.OK);
                txtPort.Focus();
                return;
            }

            iPEndPoint = new IPEndPoint(IPAddress.Parse(txtHost.Text), int.Parse(txtPort.Text));
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(iPEndPoint);
            server.Listen(100);
            txtResult.Items.Add("Start successfully");

            //int port = 7777;
            //string IpAddress = "127.0.0.1";
            //Socket ServerListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IpAddress), port);
            //ServerListener.Bind(ep);
            //ServerListener.Listen(100);
            //Console.WriteLine("Server is listening... ");
            //Socket ClientSocket = default(Socket);
            //int counter = 0;
            //Program pg = new Program();
            //while (true)
            //{
            //    counter++;
            //    ClientSocket = ServerListener.Accept();
            //    Console.WriteLine(counter + " Client(s) connected");
            //    Thread UserThread = new Thread(new ThreadStart(() => pg.User(ClientSocket)));
            //    UserThread.Start();
            //}

            //try
            //{
            //    server.Listen(int.Parse(txtPort.Text));
            //    Socket clientSocket = server.Accept();
            //    byte[] instream = new byte[9000];
            //    int insteamLength = clientSocket.Receive(instream);
            //    String directory = Encoding.UTF8.GetString(instream);
            //    txtResult.Items.Add(directory);
            //}
            //catch(SocketException ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            //}

        }
    }
}
