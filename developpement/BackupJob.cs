using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using Projet_Programmation_Système.developpement;


public class BackupJob
{
    public string id { set; get; }
    public string name { set; get; }
    public string sourcePath { set; get; }
    public string destinationPath { set; get; }
    public string type { set; get; }
    
    public void Save()
    {
        if (!BackupJobsManager.AssertThatPathExist(sourcePath)) {
            ConsoleManager.DisplayLanguage("SourcePathDoesntExist");
            return;
        }
        
        if (type == "1") FullSave();
        else DifferentialSave();
    }

    public void FullSave()
    {
        System.IO.Directory.CreateDirectory(destinationPath);
        var allDirectories = Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories);
        foreach (string dir in allDirectories)
        {
            string dirToCreate = dir.Replace(sourcePath, destinationPath);
            Directory.CreateDirectory(dirToCreate);
        }
        
        var allFiles = Directory.GetFiles(sourcePath, "*", SearchOption.AllDirectories);
        foreach (string file in allFiles)
        {
            string fileToCopy = file.Replace(sourcePath, destinationPath);
            File.Copy(file, fileToCopy);
        }
    }
    
    public void DifferentialSave() {
        var allDirectories = Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories);
        foreach (string dir in allDirectories)
        {
            string dirToCreate = dir.Replace(sourcePath, destinationPath + name);
            if (!Directory.Exists(dirToCreate)) Directory.CreateDirectory(dirToCreate);
        }
        var allFiles = Directory.GetFiles(sourcePath, "*", SearchOption.AllDirectories);
        foreach (string file in allFiles)
        {
            string fileToCopy = file.Replace(sourcePath, destinationPath + name);
            if (!File.Exists(fileToCopy)) File.Replace(file, fileToCopy, null);
        }
    }
}


