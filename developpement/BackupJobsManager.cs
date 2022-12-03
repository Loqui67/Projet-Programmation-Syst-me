using AppWPF.developpement;
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
    //Création d'une classe qui permet de modifier, créer, supprimer des travaux de sauvegarde.
    //Creating a class that allows modifying, creating, deleting backup jobs.
    public static class BackupJobsManager
    {
        //Création d'une liste de sauvegarde.
        //Creating a backup list.
        public static List<BackupJob>? backupJobs = GetBackupJobs();
        //Création d'une méthode qui permet d'écrire ce que font les travaux de sauvegardes dans le fichier json.
        //Created a method that writes what the backup jobs do in the json file.
        public static void WriteBackupJob()
        {
            FileManager.WriteBackupJobToFile(backupJobs);
        }


        public static List<BackupJob>? GetBackupJobs()
        {
            return FileManager.ReadBackupJobFile();
        }

        //Création d'une méthode qui permet de récupérer un travail de sauvegarde.
        //Created a method to recover a backup job.
        public static BackupJob? GetBackupJob(string name)
        {
            foreach (BackupJob backupJob in backupJobs)
            {
                if (backupJob.Name == name)
                {
                    return backupJob;
                }
            }
            return null;
        }

        //Création d'une méthode qui permet de créer un nouveau travail de sauvegarde.
        //Created a method to create a new backup job.
        public static void CreateBackupJob()
        {
            //Création de plusiurs variables qui permettent de récupérer les informations sur les travaux de sauvegardes.
            //Creation of several variables that allow to retrieve information on backup jobs.
            backupJobs.Add(new BackupJob { 
                Name = AskForNameNotEmpty(),
                SourcePath = AskForSourcePathNotEmpty(),
                DestinationPath = AskForDestinationPathNotEmpty(),
                Type = AskForTypeNotEmpty()
            });
            WriteBackupJob();
        }

        //Création d'une méthode qui permet de modifier un travail de sauvegarde.
        //Created a method to modify a backup job.
        public static void ModifyBackupJob(string name)
        {
            //Cherche à avoir les informations sur les travaux de sauvegarde.
            //Seeks information about backup jobs.

            
            foreach (BackupJob backupJob in backupJobs)
            {
                if (backupJob.Name == name)
                {
                    string nameChange = AskForNameModification();
                    if (nameChange != "") backupJob.Name = nameChange;

                    string sourcePath = AskForSourcePathModification();
                    if (sourcePath != "") backupJob.SourcePath = sourcePath;

                    string destinationPath = AskForDestinationPathModification();
                    if (destinationPath != "") backupJob.DestinationPath = destinationPath;

                    string type = AskForTypeModification();
                    if (type != "") backupJob.Type = type;
                    break;
                }
            }
            WriteBackupJob();
        }

        //Création d'une méthode qui permet de supprimer un travail de sauvegarde.
        //Created a method to delete a backup job.
        public static void DeleteBackupJob(string name)
        {
            //Cherche à avoir les informations sur les travaux de sauvegarde à supprimer.
            //Seeks information about the backup jobs to be deleted.
            foreach (BackupJob backupJob in backupJobs.ToList())
            {
                if (backupJob.Name == name)
                {
                    backupJobs.Remove(backupJob);
                    break;
                }
            }
            WriteBackupJob();
        }

        //Méthode qui permet de lancer une sauvegarde.
        //Method that allows to launch a backup.
        public static void LaunchSave(bool restore, string name)
        {
            BackupJob? backupJob = GetBackupJob(name);
            backupJob.Save(restore);
        }

        //Méthode qui permet de lancer toutes les sauvegardes en meme temps.
        //Method which makes it possible to launch all the safeguards at the same time.
        public static void LaunchAllSave(bool restore)
        {
            foreach (BackupJob backupJob in backupJobs)
            {
                backupJob.Save(restore);
            }
        }

        //Méthode qui vérifier si le chemin existe.
        //Method that checks if the path exists.
        public static bool AssertThatPathExist(string path)
        {
            return System.IO.Directory.Exists(path);
        }


        //probablement beaucoup d'améliorations contre la redondance de code possibles

        //Création d'une méthode qui demande à l'utilisateur le chemin du fichier et vérifie si il existe.
        //Created a method that asks the user for the file path and checks if it exists.
        public static string AskPathAndCheckIfExist(Func<string> MethodName, bool emptyAccepted)
        {
            string input = GetInput();
            //Vérifie si le chemin existe.
            //Checks if the path exists.
            if (AssertThatPathExist(input) || (input == "" && emptyAccepted)) return input;
            DisplayLanguage("PathDoesntExist");
            return MethodName();
        }

        //Création d'une méthode qui demande le type de travail de sauvegarde souhaité.
        //Create a method that requests the desired backup type.
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

        //Méthode qui permet de modifier le nom d'un travail de sauvegarde.
        //Method for changing the name of a Backup Job.
        public static string AskForNameModification()
        {
            DisplayLanguage("ModifyName");
            return GetInput();
        }

        //Méthode qui permet de demander un nom de travail de sauvegarde non vide.
        //Method that allows to ask for a non-empty Backup Job name.
        public static string AskForNameNotEmpty()
        {
            DisplayLanguage("Name");
            return GetInputNotEmpty();
        }

        //Méthode qui permet de modifier le chemin source d'un travail de sauvegarde.
        //Method for changing the source path of a Backup Job.
        public static string AskForSourcePathModification()
        {
            DisplayLanguage("ModifySourcePath");
            return AskPathAndCheckIfExist(AskForSourcePathModification, true);
        }

        //Méthode qui permet de demander un chemin source de travail de sauvegarde non vide.
        //Method to request a non-empty backup job source path.
        public static string AskForSourcePathNotEmpty()
        {
            DisplayLanguage("SourcePath");
            return AskPathAndCheckIfExist(AskForSourcePathNotEmpty, false);
        }

        //Méthode qui permet de modifier le chemin de destination d'un travail de sauvegarde.
        //A method of changing the destination path of a backup job.
        public static string AskForDestinationPathModification()
        {
            DisplayLanguage("ModifyDestinationPath");
            return AskPathAndCheckIfExist(AskForDestinationPathModification, true);
        }

        //Méthode qui permet de demander un chemin de destination de travail de sauvegarde non vide.
        //A method to request a non-empty backup job destination path.
        public static string AskForDestinationPathNotEmpty()
        {
            DisplayLanguage("DestinationPath");
            return AskPathAndCheckIfExist(AskForDestinationPathNotEmpty, false);
        }

        //Méthode qui permet de modifier le type d'un travail de sauvegarde.
        //A method of changing the type of a backup job.
        public static string AskForTypeModification()
        {
            DisplayLanguage("ModifyType");
            return AskTypeAndCheck(AskForTypeModification, true);
        }

        //Méthode qui permet de demander un type de travail de sauvegarde non vide.
        //Method to request a non-empty backup job type.
        public static string AskForTypeNotEmpty()
        {
            DisplayLanguage("Type");
            return AskTypeAndCheck(AskForTypeNotEmpty, false);
        }
    }
}
