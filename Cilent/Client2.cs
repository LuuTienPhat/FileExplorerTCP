using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        public Client2()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            host = txtHost.Text;
            port = int.Parse(txtPort.Text);

            client = new TcpClient(host, port);

            
        }
    }
}