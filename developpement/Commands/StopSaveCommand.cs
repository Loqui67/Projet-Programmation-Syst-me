using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.developpement.Commands
{
    public class StopSaveCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        public StopSaveCommand(ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
            
        }

        public override void Execute(object parameter)
        {
            BackupJobsStore.StopSave();
            _modalNavigationStore.Close();
        }
    }
}
