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

    private long fileSize = 0;
    private TimeSpan fileTransferTime;
    //Création d'un constructeur pour les sauvegardes.
    //Creation of a constructor for backups.
    public void Save()
    {
        //Vérification du chemin d'accès.
        //Verification of the access path.
        if (!BackupJobsManager.AssertThatPathExist(sourcePath)) {
            DisplayLanguage("SourcePathDoesntExist");
            return;
        }
        //Parametrage du type de sauvegarde (complète ou différentiel).
        //Configuration of the backup type (full or differential).
        if (type == "1") FullSave();
        else DifferentialSave();

        JsonFileManager.WriteDailyLogToFile(GenerateLog());
    }

    //Création d'une méthode pour les sauvegardes complètes.
    //Creation of a method for full backups.
    public void FullSave()
    {
        DateTime date1 = DateTime.Now;
        //Création d'un nouveau dossier de sauvegarde.
        //Creation of a new backup folder.
        fileSize = DirSize(new DirectoryInfo(sourcePath));
        System.IO.Directory.Delete(destinationPath, true);
        System.IO.Directory.CreateDirectory(destinationPath);
        var allDirectories = Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories);
        //Création d'un dossier pour chaque dossier du dossier source durant la sauvegarde.
        //Creation of a folder for each folder of the source folder during the backup.

        
        foreach (string dir in allDirectories)
        {
            string dirToCreate = dir.Replace(sourcePath, destinationPath);
            Directory.CreateDirectory(dirToCreate);
        }
        //Copie de chaque fichier du dossier source dans le dossier de sauvegarde.
        //Copies each file from the source folder to the backup folder.
        var allFiles = Directory.GetFiles(sourcePath, "*", SearchOption.AllDirectories);
        //Copie de chaque fichier du dossier source dans le dossier de sauvegarde.
        //Copies each file from the source folder to the backup folder.
        foreach (string file in allFiles)
        {
            string fileToCopy = file.Replace(sourcePath, destinationPath);
            File.Copy(file, fileToCopy, true);
        }
        fileTransferTime = DateTime.Now - date1;
    }


    //Création d'une méthode pour les sauvegardes différentielles.
    //Creation of a method for differential backups.
    public void DifferentialSave() {
        DateTime date1 = DateTime.Now;
        //Création d'un nouveau dossier de sauvegarde.
        //Creation of a new backup folder.
        var allDirectories = Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories);
        //Création d'un dossier pour chaque dossier du dossier source durant la sauvegarde.
        //Creation of a folder for each folder of the source folder during the backup.
        foreach (string dir in allDirectories)
        {
            string dirToCreate = dir.Replace(sourcePath, destinationPath);
            if (!Directory.Exists(dirToCreate)) Directory.CreateDirectory(dirToCreate);
        }
        //Copie de chaque fichier du dossier source dans le dossier de sauvegarde.
        //Copies each file from the source folder to the backup folder.
        var allFiles = Directory.GetFiles(sourcePath, "*", SearchOption.AllDirectories);
        foreach (string file in allFiles)
        {
            string fileToCopy = file.Replace(sourcePath, destinationPath);
            //Si le fichier n'existe pas dans le dossier de sauvegarde, on le copie.
            //If the file does not exist in the backup folder, we copy it.
            if (!File.Exists(fileToCopy)) File.Replace(file, fileToCopy, null);
        }
        fileTransferTime = DateTime.Now - date1;
    }

    public static long DirSize(DirectoryInfo d)
    {
        long size = 0;
        // Add file sizes.
        FileInfo[] fis = d.GetFiles();
        foreach (FileInfo fi in fis)
        {
            size += fi.Length;
        }
        // Add subdirectory sizes.
        DirectoryInfo[] dis = d.GetDirectories();
        foreach (DirectoryInfo di in dis)
        {
            size += DirSize(di);
        }
        return size;
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
            fileSize = fileSize.ToString(),
            fileTransferTime = fileTransferTime.TotalMilliseconds.ToString() + "ms",
            date = DateTime.Now
        };
    }
}


