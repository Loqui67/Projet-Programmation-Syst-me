using EasySave.developpement.Stores;
using EasySave.developpement.ViewModels;
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

namespace EasySave.developpement.Models
{
    public static class BackupJobSaver
    {
        public static long fileSizeLeft = 0;
        public static long fileNumberLeft = 0;
        public static long fileSizeTotal = 0;
        public static long fileNumberTotal = 0;

        public static SaveBackupJobStatusViewModel saveBackupJobStatusViewModel;
        public static SaveBackupJobViewModel saveBackupJobViewModel;
        public static ModalNavigationStore modalNavigationStore;
        public static SaveAllBackupJobsViewModel saveAllBackupJobsViewModel;

        public static bool isPausing = false;
        public static bool isStoppingEncrypting = false;
        public static bool isStoppingCopying = false;


        public static async Task StartSave(SaveFiles saveFiles)
        {
            isPausing = false;
            await CreateDirectories(saveFiles);
            Thread t = new(async () =>
            {
                fileNumberTotal = saveFiles.FileNumberTotal;
                fileSizeTotal = saveFiles.FileSizeTotal;
                fileNumberLeft = fileNumberTotal;
                fileSizeLeft = fileSizeTotal;
                
                Action action = () => SetIsSaving(true);
                action.Invoke();
                await Save(saveFiles, false);
                List<Log> logs = new()
                {
                    CreateLog(saveFiles)
                };
                await WriteToDailyLog(logs);
                action = () =>
                {
                    SetIsSaving(false);
                    SetIsPaused(false);
                    modalNavigationStore.Close();

                    isPausing = false;
                    isStoppingEncrypting = false;
                    isStoppingCopying = false;

                    Server.Send("saveDone", null, null);
                };
                action.Invoke();
            });
            t.Start();
        }
        
        public static async Task<Log> StartListSaveInParallel(SaveFiles saveFiles)
        {
            await CreateDirectories(saveFiles);
            await Save(saveFiles, true);
            return CreateLog(saveFiles);
        }

        private static async Task Save(SaveFiles saveFiles, bool AllSave)
        {
            await Task.WhenAll(EncryptFiles(saveFiles, true, AllSave), CopyFiles(saveFiles, true, AllSave));
            await Task.WhenAll(EncryptFiles(saveFiles, false, AllSave), CopyFiles(saveFiles, false, AllSave));
        }

        public static void PauseSave()
        {
            SetIsPaused(true);
            SetIsPausedAllSave(true);
            isPausing = true;
        }

        public static void ResumeSave()
        {
            SetIsPaused(false);
            SetIsPausedAllSave(false);
            isPausing = false;
        }

        public static void StopSave()
        {
            isStoppingEncrypting = true;
            isStoppingCopying = true;
        }



        private static async Task CreateDirectories(SaveFiles saveFiles)
        {
            foreach (string directory in saveFiles.Directories)
            {
                string folderDestinationPath = directory.Replace(saveFiles.SourcePath, saveFiles.DestinationPath);
                if (!Directory.Exists(folderDestinationPath))
                {
                    Directory.CreateDirectory(folderDestinationPath);
                }
            }
        }

        private static async Task EncryptFiles(SaveFiles saveFiles, bool isPriority, bool AllSave)
        {
            List<FilesInfo> encrypt = new();
            if (isPriority == true)
            {
                encrypt = saveFiles.FilesToEncryptAndPriority;
            }
            else
            {
                encrypt = saveFiles.FilesToEncrypt;
            }
            
            if (encrypt.Count == 0)
            {
                saveFiles.FileEncryptTime = new TimeSpan(0);
                return;
            }
            try
            {
                DateTime start = DateTime.Now;
                foreach (FilesInfo fileToEncrypt in encrypt)
                {
                    await Task.Run(() =>
                    {
                        string path = "\"" + fileToEncrypt.Path + "\"" + " " + "\"" + fileToEncrypt.Path.Replace(saveFiles.SourcePath, saveFiles.DestinationPath) + "\"";
                        ProcessStartInfo process = new("..\\..\\..\\CryptoSoft\\CryptoSoft.exe")
                        {
                            Arguments = path
                        };
                        Process.Start(process);
                    });
                    fileSizeLeft -= fileToEncrypt.Size;
                    fileNumberLeft--;
                    
                    HandlePause();
                    if (isStoppingEncrypting) return;
                    
                }
                DateTime end = DateTime.Now;
                saveFiles.FileEncryptTime = end - start;
            }
            catch (Exception)
            {
                saveFiles.FileEncryptTime = new TimeSpan(-1);
            }
        }


