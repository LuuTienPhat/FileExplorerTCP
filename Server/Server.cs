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
        Thread thrd;
        public Server()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
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


            thrd = new Thread(() => AsynchronousSocketListener.StartListening());
            thrd.IsBackground = true;
            thrd.Start();
        }

        [Obsolete]
        private void btnStop_Click(object sender, EventArgs e)
        {
            thrd.Suspend();
        }
    }
}
