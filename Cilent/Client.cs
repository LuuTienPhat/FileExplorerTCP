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

namespace Cilent
{

    public partial class Client : Form
    {
        TcpClient clientSocket = new TcpClient();
        NetworkStream serverStream = default(NetworkStream);
        String readData = null;

        private static Socket client;
        private static byte[] data = new byte[1024];

        public static String messageCurrent = "hello";
        public Client()
        {
            InitializeComponent();
            btnBrowse.Enabled = btnCheck.Enabled = txtDirectory.Enabled = txtResult.Enabled = false;
        }

        private void sendData(IAsyncResult iar)
        {
            Socket remote = (Socket)iar.AsyncState;
            int sent = remote.EndSend(iar);
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (txtDirectory.Text == "")
            {
                MessageBox.Show("Please choose directory", "Warning", MessageBoxButtons.OK);
                txtDirectory.Focus();
                return;
            }

            byte[] outStream = Encoding.UTF8.GetBytes(txtDirectory.Text);
            client.BeginSend(outStream, 0, outStream.Length, 0, new AsyncCallback(sendData), client);

            //serverStream.Write(outStream, 0, outStream.Length);
            //serverStream.Flush();
        }

       

        private void getMessage()
        {
            string returnData;
            while (true)
            {
                serverStream = clientSocket.GetStream();
                var buffsize = clientSocket.ReceiveBufferSize;
                byte[] instream = new byte[buffsize];
                serverStream.Read(instream, 0, buffsize);

                returnData = System.Text.Encoding.UTF8.GetString(instream);

                readData = returnData;
                msg();
            }
        }

        private void msg()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(msg));
            }
            else
            {
                txtDirectory.Text = readData;
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
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

            txtResult.Items.Add("Connecting...");
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint iPEnd = new IPEndPoint(IPAddress.Parse(txtHost.Text), int.Parse(txtPort.Text));
            client.BeginConnect(iPEnd, new AsyncCallback(Connected), client);

            //try
            //{
            //    clientSocket.Connect(txtHost.Text, Int32.Parse(txtPort.Text));
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK);
            //    return;
            //}


            //Thread thread = new Thread(getMessage);
            //thread.Start();
            //btnBrowse.Enabled = btnCheck.Enabled = txtDirectory.Enabled = listFiles.Enabled = true;

        }

        private void Connected(IAsyncResult asyncResult)
        {
            try
            {
                client.EndConnect(asyncResult);
                txtResult.Items.Add("Connected to: " + client.RemoteEndPoint.ToString());
                Thread receiver = new Thread(new ThreadStart(ReceiveData));
                receiver.Start();
                btnBrowse.Enabled = btnCheck.Enabled = txtDirectory.Enabled = txtResult.Enabled = true;
            }
            catch (SocketException ex)
            {
                txtResult.Items.Add("Connecting error");
                MessageBox.Show(ex.Message + "\n" + ex.SocketErrorCode);
            }
        }

        private void ReceiveData()
        {
            int recv;
            String stringData;
            while (true)
            {
                recv = client.Receive(data);
                stringData = Encoding.UTF8.GetString(data, 0, recv);
                if (stringData == "bye") break;
                txtResult.Items.Add(stringData);

            }
            stringData = "bye";
            byte[] message = Encoding.UTF8.GetBytes(stringData);
            client.Send(message);
            client.Close();
            txtResult.Items.Add("Connection stop");
            return;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            txtDirectory.Text = folderBrowserDialog.SelectedPath;
        }


    }
}
