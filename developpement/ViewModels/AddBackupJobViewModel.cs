using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.developpement.ViewModels
{
    public class AddBackupJobViewModel : ViewModelBase
    {
        public BackupJobDetailsFormViewModel BackupJobDetailsFormViewModel { get; }

        public AddBackupJobViewModel()
        {
            BackupJobDetailsFormViewModel = new BackupJobDetailsFormViewModel();
        }
    }
}
