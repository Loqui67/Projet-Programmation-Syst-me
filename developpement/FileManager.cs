using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Projet_Programmation_Système.developpement
{
    public static class FileManager
    {
        private static string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static string appDataFolder = "Projet Programmation Système";
        private static string appDataFolderPath = createFolderIfNotExistAndReturnString(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), appDataFolder));
        private static string backupJobJsonFileName = Path.Combine(appDataFolderPath, "SaveBackupJob.json");
        private static string activeStateFileName = Path.Combine(appDataFolderPath, "activeState.json");
        private static JsonSerializerOptions optionsWriteIndented = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        private static string createFolderIfNotExistAndReturnString(string path)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            return path;
        }

        private static void CreateJsonBackupJobFileIfNotExist() // COMMENTER// COMMENTER// COMMENTER// COMMENTER// COMMENTER// COMMENTER// COMMENTER// COMMENTER// COMMENTER// COMMENTER// COMMENTER// COMMENTER// COMMENTER et tester en sup dans appdata 
        {
            if (!File.Exists(backupJobJsonFileName))
            {
                List<BackupJob> backupJobs = new List<BackupJob>();
                using (FileStream fs = File.Create(backupJobJsonFileName))
                {
                    for (int i = 1; i < 6; i++)
                    {
                        backupJobs.Add(new BackupJob { id = i.ToString(), name = "" });
                    }
                }
                WriteBackupJobToFile(backupJobs);
            }
        }

        public static void WriteBackupJobToFile(List<BackupJob> backupJobs)
        {
            if (File.Exists(backupJobJsonFileName))
            {
                using (FileStream fs = File.OpenWrite(backupJobJsonFileName))
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
            using (FileStream fs = File.OpenRead(backupJobJsonFileName))
            {
                byte[] jsonBytes = new byte[fs.Length];
                fs.Read(jsonBytes, 0, jsonBytes.Length);
                string jsonString = Encoding.UTF8.GetString(jsonBytes);
                return JsonSerializer.Deserialize<List<BackupJob>>(jsonString);
            }
        }

        public static void WriteDailyLogToFile(Log dailyLog)
        {
            string path = createFolderIfNotExistAndReturnString(Path.Combine(appDataFolderPath, "logs"));
            List<Log>? logs;
            logs = ReadDailyLogFile(path);
            logs.Add(dailyLog);

            using (FileStream fs = File.Create(Path.Combine(path, GetDailyFileName() + ".json")))
            {
                string jsonString = JsonSerializer.Serialize(logs, optionsWriteIndented);
                byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonString);
                fs.Write(jsonBytes, 0, jsonBytes.Length);
            }
        }

        public static List<Log>? ReadDailyLogFile(string path)
        {
            string filePath = Path.Combine(path, GetDailyFileName() + ".json");
            if (File.Exists(filePath))
            {
                using (FileStream fs = File.OpenRead(filePath))
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

            if (logs != new List<StateLog>())
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
            return DateTime.Now.ToString("yyyy-MM-dd") + "-log";
        }
    }
}
