using System;
using System.Collections.Generic;

namespace SharedClass
{
    [Serializable]
    public class Dir
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public List<Dir> SubDirectories { get; set; }
        public List<FileDir> SubFiles { get; set; }

        public Dir(string name, string path)
        {
            this.Name = name;
            this.Path = path;
            SubFiles = new List<FileDir>();
            SubDirectories = new List<Dir>();
        }

        public Dir() { }

    }
}
