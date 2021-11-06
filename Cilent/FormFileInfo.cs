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
            this.Text = fileView.fileInfo.Name + " Properties";
            txtName.Text = fileView.fileInfo.Name;
            txtPath.Text = fileView.fileInfo.FullName;
            txtType.Text = fileView.fileInfo.Extension;
            txtSize.Text = ConvertFileSize(fileView.fileInfo.Length);
            txtDateModified.Text = fileView.fileInfo.LastWriteTime.ToString();
            txtDateCreated.Text = fileView.fileInfo.CreationTime.ToString();
            if (fileView.fileInfo.IsReadOnly) ckReadOnly.CheckState = CheckState.Checked;
            if (fileView.fileInfo.Attributes.HasFlag(FileAttributes.Hidden)) ckHidden.CheckState = CheckState.Checked;
        }

        public static string ConvertFileSize(double length)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            while (length >= 1024 && order < sizes.Length - 1)
            {
                order++;
                length = length / 1024;
            }

            return string.Format("{0:0.##} {1}", length, sizes[order]);
        }
    }
}