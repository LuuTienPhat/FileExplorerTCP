using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SharedClass
{
    [Serializable]
    public partial class FileView
    {
        public FileInfo fileInfo { get; set; }
        public string data { get; set; }
        public FileView(FileInfo fileInfo, string data)
        {
            this.fileInfo = fileInfo;
            this.data = data;
        }
        public FileView(string path, string data)
        {
            this.fileInfo = new FileInfo(path);
            this.data = data;
        }
        public FileView() { }
    }
}
