using AppWPF.developpement.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.developpement.Commands
{
    public class LoadBackupJobsCommand : AsyncCommandBase
    {
        private readonly BackupJobsStore _backupJobsStore;


        public LoadBackupJobsCommand(BackupJobsStore backupJobsStore)
        {
            _backupJobsStore = backupJobsStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _backupJobsStore.Load();
            }
            catch (Exception) { }
        }
    }
}
