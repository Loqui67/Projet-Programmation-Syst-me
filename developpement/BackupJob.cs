﻿using System;
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

        DateTime date1 = DateTime.Now;

        fileSizeTotal = 0;
        fileNumberTotal = 0;
        
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

        fileTransferTime = DateTime.Now - date1;
        
        if (MainMenuManager.formatLogs == "json") FileManager.WriteDailyLogToFile(GenerateLog(restore));
        else FileManager.SerializeToXML(GenerateLog(restore));
    }



    public async Task Copy(DirectoryInfo d, string sourcePath, string destinationPath)
    {
        FileInfo[] fis = d.GetFiles();
        foreach (FileInfo fi in fis)
        {
            string fileToCopy = fi.FullName.Replace(sourcePath, destinationPath);

            if (!File.Exists(fileToCopy) || type == "1" || IsFileModified(fi, destinationPath)) //full save or differential save 
            {
                Console.Write("\r{0}%   ", fi.Name);
                ClearCurrentConsoleLine();
                
                File.Copy(fi.FullName, fileToCopy, true);
                
                fileNumberLeft--;
                fileSizeLeft -= fi.Length;
                
                Task writeStateLog = FileManager.WriteStateLog(new StateLog { 
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
            if (!File.Exists(dirToCreate) || type == "1") Directory.CreateDirectory(dirToCreate); //full save or differential save 
            Task copy = Copy(di, sourcePath, destinationPath);
            await copy;
        }
    }

    public bool IsFileModified(FileInfo fileInfo, string destinationPath)
    {
        string fileToCopy = fileInfo.FullName.Replace(sourcePath, destinationPath);
        try {
            FileInfo fileDest = new FileInfo(fileToCopy);
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
            if (!File.Exists(fileToCopy) || type == "1") //full save or differential save 
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
                name = name,
                sourcePath = destinationPath,
                destinationPath = sourcePath,
                type = type,
                fileSize = fileSizeTotal.ToString() + " B",
                fileTransferTime = fileTransferTime.TotalMilliseconds.ToString() + " ms",
                date = DateTime.Now.ToString("F")
            };
        }
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


