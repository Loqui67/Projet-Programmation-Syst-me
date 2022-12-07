using AppWPF.developpement.Models;
using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;
using System.Threading.Tasks;

namespace AppWPF.developpement.Commands
{
    ///Classe qui permet d'ajouter un processus
    ///Class that allows to add a process
    public class AddProcessCommand : AsyncCommandBase
    {
        ///Variable qui permet de créer des instances d'autre classe, ici SettingsViewModel
        ///Variable that allows you to create instances of another class, here SettingsViewModel
        private SettingsViewModel _settingsViewModel;
        private ProcessusStore _processusStore;

        ///Méthode qui permet d'ajouter un processus.
        ///Method to add a process.
        public AddProcessCommand(SettingsViewModel settingsViewModel, ProcessusStore processusStore)
        {
            _settingsViewModel = settingsViewModel;
            _processusStore = processusStore;
        }

        ///Méthode qui permet d'éxécuter de maniere asymétrique.
        ///Method that allows you to execute asymmetrically.
        public override async Task ExecuteAsync(object parameter)
        {
            if (!string.IsNullOrEmpty(_settingsViewModel.ProcessToAdd))
            {
                await _processusStore.Add(new Processus(_settingsViewModel.ProcessToAdd));
                _settingsViewModel.ProcessToAdd = "";
            }
        }
    }
}