using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Projet_Programmation_Système.developpement
{
    public static class JsonBackupJobFileManager
    {
        private const string fileName = "SaveBackupJob.json";
        public static FileStream stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);

        public static void WriteFile(IList<BackupJob> backupJobs) {
            File.WriteAllText(fileName, JsonSerializer.Serialize(backupJobs));
        }
        
        public static IList<BackupJob>? ReadBackupJobFile()
        {
            return JsonSerializer.Deserialize<IList<BackupJob>>(File.ReadAllText(fileName));
        }   
    }
}
