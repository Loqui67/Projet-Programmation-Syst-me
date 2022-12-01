using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Fichier servant pour les logs d'activités.
//File used for activity logs.
namespace Projet_Programmation_Système.developpement
{
    public class StateLog
    {
        public BackupJob backupJob { set; get; }
        public bool active { set; get; }
        public long fileSize { set; get; }
        public long fileNumber { set; get; }
        public long fileSizeLeft { set; get; }
        public long fileNumberLeft { set; get; }
    }
}
