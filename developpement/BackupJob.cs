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

    private long fileSize = 0;
    private TimeSpan fileTransferTime;



    //Création d'un constructeur pour les sauvegardes.
    //Creation of a constructor for backups.
    public void Save()
    {
        //Vérification du chemin d'accès.
        //Verification of the access path.
        if (!BackupJobsManager.AssertThatPathExist(sourcePath) || !BackupJobsManager.AssertThatPathExist(destinationPath)) {
            DisplayLanguage("PathDoesntExist");
            return;
        }

        DateTime date1 = DateTime.Now;

        //Création d'un nouveau dossier de sauvegarde.
        //Creation of a new backup folder.
        fileSize = 0;
        DisplayLanguage("CopyFiles");
        Copy(new DirectoryInfo(sourcePath));
        DisplayEmptyLine();
        DisplayLanguage("Done");
        fileTransferTime = DateTime.Now - date1;
        JsonFileManager.WriteDailyLogToFile(GenerateLog());
    }



    public void Copy(DirectoryInfo d)
    {
        FileInfo[] fis = d.GetFiles();
        foreach (FileInfo fi in fis)
        {
            string fileToCopy = fi.FullName.Replace(sourcePath, destinationPath);
            if (!File.Exists(fileToCopy) || type == "1")
            {
                Console.Write("\r{0}%   ", fi.Name);
                File.Copy(fi.FullName, fileToCopy, true);
                fileSize += fi.Length;
            }
        }
        DirectoryInfo[] dis = d.GetDirectories();
        foreach (DirectoryInfo di in dis)
        {
            string dirToCreate = di.FullName.Replace(sourcePath, destinationPath);
            if (!File.Exists(dirToCreate) || type == "1") Directory.CreateDirectory(dirToCreate);
            Copy(di);
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
            fileSize = fileSize.ToString() + " B",
            fileTransferTime = fileTransferTime.TotalMilliseconds.ToString() + " ms",
            date = DateTime.Now.ToString("F")
        };
    }
}


