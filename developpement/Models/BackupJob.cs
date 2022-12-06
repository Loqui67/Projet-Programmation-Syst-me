﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using AppWPF.developpement.ViewModels;

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
        public static string LogExtension = "json";
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
            if (LogExtension == "json") FileManager.WriteDailyLogToFile(GenerateLog());
            else FileManager.SerializeToXML(GenerateLog());
            _saveBackupJobViewModel.IsLoadingStats = "Collapsed";
        }


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
                if (!File.Exists(fileToCopy) || Type == "0" || IsFileModified(fileInfo, fileToCopy)) {
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
                await Task.Run(() => {
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