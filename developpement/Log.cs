using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Programmation_Système.developpement
{
    public class Log
    {
        public string name { set; get; }
        public string sourcePath { set; get; }
        public string destinationPath { set; get; }
        public string type { set; get; }
        public string fileSize { set; get; }
        public string fileTransferTime { set; get; }
        public DateTime date { set; get; }
    }
}
