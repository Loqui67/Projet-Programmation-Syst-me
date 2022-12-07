using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;
using System;
using System.Threading.Tasks;

namespace AppWPF.developpement.Commands
{
    ///Classe qui permet de charger un processus
    ///Class that allows to load a process
    public class LoadProcessusCommand : AsyncCommandBase
    {
        ///Variables qui instancient plusieurs classes
        ///Variables that instantiate multiple classes
        private readonly SettingsViewModel _settingsViewModel;
        private readonly ProcessusStore _processusStore;

        ///Méthode qui permet de charger un processus 
        ///Method to load a process
        public LoadProcessusCommand(SettingsViewModel settingsViewModel, ProcessusStore processusStore)
        {
            _settingsViewModel = settingsViewModel;
            _processusStore = processusStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _processusStore.Load();
            }
            catch (Exception) { }
        }
    }
}
