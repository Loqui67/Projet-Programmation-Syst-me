using EasySave.developpement.Stores;
using EasySave.developpement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave.developpement.Commands
{
    public class PauseSaveCommand : CommandBase
    {
        public PauseSaveCommand()
        {
        }

        public override void Execute(object parameter)
        {
            BackupJobsStore.PauseSave();
        }
    }
}
