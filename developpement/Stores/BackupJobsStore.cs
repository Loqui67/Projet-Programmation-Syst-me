using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.developpement.Stores
{
    public class BackupJobsStore
    {
        public event Action<BackupJob> BackupJobAdded;
        public event Action<BackupJob> BackupJobUpdated;

        public async Task Add(BackupJob backupJob)
        {
            BackupJobAdded?.Invoke(backupJob);
        }

        public async Task Update(BackupJob backupJob)
        {
            BackupJobUpdated?.Invoke(backupJob);
        }
    }
}
