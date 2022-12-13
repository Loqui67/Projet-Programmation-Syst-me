using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace AppWPF.developpement.Models
{
    ///Création d'une classe qui permet de gérer les fichiers
    ///Create a class to manage files
    public static class FileManager
    {
        private static readonly string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static readonly string appDataFolder = "Projet Programmation Système";
        private static readonly string appDataFolderPath = CreateFolderIfNotExistAndReturnString(System.IO.Path.Combine(appData, appDataFolder));
        private static readonly string backupJobJsonFileName = System.IO.Path.Combine(appDataFolderPath, "SaveBackupJob.json");
        private static readonly string activeStateFileName = System.IO.Path.Combine(appDataFolderPath, "activeState.json");
        private static readonly string configFileName = System.IO.Path.Combine(appDataFolderPath, "config.json");
        private static readonly string dailyLogsFolder = System.IO.Path.Combine(appDataFolderPath, "logs");
        private static readonly JsonSerializerOptions optionsWriteIndented = new JsonSerializerOptions()
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

        //Méthode qui permet de créer un fichier si il n'éxiste pas déja 
        //Method that allows to create a file if it does not already exist
        private static bool CreateFileIfNotExist(string path)
        {
            if (File.Exists(path)) return true;
            File.Create(path);
            return false;
        }

        ///Méthode qui permet de charger une configuration déja éxistante
        ///Method that allows to load an already existing configuration
        public static Config LoadConfig()
        {
            try
            {
                if (!CreateFileIfNotExist(configFileName)) return new Config { DefaultLanguage = "en", LogExtension = "0", 
                    AllProcessus = new List<Processus>(), AllExtensionCryptage = new List<ExtensionCryptage>(), 
                    AllExtensionPriority = new List<ExtensionPriority>() };
                string json = File.ReadAllText(configFileName);
                return JsonSerializer.Deserialize<Config>(json);
            }
            catch
            {
                return new Config { DefaultLanguage = "en", LogExtension = "0", AllProcessus = new List<Processus>(), 
                    AllExtensionCryptage = new List<ExtensionCryptage>(), AllExtensionPriority = new List<ExtensionPriority>() };
            }
        }

        ///Méthode qui permet de sauvegarder une configuration
        ///Method to save a configuration
        public static void SaveConfig(Config config)
        {
            try
            {
                CreateFileIfNotExist(configFileName);
                string json = JsonSerializer.Serialize(config, optionsWriteIndented);
                File.WriteAllText(configFileName, json);
            }
            catch (Exception) { }
        }

        //ecrit dans le fichier les différents travaux de sauvegarde
        //write in the file the different backup jobs
        public static async Task WriteBackupJobToFile(List<BackupJob> backupJobs)
        {
            CreateFileIfNotExist(backupJobJsonFileName);
            using FileStream fs = new FileStream(backupJobJsonFileName, FileMode.Truncate);
            string jsonString = JsonSerializer.Serialize(backupJobs, optionsWriteIndented);
            byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonString);
            await fs.WriteAsync(jsonBytes);
        }

        ///Méthode qui permet d'ajouter un travail de sauvegarde à un fichier 
        ///Method to add a backup job to a file
        public static async Task AddBackupJobToFile(BackupJob backupJob)
        {
            List<BackupJob>? backupJobs = ReadBackupJobFile();
            if (backupJobs == null) backupJobs = new List<BackupJob>();
            backupJobs.Add(backupJob);
            await WriteBackupJobToFile(backupJobs);
        }

        ///Méthode qui permet de supprimer un travail de sauvegarde qui est stocket dans un fichier
        ///Method to delete a backup job that is stored in a file
        public static async Task RemoveBackupJobFromFile(Guid backupJobId)
        {
            List<BackupJob>? backupJobs = ReadBackupJobFile();
            if (backupJobs == null) return;
            backupJobs.Remove(backupJobs.Find(x => x.Id == backupJobId));
            await WriteBackupJobToFile(backupJobs);
        }

        ///Méthode qui permet d'enlever tous les travaux de sauvegarde d'un fichier
        ///Method to remove all backup jobs from a file
        public static async Task RemoveAllBackupJobFromFile()
        {
            await WriteBackupJobToFile(new List<BackupJob>());
        }

        ///Méthode qui permet de mettre à jour un travail de sauvegarde dans un fichier
        ///Method to update a backup job to a file
        public static async Task UpdateBackupJobInFile(BackupJob backupJob)
        {
            List<BackupJob>? backupJobs = ReadBackupJobFile();
            if (backupJobs == null) return;
            backupJobs[backupJobs.FindIndex(x => x.Id == backupJob.Id)] = backupJob;
            await WriteBackupJobToFile(backupJobs);
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
            }
            catch (Exception)
            {
                return new List<BackupJob>();
            }
        }

        //ecrit dans le fichier les logs journalières
        //write in the file the daily logs
        public static void WriteDailyLogJson(List<Log> dailyLog)
        {
            string path = CreateFolderIfNotExistAndReturnString(System.IO.Path.Combine(appDataFolderPath, "logs"));
            List<Log>? logs;
            logs = ReadDailyLogJson(path);
            foreach (Log log in dailyLog)
            {
                logs.Add(log);
            }
            using FileStream fs = File.Create(System.IO.Path.Combine(path, GetDailyFileName() + ".json"));
            string jsonString = JsonSerializer.Serialize(logs, optionsWriteIndented);
            byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonString);
            fs.Write(jsonBytes, 0, jsonBytes.Length);
        }

        //lit le fichier des logs journalières
        //read the file of the daily logs
        public static List<Log>? ReadDailyLogJson(string path)
        {
            string filePath = System.IO.Path.Combine(path, GetDailyFileName() + ".json");
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



        public static void WriteDailyLogXml(List<Log> dailyLog)
        {
            string logsFilePath = CreateFolderIfNotExistAndReturnString(System.IO.Path.Combine(dailyLogsFolder, GetDailyFileName() + ".xml"));
            List<Log>? logs;
            logs = ReadDailyLogXml(logsFilePath);
            foreach (Log log in dailyLog)
            {
                logs.Add(log);
            }
            XmlSerializer serializer = new XmlSerializer(typeof(List<Log>));
            TextWriter writer = new StreamWriter(logsFilePath);
            serializer.Serialize(writer, logs);
            writer.Close();
        }


        public static List<Log>? ReadDailyLogXml(string logsFilePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(List<Log>));
            TextReader reader = new StreamReader(logsFilePath);
            object? obj = deserializer.Deserialize(reader);
            List<Log>? logs = (List<Log>?)obj;
            reader.Close();
            if (logs == null) return new List<Log>();
            return logs;
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
                    if (log.backupJob.Name == stateLog.backupJob.Name)
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
