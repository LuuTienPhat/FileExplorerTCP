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

                // 1. Kết nối đến Server
                client = new TcpClient(host, port);

                lbStatus.Text = "Connected";
                lbDetail.Caption = "Connected to " + client.Client.RemoteEndPoint;
                Stream stream = client.GetStream();
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

            //// 2. send
            //byte[] data = encoding.GetBytes(str);

            //stream.Write(data, 0, data.Length);

            //// 3. receive
            //data = new byte[BUFFER_SIZE];
            //stream.Read(data, 0, BUFFER_SIZE);

            //Console.WriteLine(encoding.GetString(data));
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
}