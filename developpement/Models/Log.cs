using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Fichier servant à la sauvegarde des logs(BakcupJob).
//File used to save logs(BakcupJob).
namespace AppWPF.developpement.Models
{
    public class Log
    {
        public string name { set; get; }
        public string sourcePath { set; get; }
        public string destinationPath { set; get; }
        public string type { set; get; }
        public string fileSize { set; get; }
        public string fileTransferTime { set; get; }
        public string date { set; get; }
    }
}
