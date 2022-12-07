using AppWPF.developpement.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AppWPF.developpement.Models
{

    //Création d'une classe pour les sauvegardes.
    //Creating a class for backups.
    public class BackupJob
    {
        //Création des variables paramétriques des sauvegardes.
        //Creation of parametric variables for backups.
        public Guid Id { get; }
        public string Name { get; }
        public string SourcePath { get; }
        public string DestinationPath { get; }
        public string Type { get; }

        public long fileSizeTotal = 0;
        public long fileNumberTotal = 0;
        public long fileSizeLeft = 0;
        public long fileNumberLeft = 0;
        private TimeSpan fileTransferTime;
        public static string LogExtension = "0"; //0 : json, 1 : xml
        private List<string> files;
        private List<string> directories;

        private SaveBackupJobViewModel? _saveBackupJobViewModel;

        public BackupJob(Guid id, string name, string sourcePath, string destinationPath, string type)
        {
            Id = id;
            Name = name;
            SourcePath = sourcePath;
            DestinationPath = destinationPath;
            Type = type;
        }

        ///Création d'une méthode pour sauvegarder les travaux de sauvegardes dans le ViewModel
        ///Creating a method to save backup jobs in the ViewModel
        public async Task Save(SaveBackupJobViewModel saveBackupJobViewModel)
        {
            files = new List<string>();
            directories = new List<string>();
            fileSizeTotal = 0;
            fileNumberTotal = 0;
            _saveBackupJobViewModel = saveBackupJobViewModel;
            _saveBackupJobViewModel.IsLoadingStats = "Visible";
            await Task.Run(GetStats);
            fileSizeLeft = fileSizeTotal;
            fileNumberLeft = fileNumberTotal;
            await Task.Run(SaveBackup);
            if (LogExtension == "0") FileManager.WriteDailyLogToFile(GenerateLog());
            else FileManager.SerializeToXML(GenerateLog());
            _saveBackupJobViewModel.IsLoadingStats = "Collapsed";
        }


        ///Création d'une méthode pour obtenir des statistiques pour les sauvegardes
        ///Created a method to get statistics for backups
        private void GetStats()
        {
            //Déclaration des variables.
            //Declaration of variables.
            List<string> allFiles = Directory.GetFiles(SourcePath, "*", SearchOption.AllDirectories).ToList();
            List<string> allDirectories = Directory.GetDirectories(SourcePath, "*", SearchOption.AllDirectories).ToList();

            //Calcul du nombre de fichiers et de dossiers.
            //Calculation of the number of files and folders.
            foreach (string file in allFiles)
            {
                string fileToCopy = file.Replace(SourcePath, DestinationPath);
                FileInfo fileInfo = new(file);
                if (!File.Exists(fileToCopy) || Type == "0" || IsFileModified(fileInfo, fileToCopy))
                {
                    files.Add(file);
                    fileSizeTotal += fileInfo.Length;
                    fileNumberTotal++;
                }
            }
            
            foreach (string directory in allDirectories)
            {
                string folderToCopy = directory.Replace(SourcePath, DestinationPath);
                if (!Directory.Exists(folderToCopy) || Type == "0")
                {
                    directories.Add(directory);
                }
            }

            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                if (!File.Exists(file.Replace(SourcePath, DestinationPath)))
                {
                    fileSizeLeft += fileInfo.Length;
                    fileNumberLeft++;
                }
            }
        }

        ///Création d'une méthode pour sauvegarder les fichiers et dossiers
        ///Creating a method to back up files and folders
        public async Task SaveBackup()
        {
            if (Type == "0") Directory.Delete(DestinationPath, true);
            foreach (string directory in directories)
            {
                string folderToCopy = directory.Replace(SourcePath, DestinationPath);
                await Task.Run(() => Directory.CreateDirectory(folderToCopy));
            }

            foreach (string file in files)
            {
                string fileToCopy = file.Replace(SourcePath, DestinationPath);
                await Task.Run(() =>
                {
                    FileInfo fileInfo = new FileInfo(file);
                    _saveBackupJobViewModel.BackupJobFileTransfering = fileInfo.Name;
                    _saveBackupJobViewModel.BackupJobFileTransferingCount = FileLeftSlashFileTotal();
                    _saveBackupJobViewModel.BackupJobProgressBarValue = ProgressBarValue();
                    File.Copy(file, fileToCopy, true);
                    fileSizeLeft -= fileInfo.Length;
                    fileNumberLeft--;
                });
            }
        }

        ///Création d'une méthode pour vérifier si un fichier a été modifié
        ///Creating a method to check if a file has been modified
        public bool IsFileModified(FileInfo fileInfo, string destinationPath)
        {
            string fileToCopy = fileInfo.FullName.Replace(SourcePath, destinationPath);
            try
            {
                FileInfo fileDest = new(fileToCopy);
                if (fileInfo.LastWriteTime != fileDest.LastWriteTime) return true;
                else return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        ///Création d'une méthode qui permet de générer la valeur de la barre de progression
        ///Creation of a method that allows to generate the value of the progress bar
        public float ProgressBarValue()
        {
            return (100 - (fileSizeLeft * 100 / fileSizeTotal));
        }

        ///Création d'une méthode qui permet de faire une division pour obtenir le nombre de fichiers restants sur le nombre total de fichiers
        ///Creation of a method that allows you to do a division to obtain the number of files remaining out of the total number of files
        public string FileLeftSlashFileTotal()
        {
            return (fileNumberTotal - fileNumberLeft + 1) + "/" + fileNumberTotal;
        }



        //Création d'une méthode pour générer un log.
        //Creation of a method to generate a log.
        public Log GenerateLog()
        {
            return new Log
            {
                name = Name,
                sourcePath = SourcePath,
                destinationPath = DestinationPath,
                type = Type,
                fileSize = fileSizeTotal.ToString() + " B",
                fileTransferTime = fileTransferTime.TotalMilliseconds.ToString() + " ms",
                date = DateTime.Now.ToString("F")
            };
        }
    }
}