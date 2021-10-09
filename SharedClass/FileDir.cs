using System;
using System.Collections.Generic;
using System.Text;

namespace SharedClass
{
    [Serializable]
    public class FileDir
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public FileDir(string name, string path)
        {
            this.Name = name;
            this.Path = path;
        }

        public FileDir() { }
    }
}
