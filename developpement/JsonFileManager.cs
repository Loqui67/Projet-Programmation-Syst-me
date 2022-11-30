using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Projet_Programmation_Système.developpement
{

    //Création d'une classe pour gérer les fichiers Json.
    //Creation of a class to manage Json files.
    public static class JsonFileManager
    {
        private const string backupJobFileName = "SaveBackupJob.json";
        private const string activeStateFileName = "activeState.json";
        private static JsonSerializerOptions optionsWriteIndented = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        //Création d'une méthode pour sauvegarder les données d'un travaux de sauvegarde dans un fichier Json.
        //Creation of a method to save the data of a backup job in a Json file.
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

        //Création d'une méthode pour sauvegarder les données d'un travaux de sauvegarde dans un fichier.
        //Create a method to save data from a backup job to a file.
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

        //Création d'une méthode pour lire les données d'un travaux de sauvegarde dans un fichier.
        //Create a method to read data from a backup job into a file.
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

        //Création d'une méthode pour sauvegarder les logs de maniere journaliere.
        //Creation of a method to save logs on a daily basis.
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

        //Méthode qui permet de lire les logs journaliers.
        //Method to read daily logs.
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

        //Créaton d'une méthode qui permet d'écrire l'état des logs.
        //Creation of a method that allows to write the state of the logs.
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

        //Création d'une méthode qui permet de lire l'état des logs.
        //Creation of a method to read the state of the logs.
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

        //Création d'une méthode qui permet d'obtenir le nom du fichier où sont sauvegardé les logs journalier.
        //Creation of a method to obtain the name of the file where the daily logs are saved.
        private static string GetDailyFileName()
        {
            return "logs/" + DateTime.Now.ToString("yyyy-MM-dd") + "-log.json";
        }
    }
}
