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
        private List<string> filesToEncrypt;
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

        public async Task Save(SaveBackupJobViewModel saveBackupJobViewModel)
        {
            if (IsProcessusRunning()) return;
            
            files = new List<string>();
            filesToEncrypt = new List<string>();
            directories = new List<string>();
            _saveBackupJobViewModel = saveBackupJobViewModel;

            _saveBackupJobViewModel.IsLoadingStats = "Visible";
            
            await Task.Run(GetStats);
            await Task.Run(SaveBackup);
            
            if (LogExtension == "0") FileManager.WriteDailyLogToFile(GenerateLog());
            else FileManager.SerializeToXML(GenerateLog());
            
            _saveBackupJobViewModel.IsLoadingStats = "Collapsed";
        }

        
        private void GetStats()
        {
            //Déclaration des variables.
            //Declaration of variables.
            fileSizeTotal = 0;
            fileNumberTotal = 0;
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
                    fileSizeTotal += fileInfo.Length;
                    fileNumberTotal++;
                    if (BackupJobsViewModel.config.AllExtensionCryptage.Exists(extensionCryptage => extensionCryptage.Name == fileInfo.Extension))
                    {
                        filesToEncrypt.Add(file);
                    } 
                    else
                    {
                        files.Add(file);
                    }
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
            fileSizeLeft = fileSizeTotal;
            fileNumberLeft = fileNumberTotal;
        }

        public async Task SaveBackup()
        {
            if (Type == "0") Directory.Delete(DestinationPath, true);
            foreach (string directory in directories)
            {
                string folderToCopy = directory.Replace(SourcePath, DestinationPath);
                await Task.Run(() => Directory.CreateDirectory(folderToCopy));
            }
            //Do two things at the same time.

            //Création des tâches.

            foreach (string fileToEncrypt in filesToEncrypt)
            {
                string fileToCopy = fileToEncrypt.Replace(SourcePath, DestinationPath);
                await Task.Run(() => 
                {
                    FileInfo fileInfo = new FileInfo(fileToEncrypt);
                    fileSizeLeft -= fileInfo.Length;
                    fileNumberLeft--;
                });
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

        public bool IsProcessusRunning()
        {
            return BackupJobsListingViewModel.IsProcessusDetected;
        }

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

        public float ProgressBarValue()
        {
            return (100 - (fileSizeLeft * 100 / fileSizeTotal));
        }

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