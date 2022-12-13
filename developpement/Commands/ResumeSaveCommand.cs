using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.developpement.Commands
{
    public class ResumeSaveCommand : CommandBase
    {
        public ResumeSaveCommand()
        {
        }

        public override void Execute(object parameter)
        {
            BackupJobsStore.ResumeSave();
        }

    }
}
