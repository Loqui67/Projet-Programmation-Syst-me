using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AppWPF.developpement.Commands
{
    public class SaveAllBackupJobsCommand : AsyncCommandBase
    {
        private readonly BackupJobsStore _backupJobsStore;

        public SaveAllBackupJobsCommand(BackupJobsStore backupJobsStore)
        {
            _backupJobsStore = backupJobsStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                Trace.WriteLine("SaveAllBackupJobsCommand.ExecuteAsync");
                //await _backupJobsStore.SaveAll();
            }
            catch (Exception) { }
        }
    }
}
