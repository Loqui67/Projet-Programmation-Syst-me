using AppWPF.developpement.Stores;
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
    public static class BackupJobSaver
    {
        public static long fileSizeLeft = 0;
        public static long fileNumberLeft = 0;

        public static SaveBackupJobStatusViewModel saveBackupJobStatusViewModel;
        public static SaveBackupJobViewModel saveBackupJobViewModel;
        public static ModalNavigationStore modalNavigationStore;
        private static BackupJob BackupJob { get; set; }

        private static string Name => BackupJob.Name;
        private static string SourcePath => BackupJob.SourcePath;
        private static string DestinationPath => BackupJob.DestinationPath;
        private static string Type => BackupJob.Type;

        private static Thread t;

        private static bool isPausing = false;
        private static bool isStoppingEncrypting = false;
        private static bool isStoppingCopying = false;


        public static async Task StartSave(SaveFiles saveFiles, BackupJob backupJob)
        {
            BackupJob = backupJob;
            await CreateDirectories(saveFiles.Directories);
            //SetIsSaving(true);
            t = new Thread(async () => {
                Action action = () => SetIsSaving(true);
                action.Invoke();
                await Task.WhenAll(EncryptFiles(saveFiles, true), CopyFiles(saveFiles, true));
                await Task.WhenAll(EncryptFiles(saveFiles, false), CopyFiles(saveFiles, false));
                await WriteToDailyLog(CreateLog(saveFiles));
                action = () => { 
                    SetIsSaving(false); 
                    SetIsPaused(false);
                    modalNavigationStore.Close();

                    isPausing = false;
                    isStoppingEncrypting = false;
                    isStoppingCopying = false;
                };
                action.Invoke();
            });
            t.Start();
        }

        public static void PauseSave()
        {
            SetIsPaused(true);
            isPausing = true;
        }

        public static void ResumeSave()
        {
            SetIsPaused(false);
            isPausing = false;
        }

        public static void StopSave()
        {
            isStoppingEncrypting = true;
            isStoppingCopying = true;
        }

        public static async Task StartListSaveInParallel(List<SaveFiles> saveFilesList, SaveAllBackupJobsViewModel saveAllBackupJobsViewModel, List<BackupJob> backupJobs)
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

        private static async Task CreateDirectories(List<string> directories)
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

        private static async Task EncryptFiles(SaveFiles saveFiles, bool isPriority)
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
                        string path = "\"" + fileToEncrypt.Path + "\"" + " " + "\"" + fileToEncrypt.Path.Replace(SourcePath, DestinationPath) + "\"";
                        ProcessStartInfo process = new ProcessStartInfo("..\\..\\..\\CryptoSoft\\CryptoSoft.exe")
                        {
                            Arguments = path
                        };
                        Process.Start(process);
                    });
                    saveFiles.FileSizeLeft -= fileToEncrypt.Size;
                    saveFiles.FileNumberLeft--;
                    
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


        private static async Task CopyFiles(SaveFiles saveFiles, bool isPriority)
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
                    saveBackupJobStatusViewModel.BackupJobFileTransfering = file.Name;
                    saveBackupJobStatusViewModel.BackupJobFileTransferingCount = FileLeftSlashFileTotal(saveFiles.FileNumberLeft, saveFiles.FileNumberTotal);
                    saveBackupJobStatusViewModel.BackupJobProgressBarValue = ProgressBarValue(saveFiles.FileSizeLeft, saveFiles.FileSizeTotal);

                    await using (FileStream sourceStream = new FileStream(file.Path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
                    {
                        try
                        {
                            await using FileStream destinationStream = new FileStream(file.Path.Replace(SourcePath, DestinationPath), FileMode.Create, FileAccess.Write, FileShare.None, 4096, true);
                            await sourceStream.CopyToAsync(destinationStream);
                        }
                        catch (Exception) { }
                    }
                });
                saveFiles.FileSizeLeft -= file.Size;
                saveFiles.FileNumberLeft--;

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

        private static async Task WriteToDailyLog(Log log)
        {
            if (Type == "0")
            {
                FileManager.WriteDailyLogJson(log);
                return;
            }
            FileManager.WriteDailyLogXml(log);
        }


        private static Log CreateLog(SaveFiles saveFiles)
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

        private static void SetIsSaving(bool isSaving)
        {
            saveBackupJobViewModel.IsSaving = isSaving;
        }

        private static void SetIsPaused(bool isPaused)
        {
            saveBackupJobViewModel.IsPaused = isPaused;
        }
    }
}
