using System;
using System.Collections.Generic;
using System.IO;

namespace SharedClass
{
    [Serializable]
    public partial class DirectoryView
    {
        public DirectoryInfo directoryInfo { get; set; }
        public List<DirectoryView> subDirectories { get; set; }
        public List<FileView> subFiles { get; set; }
        public DirectoryView(DirectoryInfo directoryInfo)
        {
            this.directoryInfo = directoryInfo;
            this.subDirectories = new List<DirectoryView>();
            this.subFiles = new List<FileView>();
        }

        public DirectoryView(string path)
        {
            this.directoryInfo = new DirectoryInfo(path);
            this.subDirectories = new List<DirectoryView>();
            this.subFiles = new List<FileView>();
        }

        public DirectoryView() { }

    }
}
