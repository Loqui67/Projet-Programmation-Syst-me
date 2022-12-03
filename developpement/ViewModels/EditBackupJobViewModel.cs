using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.developpement.ViewModels
{
    public class EditBackupJobViewModel
    {
        public BackupJobDetailsFormViewModel BackupJobDetailsFormViewModel { get; }

        public EditBackupJobViewModel()
        {
            BackupJobDetailsFormViewModel = new BackupJobDetailsFormViewModel();
        }
    }
}
