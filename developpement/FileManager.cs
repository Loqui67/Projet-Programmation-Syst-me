using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using AppWPF.developpement;

namespace Projet_Programmation_Système.developpement
{
    public static class FileManager
    {
        private static readonly string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static readonly string appDataFolder = "Projet Programmation Système";
        private static readonly string appDataFolderPath = CreateFolderIfNotExistAndReturnString(Path.Combine(appData, appDataFolder));
        private static readonly string backupJobJsonFileName = Path.Combine(appDataFolderPath, "SaveBackupJob.json");
        private static readonly string activeStateFileName = Path.Combine(appDataFolderPath, "activeState.json");
        private static readonly JsonSerializerOptions optionsWriteIndented = new()
        {
            WriteIndented = true
        };

        //creer un dossier s'il n'existe pas
        //create a folder if it doesn't exist
        private static string CreateFolderIfNotExistAndReturnString(string path)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            return path;
        }

        private static string CreateFileIfNotExist(string path)
        {
            if (!File.Exists(path)) File.Create(path);
            return path;
        }

        //ecrit dans le fichier les différents travaux de sauvegarde
        //write in the file the different backup jobs
        public static void WriteBackupJobToFile(List<BackupJob> backupJobs)
        {
            if (File.Exists(backupJobJsonFileName))
            {
                using FileStream fs = new(backupJobJsonFileName, FileMode.Truncate);
                string jsonString = JsonSerializer.Serialize(backupJobs, optionsWriteIndented);
                byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonString);
                fs.Write(jsonBytes, 0, jsonBytes.Length);
            }
        }

        //lit le fichier des différents travaux de sauvegarde
        //read the file of the different backup jobs
        public static List<BackupJob>? ReadBackupJobFile()
        {
            CreateFileIfNotExist(backupJobJsonFileName);
            try
            {
                using FileStream fs = File.OpenRead(backupJobJsonFileName);
                byte[] jsonBytes = new byte[fs.Length];
                fs.Read(jsonBytes, 0, jsonBytes.Length);
                string jsonString = Encoding.UTF8.GetString(jsonBytes);
                return JsonSerializer.Deserialize<List<BackupJob>>(jsonString);
            } catch (Exception)
            {
                return new List<BackupJob>();
            }
        }

        //ecrit dans le fichier les logs journalières
        //write in the file the daily logs
        public static void WriteDailyLogToFile(Log dailyLog)
        {
            string path = CreateFolderIfNotExistAndReturnString(Path.Combine(appDataFolderPath, "logs"));
            List<Log>? logs;
            logs = ReadDailyLogFile(path);
            logs.Add(dailyLog);

            using FileStream fs = File.Create(Path.Combine(path, GetDailyFileName() + ".json"));
            string jsonString = JsonSerializer.Serialize(logs, optionsWriteIndented);
            byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonString);
            fs.Write(jsonBytes, 0, jsonBytes.Length);
        }

        //lit le fichier des logs journalières
        //read the file of the daily logs
        public static List<Log>? ReadDailyLogFile(string path)
        {
            string filePath = Path.Combine(path, GetDailyFileName() + ".json");
            if (File.Exists(filePath))
            {
                using FileStream fs = File.OpenRead(filePath);
                byte[] jsonBytes = new byte[fs.Length];
                fs.Read(jsonBytes, 0, jsonBytes.Length);
                string jsonString = Encoding.UTF8.GetString(jsonBytes);
                return JsonSerializer.Deserialize<List<Log>>(jsonString);
            }
            return new List<Log>();
        }


        public static void SerializeToXML(Log log)
        {
            string path = CreateFolderIfNotExistAndReturnString(Path.Combine(appDataFolderPath, "logs"));
            List<Log> logs = DeserializeFromXML(path);
            logs.Add(log);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Log>));
            TextWriter writer = new StreamWriter(Path.Combine(path, GetDailyFileName() + ".xml"));
            serializer.Serialize(writer, logs);
            writer.Close();
        }

        public static List<Log> DeserializeFromXML(string path)
        {
            if (File.Exists(GetDailyFileName() + ".xml"))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(List<Log>));
                TextReader reader = new StreamReader(Path.Combine(path, GetDailyFileName() + ".xml"));
                object? obj = deserializer.Deserialize(reader);
                List<Log>? logs = (List<Log>?)obj;
                reader.Close();
                if (logs == null) return new List<Log>();
                return logs;
            }
            else
            {
                File.Create(GetDailyFileName() + ".json");
                return new List<Log>();
            }
        }

        //ecrit le fichier des logs d'activités
        //write in the file the activity logs
        public async static Task WriteStateLog(StateLog stateLog)
        {
            List<StateLog> logs = ReadStateLog();

            if (logs != new List<StateLog>())
            {
                foreach (StateLog log in logs.ToList())
                {
                    if (log.backupJob.name == stateLog.backupJob.name)
                    {
                        logs.Remove(log);
                    }
                }
            }

            logs.Add(stateLog);



            using FileStream fs = File.Create(activeStateFileName);
            string jsonString = JsonSerializer.Serialize(logs, optionsWriteIndented);
            byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonString);
            await fs.WriteAsync(jsonBytes);
        }

        //lit le fichier des logs d'activités
        //read the file of the activity logs
        public static List<StateLog>? ReadStateLog()
        {
            if (File.Exists(activeStateFileName))
            {
                using FileStream fs = File.OpenRead(activeStateFileName);
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
            return new List<StateLog>();
        }

        //retourne le nom du fichier journalier
        //return the name of the daily file
        private static string GetDailyFileName()
        {
            return DateTime.Now.ToString("yyyy-MM-dd") + "-log";
        }
    }
}
