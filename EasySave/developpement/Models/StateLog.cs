//Fichier servant pour les logs d'activités.
//File used for activity logs.
namespace EasySave.developpement.Models
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
