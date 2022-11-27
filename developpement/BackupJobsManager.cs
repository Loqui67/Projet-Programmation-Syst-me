using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Projet_Programmation_Système.developpement
{
    public static class BackupJobsManager
    {
        public static IList<BackupJob>? backupJobs = GetBackupJobs();
        
        public static void WriteBackupJob(BackupJob backupJob)
        {
            if (IsBackupJobsNull()) return;
            foreach (BackupJob job in backupJobs)
            {
                if (job.id == backupJob.id)
                {
                    job.name = backupJob.name;
                    job.sourcePath = backupJob.sourcePath;
                    job.destinationPath = backupJob.destinationPath;
                    job.type = backupJob.type;
                    break;
                }
            }
            JsonBackupJobFileManager.WriteFile(backupJobs);
        }

        public static IList<BackupJob>? GetBackupJobs()
        {
            return JsonBackupJobFileManager.ReadBackupJobFile();
        }

        public static void DisplayBackupJobs()
        {
            if (IsBackupJobsNull()) return;
            foreach (BackupJob backupJob in backupJobs)
            {
                if (backupJob.name == "")
                {
                    ConsoleManager.DisplayLanguage("Empty");
                } else {
                    ConsoleManager.DisplayArrayInLine(new string[] { backupJob.id, " - ", backupJob.name, " / ", backupJob.type });
                    ConsoleManager.DisplayEmptyLine();
                }
            }
            ConsoleManager.DisplayEmptyLine();
        }

        public static IList<BackupJob>? GetConfigureBackupJob()
        {
            {
                IList<BackupJob> configureBackupJobs = new List<BackupJob>();

                if (IsBackupJobsNull()) return null;

                foreach (BackupJob backupJob in backupJobs)
                {
                    if (backupJob.name != "")
                    {
                        configureBackupJobs.Add(backupJob);
                    }
                }
                return configureBackupJobs;
            }
        }

        public static bool IsBackupJobsNull()
        {
            if (backupJobs == null)
            {
                ConsoleManager.DisplayLanguage("NoBackupJobError");
                return true;
            }
            return false;
        }



        public static BackupJob CreateBackupJob()
        {
            return new BackupJob
            {
                id = AskForId(),
                name = AskForName(),
                sourcePath = AskForSourcePath(),
                destinationPath = AskForDestinationPath(),
                type = AskForType(),
            };
        }

        public static string AskForId()
        {
            ConsoleManager.DisplayLanguage("Id");
            string input = ConsoleManager.GetInputNotNull();
            if (Int32.TryParse(input, out int x) && x < 6 && x > 0)
            {
                return input;
            }
            ConsoleManager.DisplayLanguage("InvalidInput");
            return AskForId();
        }

        public static string AskForName()
        {
            ConsoleManager.DisplayLanguage("Name");
            return ConsoleManager.GetInputNotNull();
        }

        public static string AskForSourcePath()
        {
            ConsoleManager.DisplayLanguage("SourcePath");
            return ConsoleManager.GetInputNotNull();
        }

        public static string AskForDestinationPath()
        {
            ConsoleManager.DisplayLanguage("DestinationPath");
            return ConsoleManager.GetInputNotNull();
        }

        public static string AskForType()
        {
            ConsoleManager.DisplayLanguage("Type");
            string input = ConsoleManager.GetInputNotNull();
            if (Int32.TryParse(input, out int x) && (x == 1 || x == 2))
            {
                return input;
            }
            ConsoleManager.DisplayLanguage("InvalidInput");
            return AskForType();
        }
    }
}
