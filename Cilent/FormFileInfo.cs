using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SharedClass;

namespace Cilent
{
    public partial class FormFileInfo : DevExpress.XtraEditors.XtraForm
    {
        public FormFileInfo(FileView fileView)
        {
            InitializeComponent();

            this.Name = "";
            txtName.Text = fileView.fileInfo.Name;
            txtPath.Text = fileView.fileInfo.FullName;
            txtSize.Text = fileView.fileInfo.Length.ToString();
            //txtType.Text = fileView.fileInfo.Extension;
            txtDateModified.Text = fileView.fileInfo.LastWriteTime.ToString();
            txtDateCreated.Text = fileView.fileInfo.CreationTime.ToString();
        }

        public FormFileInfo(DirectoryView directoryView)
        {
            InitializeComponent();
            txtName.Text = directoryView.directoryInfo.Name;
            txtPath.Text = directoryView.directoryInfo.FullName;
            //txtType.Text = directoryView.directoryInfo.Extension;
            txtDateModified.Text = directoryView.directoryInfo.LastWriteTime.ToString();
            txtDateCreated.Text = directoryView.directoryInfo.CreationTime.ToString();
        }

        private void FormDetail_Load(object sender, EventArgs e)
        {

        }

        private void labelControl8_Click(object sender, EventArgs e)
        {

        }

        private void labelControl5_Click(object sender, EventArgs e)
        {

        }

        private void labelControl8_Click_1(object sender, EventArgs e)
        {

        }

        private void txtSize_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}