        private static async Task CopyFiles(SaveFiles saveFiles, bool isPriority, bool AllSave)
        {
            List<FilesInfo> files = new();
            if (isPriority == true)
            {
                files = saveFiles.FilesPriority;
            }
            else
            {
                files = saveFiles.Files;
            }

            DateTime start = DateTime.Now;
            
            foreach (FilesInfo file in files)
            {
                await Task.Run(async () =>
                {
                    if (!AllSave)
                    {
                        saveBackupJobStatusViewModel.BackupJobFileTransfering = file.Name;
                        saveBackupJobStatusViewModel.BackupJobFileTransferingCount = FileLeftSlashFileTotal();
                        saveBackupJobStatusViewModel.BackupJobProgressBarValue = ProgressBarValue();
                    } else {
                        saveAllBackupJobsViewModel.ProgressBarAllBackupJobsValue = ProgressBarValue();
                        saveAllBackupJobsViewModel.AllBackupJobProgression = FileLeftSlashFileTotal();
                    }
                    Server.Send("progress", FileLeftSlashFileTotal(), ProgressBarValue());

                    await using (FileStream sourceStream = new(file.Path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
                    {
                        try
                        {
                            await using FileStream destinationStream = new(file.Path.Replace(saveFiles.SourcePath, saveFiles.DestinationPath), FileMode.Create, FileAccess.Write, FileShare.None, 4096, true);
                            await sourceStream.CopyToAsync(destinationStream);
                        }
                        catch (Exception) { }
                    }
                });
                fileSizeLeft -= file.Size;
                fileNumberLeft--;

                HandlePause();
                if (isStoppingCopying) return;
            }
            DateTime end = DateTime.Now;
            saveFiles.FileTransferTime = end - start;
            return;
        }

        private static void HandlePause()
        {
            while (isPausing)
            {
                if (isStoppingEncrypting || isStoppingCopying) return;
                Thread.Sleep(1000);
            }
        }

        public static async Task WriteToDailyLog(List<Log> logs)
        {
            if (BackupJob.LogExtension == "0")
            {
                FileManager.WriteDailyLogJson(logs);
                return;
            }
            FileManager.WriteDailyLogXml(logs);
        }


        private static Log CreateLog(SaveFiles saveFiles)
        {
            return new Log(saveFiles.Name, saveFiles.SourcePath, saveFiles.DestinationPath, saveFiles.Type, saveFiles.FileSizeTotal, saveFiles.FileNumberTotal, saveFiles.FileTransferTime, saveFiles.FileEncryptTime, DateTime.Now.ToString("F"));
        }



        private static float ProgressBarValue()
        {
            return (100 - (fileSizeLeft * 100 / fileSizeTotal));
        }

        private static string FileLeftSlashFileTotal()
        {
            return (fileNumberTotal - fileNumberLeft + 1) + "/" + fileNumberTotal;
        }

        private static void SetIsSaving(bool isSaving)
        {
            saveBackupJobViewModel.IsSaving = isSaving;
        }

        private static void SetIsPaused(bool isPaused)
        {
            if (saveBackupJobViewModel == null) return;
            saveBackupJobViewModel.IsPaused = isPaused;
        }

        public static void SetIsPausedAllSave(bool isPaused)
        {
            if (saveAllBackupJobsViewModel == null) return;
            saveAllBackupJobsViewModel.IsPaused = isPaused;
        }
    }
}
