﻿using AppWPF.developpement.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.developpement.Models
{
    public class SaveFiles
    {
        public List<FilesInfo> Files { get; set; }
        public List<FilesInfo> FilesToEncrypt { get; set; }
        public List<string> Directories { get; set; }
        public long FileSizeTotal { get; set; }
        public long FileNumberTotal { get; set; }

        public long FileSizeLeft { get; set; }
        public long FileNumberLeft { get; set; }
        public TimeSpan FileTransferTime { get; set; }
        public TimeSpan FileEncryptTime { get; set; }
        
        public async Task GetInfos(BackupJob backupJob)
        {
            List<string> allDirectory = Directory.GetDirectories(backupJob.SourcePath, "*", SearchOption.AllDirectories).ToList();
            List<string> allFiles = Directory.GetFiles(backupJob.SourcePath, "*", SearchOption.AllDirectories).ToList();
            FilesToEncrypt = new List<FilesInfo>();
            Files = new List<FilesInfo>();
            Directories = new List<string>();
            FileSizeTotal = 0;
            FileNumberTotal = 0;

            foreach (string directory in allDirectory)
            {
                if (!Directory.Exists(directory.Replace(backupJob.SourcePath, backupJob.DestinationPath)) || backupJob.Type == "0")
                {
                    Directories.Add(directory);
                }
            }

            foreach (string file in allFiles)
            {
                FileInfo fileInfo = new FileInfo(file);
                if (!File.Exists(file.Replace(backupJob.SourcePath, backupJob.DestinationPath)) || IsFileModified(fileInfo, backupJob) || backupJob.Type == "0")
                {
                    
                    FileSizeTotal += fileInfo.Length;
                    FileNumberTotal++;
                    if (BackupJobsViewModel.config.AllExtensionCryptage.Exists(extensionCryptage => extensionCryptage.Name == fileInfo.Extension))
                    {
                        FilesToEncrypt.Add(new FilesInfo(fileInfo.Name, fileInfo.FullName, fileInfo.Length));
                    }
                    else
                    {
                        Files.Add(new FilesInfo(fileInfo.Name, fileInfo.FullName, fileInfo.Length));
                    }
                }
            }

            FileSizeLeft = FileSizeTotal;
            FileNumberLeft = FileNumberTotal;
        }

        private bool IsFileModified(FileInfo fileInfo, BackupJob backupJob)
        {
            try
            {
                FileInfo fileInfoDestination = new FileInfo(fileInfo.FullName.Replace(backupJob.SourcePath, backupJob.DestinationPath));

                if (fileInfo.LastWriteTime > fileInfoDestination.LastWriteTime)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
