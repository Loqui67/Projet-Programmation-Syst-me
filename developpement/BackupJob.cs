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
    
    public bool AssertThatPathExist(string path)
    {
        return System.IO.Directory.Exists(path);
    }

    public void Save()
    {
        if (!AssertThatPathExist(sourcePath)) {
            ConsoleManager.DisplayLanguage("SourcePathDoesntExist");
            return;
        }

        System.IO.Directory.CreateDirectory(destinationPath);
        var allDirectories = Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories);
        foreach (string dir in allDirectories)
        {
            string dirToCreate = dir.Replace(sourcePath, destinationPath + name);
            Directory.CreateDirectory(dirToCreate);
        }            
    }

    
}


