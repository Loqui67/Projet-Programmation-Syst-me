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

        public static BackupJob GetBackupJob(string id)
        {
            foreach (BackupJob backupJob in backupJobs)
            {
                if (backupJob.id == id)
                {
                    return backupJob;
                }
            }
            return new BackupJob();
        }



        public static void DisplayBackupJobs()
        {
            foreach (BackupJob backupJob in backupJobs)
            {
                if (backupJob.name == "")
                {
                    ConsoleManager.DisplayArrayInLine(new string[] { backupJob.id, " - " });
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
            try {
                if (backupJobs != null) return false;
                ConsoleManager.DisplayLanguage("NoBackupJobError");
                return true;
            } catch(Exception) {
                return true;
            }
        }

        public static BackupJob CreateBackupJob()
        {
            BackupJob? backupJob = GetBackupJob(AskForId());
            backupJob.name = AskForNameNotEmpty();
            backupJob.sourcePath = AskForSourcePathNotEmpty();
            backupJob.destinationPath = AskForDestinationPathNotEmpty();
            backupJob.type = AskForTypeNotEmpty();
            return backupJob;
        }

        public static BackupJob ModifyBackupJob()
        {
            BackupJob? backupJob = GetBackupJob(AskForId());

            string name = AskForNameModification();
            if (name != "") backupJob.name = name;
            
            string sourcePath = AskForSourcePathModification();
            if (sourcePath != "") backupJob.sourcePath = sourcePath;
            
            string destinationPath = AskForDestinationPathModification();
            if (destinationPath != "") backupJob.destinationPath = destinationPath;
            
            string type = AskForTypeModification();
            if (type != "") backupJob.type = type;

            return backupJob;
        }
        
        public static BackupJob DeleteBackupJob()
        {
            BackupJob? backupJob = GetBackupJob(AskForId());
            backupJob.name = "";
            backupJob.sourcePath = "";
            backupJob.destinationPath = "";
            backupJob.type = "";
            return backupJob;
        }

        public static bool AssertThatPathExist(string path)
        {
            return System.IO.Directory.Exists(path);
        }

        public static string AskForId()
        {
            ConsoleManager.DisplayLanguage("Id");
            string input = ConsoleManager.GetInputNotEmpty();
            if (Int32.TryParse(input, out int x) && x < 6 && x > 0)
            {
                return input;
            }
            ConsoleManager.DisplayLanguage("InvalidInput");
            return AskForId();
        }



        //probablement beaucoup d'amélioration contre la redondance de code possible



        public static string AskForNameModification()
        {
            ConsoleManager.DisplayLanguage("ModifyName");
            return ConsoleManager.GetInput();
        }

        public static string AskForNameNotEmpty()
        {
            ConsoleManager.DisplayLanguage("Name");
            return ConsoleManager.GetInputNotEmpty();
        }

        public static string AskForSourcePathModification()
        {
            ConsoleManager.DisplayLanguage("ModifySourcePath");
            string input = ConsoleManager.GetInput();
            if (AssertThatPathExist(input)) return input;
            ConsoleManager.DisplayLanguage("PathDoesntExist");
            return AskForSourcePathModification();
        }

        public static string AskForSourcePathNotEmpty()
        {
            ConsoleManager.DisplayLanguage("SourcePath");
            string input = ConsoleManager.GetInput();
            if (AssertThatPathExist(input)) return input;
            ConsoleManager.DisplayLanguage("PathDoesntExist");
            return AskForSourcePathNotEmpty();
        }

        public static string AskForDestinationPathModification()
        {
            ConsoleManager.DisplayLanguage("ModifyDestinationPath");
            string input = ConsoleManager.GetInput();
            if (AssertThatPathExist(input)) return input;
            ConsoleManager.DisplayLanguage("PathDoesntExist");
            return AskForDestinationPathModification();
        }

        public static string AskForDestinationPathNotEmpty()
        {
            ConsoleManager.DisplayLanguage("DestinationPath");
            string input = ConsoleManager.GetInput();
            if (AssertThatPathExist(input)) return input;
            ConsoleManager.DisplayLanguage("PathDoesntExist");
            return AskForDestinationPathNotEmpty();
        }

        public static string AskForTypeModification()
        {
            ConsoleManager.DisplayLanguage("ModifyType");
            string input = ConsoleManager.GetInput();
            if (Int32.TryParse(input, out int x) && (x == 1 || x == 2))
            {
                return input;
            }
            ConsoleManager.DisplayLanguage("InvalidInput");
            return AskForTypeModification();
        }

        public static string AskForTypeNotEmpty()
        {
            ConsoleManager.DisplayLanguage("Type");
            string input = ConsoleManager.GetInput();
            if (Int32.TryParse(input, out int x) && (x == 1 || x == 2))
            {
                return input;
            }
            ConsoleManager.DisplayLanguage("InvalidInput");
            return AskForTypeNotEmpty();
        }
    }
}
