using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using Projet_Programmation_Système.developpement;
using static Projet_Programmation_Système.developpement.ConsoleManager;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace AppWPF.developpement
{

    //Création d'une classe pour les sauvegardes.
    //Creating a class for backups.
    public class BackupJob
    {
        //Création des variables paramétriques des sauvegardes.
        //Creation of parametric variables for backups.
        public string Name { set; get; }
        public string SourcePath { set; get; }
        public string DestinationPath { set; get; }
        public string Type { set; get; }

        public long fileSizeTotal = 0;
        public long fileNumberTotal = 0;
        public long fileSizeLeft = 0;
        public long fileNumberLeft = 0;
        private TimeSpan fileTransferTime;


        public async void Save(bool restore)
        {
            //Vérification du chemin d'accès.
            //Verification of the access path.
            if (!BackupJobsManager.AssertThatPathExist(SourcePath) || !BackupJobsManager.AssertThatPathExist(DestinationPath))
            {
                DisplayLanguage("PathDoesntExist");
                return;
            }

            fileSizeTotal = 0;
            fileNumberTotal = 0;

            //Création d'une méthode pour réstaurer un fichier.
            //Creation of a method to restore a file.
            DisplayLanguage("CopyFiles");
            DirectoryInfo d;
            if (restore)
            {
                d = new DirectoryInfo(DestinationPath);
                Task directoryInfo = GetDirectoryInfo(d, DestinationPath, SourcePath);
                await directoryInfo;
            }
            else
            {
                d = new DirectoryInfo(SourcePath);
                Task directoryInfo = GetDirectoryInfo(d, SourcePath, DestinationPath);
                await directoryInfo;
            }

            fileSizeLeft = fileSizeTotal;
            fileNumberLeft = fileNumberTotal;


            //Calcule de la date dans une variable.
            //Calculation of the date in a variable.
            DateTime date1 = DateTime.Now;

            //Création d'une méthode pour copier les fichiers durant la restauration.
            //Create a method to copy files during recovery.
            if (restore)
            {
                d = new DirectoryInfo(DestinationPath);
                Task copy = Copy(d, DestinationPath, SourcePath);
                await copy;
            }
            else
            {
                d = new DirectoryInfo(SourcePath);
                Task copy = Copy(d, SourcePath, DestinationPath);
                await copy;
            }
            fileTransferTime = DateTime.Now - date1;


            DisplayEmptyLine();
            DisplayLanguage("Done");

            //Générer l'objet de log d'activité a écrire dans les fichiers
            //Generate the activity Log object to write to the files

            Task writeStateLog = FileManager.WriteStateLog(new StateLog
            {
                backupJob = this,
                fileNumber = 0,
                fileSize = 0,
                active = false,
                fileNumberLeft = 0,
                fileSizeLeft = 0
            });

            await writeStateLog;



            if (MainMenuManager.formatLogs == "json") FileManager.WriteDailyLogToFile(GenerateLog(restore));
            else FileManager.SerializeToXML(GenerateLog(restore));
        }


        //Création d'une méthode pour copier les fichiers
        //Copy files from one directory to another.
        public async Task Copy(DirectoryInfo d, string sourcePath, string destinationPath)
        {
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                string fileToCopy = fi.FullName.Replace(sourcePath, destinationPath);

                if (!File.Exists(fileToCopy) || Type == "1" || IsFileModified(fi, destinationPath)) //full save or differential save 
                {
                    Console.Write("\r{0}%   ", fi.Name);
                    ClearCurrentConsoleLine();

                    File.Copy(fi.FullName, fileToCopy, true);

                    fileNumberLeft--;
                    fileSizeLeft -= fi.Length;

                    Task writeStateLog = FileManager.WriteStateLog(new StateLog
                    {
                        backupJob = this,
                        fileNumber = fileNumberTotal,
                        fileSize = fileSizeTotal,
                        active = true,
                        fileNumberLeft = fileNumberLeft,
                        fileSizeLeft = fileSizeLeft
                    });
                    await writeStateLog;
                }
            }
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                string dirToCreate = di.FullName.Replace(sourcePath, destinationPath);
                //On copie tout les sous dossiers, et on rappelle la méthode pour les sous dossiers.
                //Copy all the subfolders, and call the method for the subfolders.
                if (!File.Exists(dirToCreate) || Type == "1") Directory.CreateDirectory(dirToCreate);
                Task copy = Copy(di, sourcePath, destinationPath);
                await copy;
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

        public async Task GetDirectoryInfo(DirectoryInfo d, string sourcePath, string destinationPath)
        {
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                string fileToCopy = fi.FullName.Replace(sourcePath, destinationPath);
                if (!File.Exists(fileToCopy) || Type == "1" || IsFileModified(fi, destinationPath))
                {
                    fileSizeTotal += fi.Length;
                    fileNumberTotal++;
                }
            }
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                Task directoryInfo = GetDirectoryInfo(di, sourcePath, destinationPath);
                await directoryInfo;
            }
        }



        //Création d'une méthode pour générer un log.
        //Creation of a method to generate a log.
        public Log GenerateLog(bool restore)
        {
            if (restore)
            {
                return new Log
                {
                    name = Name,
                    sourcePath = DestinationPath,
                    destinationPath = SourcePath,
                    type = Type,
                    fileSize = fileSizeTotal.ToString() + " B",
                    fileTransferTime = fileTransferTime.TotalMilliseconds.ToString() + " ms",
                    date = DateTime.Now.ToString("F")
                };
            }
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