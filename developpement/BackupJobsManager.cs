using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Projet_Programmation_Système.developpement.ConsoleManager;

namespace Projet_Programmation_Système.developpement
{
    public static class BackupJobsManager
    {
        public static List<BackupJob>? backupJobs = GetBackupJobs();
        
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
            JsonFileManager.WriteBackupJobToFile(backupJobs);
        }

        public static List<BackupJob>? GetBackupJobs()
        {
            return JsonFileManager.ReadBackupJobFile();
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
                    DisplayArrayInLine(new string[] { backupJob.id, " - " });
                    DisplayLanguage("Empty");
                } else {
                    DisplayArrayInLine(new string[] { backupJob.id, " - ", backupJob.name, " / ", backupJob.type });
                    DisplayEmptyLine();
                }
            }
            DisplayEmptyLine();
        }

        public static bool IsBackupJobsNull()
        {
            try {
                if (backupJobs != null) return false;
                DisplayLanguage("NoBackupJobError");
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

        public static void LaunchSave()
        {
            string id = AskForId();
            BackupJob? backupJob = GetBackupJob(id);
            backupJob.Save();
        }

        public static void LaunchAllSave()
        {
            foreach (BackupJob backupJob in backupJobs)
            {
                if (backupJob.name != "")
                {
                    backupJob.Save();
                }
            }
        }

        public static bool AssertThatPathExist(string path)
        {
            return System.IO.Directory.Exists(path);
        }

        

        public static string AskForId()
        {
            DisplayLanguage("Id");
            string input = GetInputNotEmpty();
            if (int.TryParse(input, out int x) && x < 6 && x > 0)
            {
                return input;
            }
            DisplayLanguage("InvalidInput");
            return AskForId();
        }



        //probablement beaucoup d'améliorations contre la redondance de code possibles
        

        public static string AskPathAndCheckIfExist(Func<string> MethodName, bool emptyAccepted)
        {
            string input = GetInput();
            if (AssertThatPathExist(input) || (input == "" && emptyAccepted)) return input;
            DisplayLanguage("PathDoesntExist");
            return MethodName();
        }

        public static string AskTypeAndCheck(Func<string> MethodName, bool emptyAccepted)
        {
            string input = GetInput();
            if ((int.TryParse(input, out int x) && (x == 1 || x == 2)) || (input == "" && emptyAccepted))
            {
                return input;
            }
            DisplayLanguage("InvalidInput");
            return MethodName();
        }

        public static string AskForNameModification()
        {
            DisplayLanguage("ModifyName");
            return GetInput();
        }

        public static string AskForNameNotEmpty()
        {
            DisplayLanguage("Name");
            return GetInputNotEmpty();
        }

        public static string AskForSourcePathModification()
        {
            DisplayLanguage("ModifySourcePath");
            return AskPathAndCheckIfExist(AskForSourcePathModification, true);
        }
        
        public static string AskForSourcePathNotEmpty()
        {
            DisplayLanguage("SourcePath");
            return AskPathAndCheckIfExist(AskForSourcePathNotEmpty, false);
        }

        public static string AskForDestinationPathModification()
        {
            DisplayLanguage("ModifyDestinationPath");
            return AskPathAndCheckIfExist(AskForDestinationPathModification, true);
        }

        public static string AskForDestinationPathNotEmpty()
        {
            DisplayLanguage("DestinationPath");
            return AskPathAndCheckIfExist(AskForDestinationPathNotEmpty, false);
        }

        public static string AskForTypeModification()
        {
            DisplayLanguage("ModifyType");
            return AskTypeAndCheck(AskForTypeModification, true);
        }

        public static string AskForTypeNotEmpty()
        {
            DisplayLanguage("Type");
            return AskTypeAndCheck(AskForTypeNotEmpty, false);
        }
    }
}
