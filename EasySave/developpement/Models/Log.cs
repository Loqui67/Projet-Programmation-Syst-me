//Fichier servant à la sauvegarde des logs(BakcupJob).
//File used to save logs(BakcupJob).
using System;

namespace EasySave.developpement.Models
{
    public class Log
    {
        public string Name { get; }
        public string SourcePath { get; }
        public string DestinationPath { get; }
        public string Type { get; }
        public long FileSize { get; }
        public long FileNumber { get; }
        public TimeSpan FileTransferTime { get; }
        public TimeSpan FileEncryptTime { get; }
        public string Date { get; }

        public Log(string name, string sourcePath, string destinationPath, string type, long fileSize, long fileNumber, TimeSpan fileTransferTime, TimeSpan fileEncryptTime, string date)
        {
            Name = name;
            SourcePath = sourcePath;
            DestinationPath = destinationPath;
            Type = type;
            FileSize = fileSize;
            FileNumber = fileNumber;
            FileTransferTime = fileTransferTime;
            FileEncryptTime = fileEncryptTime;
            Date = date;
            
        }
    }
}
