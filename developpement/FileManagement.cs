using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Projet_Programmation_Système.developpement
{
    public static class FileManagement
    {
        
        private const string fileName = "SaveBackupJob.json";
        public static FileStream stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);

        public static void WriteFile(IList<BackupJob> value) {

            foreach (BackupJob job in value)
            {
                Console.WriteLine(job.name);
            }

            JsonSerializer.Serialize(stream, value);
        }
        
        public static IList<BackupJob>? ReadBackupJobFile()
        {
            return JsonSerializer.Deserialize<IList<BackupJob>>(File.ReadAllText(fileName));
        }   
    }
}
