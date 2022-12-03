using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppWPF.developpement.Commands
{
    public class OpenAddBackupJobCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenAddBackupJobCommand(ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter)
        {
            AddBackupJobViewModel addBackupJobViewModel = new AddBackupJobViewModel(_modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = addBackupJobViewModel;
        }

    }
}
