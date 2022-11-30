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
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

//Création d'une classe pour les sauvegardes.
//Creating a class for backups.
public class BackupJob
{
    //Création des variables paramétriques des sauvegardes.
    //Creation of parametric variables for backups.
    public string id { set; get; }
    public string name { set; get; }
    public string sourcePath { set; get; }
    public string destinationPath { set; get; }
    public string type { set; get; }
    
    public long fileSizeTotal = 0;
    public long fileNumberTotal = 0;
    public long fileSizeLeft = 0;
    public long fileNumberLeft = 0;
    private TimeSpan fileTransferTime;



    //Création d'un constructeur pour les sauvegardes.
    //Creation of a constructor for backups.
    public async void Save(bool restore)
    {
        //Vérification du chemin d'accès.
        //Verification of the access path.
        if (!BackupJobsManager.AssertThatPathExist(sourcePath) || !BackupJobsManager.AssertThatPathExist(destinationPath)) {
            DisplayLanguage("PathDoesntExist");
            return;
        }
        //Calcule de la date dans une variable.
        //Calculation of the date in a variable.
        DateTime date1 = DateTime.Now;

        fileSizeTotal = 0;
        fileNumberTotal = 0;

        //Création d'une méthode pour réstaurer un fichier.
        //Creation of a method to restore a file.
        DisplayLanguage("CopyFiles");
        DirectoryInfo d;
        if (restore)
        {
            d = new DirectoryInfo(destinationPath);
            Task directoryInfo = GetDirectoryInfo(d, destinationPath, sourcePath);
            await directoryInfo;
        }
        else { 
            d = new DirectoryInfo(sourcePath);
            Task directoryInfo = GetDirectoryInfo(d, sourcePath, destinationPath);
            await directoryInfo;
        }
        
        fileSizeLeft = fileSizeTotal;
        fileNumberLeft = fileNumberTotal;

        //Création d'une méthode pour copier les fichiers durant la restauration.
        //Create a method to copy files during recovery.
        if (restore)
        {
            d = new DirectoryInfo(destinationPath);
            Task copy = Copy(d, destinationPath, sourcePath);
            await copy;
        }
        else
        {
            d = new DirectoryInfo(sourcePath);
            Task copy = Copy(d, sourcePath, destinationPath);
            await copy;
        }

        DisplayEmptyLine();
        DisplayLanguage("Done");

        //Création du gestionnaire de fichier json.
        //Creation of the json file manager.
        Task writeStateLog = JsonFileManager.WriteStateLog(new StateLog
        {
            backupJob = this,
            fileNumber = 0,
            fileSize = 0,
            active = false,
            fileNumberLeft = 0,
            fileSizeLeft = 0
        });

        await writeStateLog;

        fileTransferTime = DateTime.Now - date1;
        
        JsonFileManager.WriteDailyLogToFile(GenerateLog());
    }


    //Création d'une méthode pour faire une sauvegarde différentiel.
    //Creation of a method to make a differential backup.
    public async Task Copy(DirectoryInfo d, string sourcePath, string destinationPath)
    {
        FileInfo[] fis = d.GetFiles();
        foreach (FileInfo fi in fis)
        {
            string fileToCopy = fi.FullName.Replace(sourcePath, destinationPath);
            if (!File.Exists(fileToCopy) || type == "1") //full save or differential save 
            {
                Console.Write("\r{0}%   ", fi.Name);
                ClearCurrentConsoleLine();
                
                File.Copy(fi.FullName, fileToCopy, true);
                
                fileNumberLeft--;
                fileSizeLeft -= fi.Length;
                
                Task writeStateLog = JsonFileManager.WriteStateLog(new StateLog { 
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
            //Condition qui choisi si l'on fait une sauvegarde complete ou différentiel.
            //Condition that chooses if we make a complete or differential backup.
            if (!File.Exists(dirToCreate) || type == "1") Directory.CreateDirectory(dirToCreate);  
            Task copy = Copy(di, sourcePath, destinationPath);
            await copy;
        }
    }

    public async Task GetDirectoryInfo(DirectoryInfo d, string sourcePath, string destinationPath)
    {
        FileInfo[] fis = d.GetFiles();
        foreach (FileInfo fi in fis)
        {
            string fileToCopy = fi.FullName.Replace(sourcePath, destinationPath);
            if (!File.Exists(fileToCopy) || type == "1")
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
    public Log GenerateLog()
    {
        return new Log {
            name = name,
            sourcePath = sourcePath,
            destinationPath = destinationPath,
            type = type,
            fileSize = fileSizeTotal.ToString() + " B",
            fileTransferTime = fileTransferTime.TotalMilliseconds.ToString() + " ms",
            date = DateTime.Now.ToString("F")
        };
    }
}


