using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Projet_Programmation_Système.developpement
{
    public static class JsonFileManager
    {
        private const string backupJobFileName = "SaveBackupJob.json";
        private static FileStream backupJobStream = OpenFileStream(backupJobFileName);
        private static FileStream dailyLogStream = OpenFileStream(GetDailyFileName());
        
        public static FileStream CreateJsonFileIfNotExist()
        {
            FileStream stream = OpenFileStream(backupJobFileName);
            
            if (!File.Exists(backupJobFileName)) 
            {
                IList<BackupJob> backupJobs = new List<BackupJob>();
                File.Create(backupJobFileName);
                for (int i = 1; i < 6; i++)
                {
                    backupJobs.Add(new BackupJob{ id = i.ToString() });
                }
                WriteBackupJobToFile(backupJobs);
            }
            return stream;
        }
        
        public static void WriteBackupJobToFile(IList<BackupJob> backupJobs) {
            File.WriteAllText(backupJobFileName, JsonSerializer.Serialize(backupJobs));
        }
        
        public static IList<BackupJob>? ReadBackupJobFile()
        {
            return JsonSerializer.Deserialize<IList<BackupJob>>(File.ReadAllText(backupJobFileName));
        }

        public static void WriteDailyLogToFile(Log dailyLog)
        {
                IList<Log>? logs;
                logs = ReadDailyLogFile();
                logs.Add(dailyLog);
                File.WriteAllText(GetDailyFileName(), JsonSerializer.Serialize(logs));
        }

        public static IList<Log>? ReadDailyLogFile()
        {
            return JsonSerializer.Deserialize<IList<Log>>(File.ReadAllText(GetDailyFileName()));
        }

        private static FileStream OpenFileStream(string fileName)
        {
            return new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }

        private static string GetDailyFileName()
        {
            return "logs/" + DateTime.Now.ToString("yyyy-MM-dd") + "-log.json";
        }
    }
}
