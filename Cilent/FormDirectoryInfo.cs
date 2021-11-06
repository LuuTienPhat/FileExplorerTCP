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
using SharedClass;
using System.IO;

namespace Cilent
{
    public partial class FormDirectoryInfo : DevExpress.XtraEditors.XtraForm
    {


        public FormDirectoryInfo(DirectoryView directoryView)
        {
            InitializeComponent();

            this.Text = directoryView.directoryInfo.Name + " Properties";
            txtName.Text = directoryView.directoryInfo.Name;
            txtPath.Text = directoryView.directoryInfo.FullName;
            txtDateModified.Text = directoryView.directoryInfo.LastWriteTime.ToString();
            txtDateCreated.Text = directoryView.directoryInfo.CreationTime.ToString();
            txtContain.Text = countFiles(directoryView).ToString() + " Files, " + countFolders(directoryView).ToString() + " Folders";

            if (directoryView.directoryInfo.Attributes.HasFlag(FileAttributes.ReadOnly)) ckReadOnly.CheckState = CheckState.Checked;
            if (directoryView.directoryInfo.Attributes.HasFlag(FileAttributes.Hidden)) ckHidden.CheckState = CheckState.Checked;
        }

        public int countFolderRecursive(DirectoryView currentDirectory, int numberOfFolders)
        {
            if (currentDirectory.subDirectories.Count() == 0) return numberOfFolders;
            else
            {
                numberOfFolders += currentDirectory.subDirectories.Count();
                foreach (DirectoryView subDirectory in currentDirectory.subDirectories)
                {
                    numberOfFolders = countFolderRecursive(subDirectory, numberOfFolders);
                }
            }
            return numberOfFolders;
        }

        private int countFolders(DirectoryView directoryView)
        {
            int numberOfFolders = 0;
            numberOfFolders += directoryView.subDirectories.Count();

            foreach (DirectoryView subDirectory in directoryView.subDirectories)
            {
                numberOfFolders = countFolderRecursive(subDirectory, numberOfFolders);
            }

            return numberOfFolders;
        }

        private int countFilesRecursive(DirectoryView currentDirectory, int numberOfFiles)
        {
            if (currentDirectory.subFiles.Count() == 0) return numberOfFiles;
            else
            {
                numberOfFiles += currentDirectory.subFiles.Count();
                foreach (DirectoryView subDirectory in currentDirectory.subDirectories)
                {
                    numberOfFiles = countFilesRecursive(subDirectory, numberOfFiles);
                }
            }
            return numberOfFiles;
        }

        private int countFiles(DirectoryView directoryView)
        {
            int numberOfFiles = 0;
            numberOfFiles += directoryView.subFiles.Count();

            foreach (DirectoryView subDirectory in directoryView.subDirectories)
            {
                numberOfFiles = countFilesRecursive(subDirectory, numberOfFiles);
            }

            return numberOfFiles;
        }

        //private int countSizeRecursive(DirectoryView currentDirectory, int totalSize)
        //{
        //    if (currentDirectory.subFiles.Count() == 0) return totalSize;
        //    else
        //    {
        //        totalSize += currentDirectory.subFiles.Count();
        //        foreach (DirectoryView subDirectory in currentDirectory.subDirectories)
        //        {
        //            totalSize = countSizeRecursive(subDirectory, totalSize);
        //        }
        //    }
        //    return totalSize;
        //}

        //private int countSize(DirectoryView directoryView)
        //{
        //    long totalSize = 0;

        //    if(directoryView.subFiles.Count() != 0)
        //    {
        //        foreach (FileView subFile in directoryView.subFiles)
        //        {
        //            totalSize += subFile.fileInfo.Length;
        //        }
        //    }
        //    totalSize += directoryView.subFiles.Count();

        //    foreach (DirectoryView subDirectory in directoryView.subDirectories)
        //    {
        //        totalSize = countSizeRecursive(subDirectory, totalSize);
        //    }

        //    return totalSize;
        //}
    }
}