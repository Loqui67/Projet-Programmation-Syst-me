using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.developpement.Models
{
    public class FilesInfo
    {
        public string Name { get; }
        public string Path { get; }
        public long Size { get; }

        public FilesInfo(string name, string path, long size)
        {
            Name = name;
            Path = path;
            Size = size;
        }
    }
}
