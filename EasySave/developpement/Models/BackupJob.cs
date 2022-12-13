using EasySave.developpement.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EasySave.developpement.Models
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
        
        public static string LogExtension = "0"; //0 : json, 1 : xml


        private SaveBackupJobStatusViewModel? _saveBackupJobStatusViewModel;

        public BackupJob(Guid id, string name, string sourcePath, string destinationPath, string type)
        {
            Id = id;
            Name = name;
            SourcePath = sourcePath;
            DestinationPath = destinationPath;
            Type = type;
        }
    }
}