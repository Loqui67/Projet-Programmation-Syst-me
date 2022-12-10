using AppWPF.developpement.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace AppWPF.developpement.Models
{
    public class BackupJobSaver
    {
        public long fileSizeLeft = 0;
        public long fileNumberLeft = 0;

        private SaveBackupJobStatusViewModel _saveBackupJobStatusViewModel;
        private BackupJob BackupJob { get; set; }

        private string Name => BackupJob.Name;
        private string SourcePath => BackupJob.SourcePath;
        private string DestinationPath => BackupJob.DestinationPath;
        private string Type => BackupJob.Type;
        
        public BackupJobSaver(SaveBackupJobStatusViewModel saveBackupJobStatusViewModel)
        {
            _saveBackupJobStatusViewModel = saveBackupJobStatusViewModel;
        }

        public async Task StartSave(SaveFiles saveFiles, BackupJob backupJob)
        {
            BackupJob = backupJob;
            await CreateDirectories(saveFiles.Directories);
            await Task.WhenAll(EncryptFiles(saveFiles), CopyFiles(saveFiles));
            await WriteToDailyLog(CreateLog(saveFiles));
        }

        public async Task StartListSaveInParallel(List<SaveFiles> saveFilesList, SaveAllBackupJobsViewModel saveAllBackupJobsViewModel, List<BackupJob> backupJobs)
        {
            int total = backupJobs.Count;
            int index = 0;
            AllSavesProgression(saveAllBackupJobsViewModel, total, index);

            //Execute all save in parallel

            await Task.WhenAll(saveFilesList.Select(async saveFiles =>
            {
                index++;
                await StartSave(saveFiles, backupJobs[index - 1]);
                AllSavesProgression(saveAllBackupJobsViewModel, index, total);
            }));




        }

        private async Task CreateDirectories(List<string> directories)
        {
            foreach (string directory in directories)
            {
                string folderDestinationPath = directory.Replace(SourcePath, DestinationPath);
                if (!Directory.Exists(folderDestinationPath))
                {
                    Directory.CreateDirectory(folderDestinationPath);
                }
            }
        }

        private async Task EncryptFiles(SaveFiles saveFiles)
        {
            if (saveFiles.FilesToEncrypt.Count == 0)
            {
                saveFiles.FileEncryptTime = new TimeSpan(0);
                return;
            }
            try
            {
                DateTime start = DateTime.Now;
                foreach (FilesInfo fileToEncrypt in saveFiles.FilesToEncrypt)
                {
                    await Task.Run(() =>
                    {
                        string path = "\"" + fileToEncrypt.Path + "\"" + " " + "\"" + fileToEncrypt.Path.Replace(SourcePath, DestinationPath) + "\"";
                        ProcessStartInfo process = new("..\\..\\..\\CryptoSoft\\CryptoSoft.exe")
                        {
                            Arguments = path
                        };
                        Process.Start(process);
                    });
                    saveFiles.FileSizeLeft -= fileToEncrypt.Size;
                    saveFiles.FileNumberLeft--;
                }
                DateTime end = DateTime.Now;
                saveFiles.FileEncryptTime = end - start;
            }
            catch (Exception)
            {
                saveFiles.FileEncryptTime = new TimeSpan(-1);
            }
        }


        private async Task CopyFiles(SaveFiles saveFiles)
        {
            DateTime start = DateTime.Now;
            foreach (FilesInfo file in saveFiles.Files)
            {
                await Task.Run(async () =>
                {
                    _saveBackupJobStatusViewModel.BackupJobFileTransfering = file.Name;
                    _saveBackupJobStatusViewModel.BackupJobFileTransferingCount = FileLeftSlashFileTotal(saveFiles.FileNumberLeft, saveFiles.FileNumberTotal);
                    _saveBackupJobStatusViewModel.BackupJobProgressBarValue = ProgressBarValue(saveFiles.FileSizeLeft, saveFiles.FileSizeTotal);

                    await using (FileStream sourceStream = new(file.Path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
                    {
                        await using FileStream destinationStream = new(file.Path.Replace(SourcePath, DestinationPath), FileMode.CreateNew, FileAccess.Write, FileShare.None, 4096, true);
                        await sourceStream.CopyToAsync(destinationStream);
                    }

                    saveFiles.FileSizeLeft -= file.Size;
                    saveFiles.FileNumberLeft--;
                });
            }
            DateTime end = DateTime.Now;
            saveFiles.FileTransferTime = end - start;
            return;
        }

        private async Task WriteToDailyLog(Log log)
        {
            if (Type == "0")
            {
                FileManager.WriteDailyLogJson(log);
                return;
            }
            FileManager.WriteDailyLogXml(log);
        }


        private Log CreateLog(SaveFiles saveFiles)
        {
            return new Log(Name, SourcePath, DestinationPath, Type, saveFiles.FileSizeTotal, saveFiles.FileNumberTotal, saveFiles.FileTransferTime, saveFiles.FileEncryptTime, DateTime.Now.ToString("F"));
        }



        private static float ProgressBarValue(long fileSizeLeft, long fileSizeTotal)
        {
            return (100 - (fileSizeLeft * 100 / fileSizeTotal));
        }

        private static string FileLeftSlashFileTotal(long fileLeft, long fileTotal)
        {
            return (fileTotal - fileLeft + 1) + "/" + fileTotal;
        }

        private static void AllSavesProgression(SaveAllBackupJobsViewModel saveAllBackupJobsViewModel, int total, int index)
        {
            saveAllBackupJobsViewModel.AllBackupJobProgression = index + "/" + total;
            saveAllBackupJobsViewModel.ProgressBarAllBackupJobsValue = index / total * 100;
        }
    }
}
