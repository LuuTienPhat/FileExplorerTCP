using System;
using System.Collections.Generic;

namespace MySharedClass
{
    [Serializable]
    public partial class Dir
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public List<Dir> SubDirectories { get; set; }
        public List<FileDir> SubFiles { get; set; }

        public Dir(string name, string path)
        {
            Name = name;
            Path = path;
            SubFiles = new List<FileDir>();
            SubDirectories = new List<Dir>();
        }

        public Dir() { }

    }

    [Serializable]
    public partial class FileDir
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public FileDir(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public FileDir() { }
    }
}
