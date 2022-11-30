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
        private const string activeStateFileName = "activeState.json";
        private static JsonSerializerOptions optionsWriteIndented = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        private static void CreateJsonBackupJobFileIfNotExist()
        {
            if (!File.Exists(backupJobFileName)) 
            {
                List<BackupJob> backupJobs = new List<BackupJob>();
                using (FileStream fs = File.Create(backupJobFileName))
                {
                    for (int i = 1; i < 6; i++)
                    {
                        backupJobs.Add(new BackupJob { id = i.ToString(), name = "" });
                    }
                }
                WriteBackupJobToFile(backupJobs);
            }
        }
        
        public static void WriteBackupJobToFile(List<BackupJob> backupJobs) {
            if (File.Exists(backupJobFileName))
            {
                using (FileStream fs = File.OpenWrite(backupJobFileName))
                {
                    string jsonString = JsonSerializer.Serialize(backupJobs, optionsWriteIndented);
                    byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonString);
                    fs.Write(jsonBytes, 0, jsonBytes.Length);
                }
            }
        }
        
        public static List<BackupJob>? ReadBackupJobFile()
        {
            CreateJsonBackupJobFileIfNotExist();
            using (FileStream fs = File.OpenRead(backupJobFileName))
            {
                byte[] jsonBytes = new byte[fs.Length];
                fs.Read(jsonBytes, 0, jsonBytes.Length);
                string jsonString = Encoding.UTF8.GetString(jsonBytes);
                return JsonSerializer.Deserialize<List<BackupJob>>(jsonString);
            }
        }

        public static void WriteDailyLogToFile(Log dailyLog)
        {
            if (!Directory.Exists("logs")) Directory.CreateDirectory("logs");

            List<Log>? logs;
            logs = ReadDailyLogFile();
            logs.Add(dailyLog);
            
            using (FileStream fs = File.Create(GetDailyFileName()))
            {

                string jsonString = JsonSerializer.Serialize(logs, optionsWriteIndented);
                byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonString);
                fs.Write(jsonBytes, 0, jsonBytes.Length);
            }
        }

        public static List<Log>? ReadDailyLogFile()
        {
            if (File.Exists(GetDailyFileName())) {
                using (FileStream fs = File.OpenRead(GetDailyFileName()))
                {
                    byte[] jsonBytes = new byte[fs.Length];
                    fs.Read(jsonBytes, 0, jsonBytes.Length);
                    string jsonString = Encoding.UTF8.GetString(jsonBytes);
                    return JsonSerializer.Deserialize<List<Log>>(jsonString);
                }
            }
            return new List<Log>();
        }

        public async static Task WriteStateLog(StateLog stateLog)
        {
            List<StateLog> logs = ReadStateLog();

            if(logs != new List<StateLog>())
            {
                foreach (StateLog log in logs.ToList())
                {
                    if (log.backupJob.id == stateLog.backupJob.id)
                    {
                        logs.Remove(log);
                    }
                }
            }

            logs.Add(stateLog);



            using (FileStream fs = File.Create(activeStateFileName))
            {
                string jsonString = JsonSerializer.Serialize(logs, optionsWriteIndented);
                byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonString);
                await fs.WriteAsync(jsonBytes, 0, jsonBytes.Length);
            }
        }

        public static List<StateLog>? ReadStateLog()
        {
            if (File.Exists(activeStateFileName))
            {
                using (FileStream fs = File.OpenRead(activeStateFileName))
                {
                    try
                    {
                        byte[] jsonBytes = new byte[fs.Length];
                        fs.Read(jsonBytes, 0, jsonBytes.Length);
                        string jsonString = Encoding.UTF8.GetString(jsonBytes);
                        return JsonSerializer.Deserialize<List<StateLog>>(jsonString);
                    }
                    catch (Exception)
                    {
                        return new List<StateLog>();
                    }
                }
            }
            return new List<StateLog>();
        }

        private static string GetDailyFileName()
        {
            return "logs/" + DateTime.Now.ToString("yyyy-MM-dd") + "-log.json";
        }
    }
}
