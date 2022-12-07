using AppWPF.developpement.Models;
using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;

namespace AppWPF.developpement.Commands
{
    ///Classe qui permet de soumettre les parametres rentrés
    ///Class that allows to submit the entered parameters
    public class SubmitSettingsCommand : CommandBase
    {
        ///Variables qui permettent d'instancier plusieurs classes
        ///Variables that allow to instantiate several classes
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly SettingsViewModel _settingsViewModel;
        private readonly ProcessusStore _processusStore;

        ///Méthode qui permet de soumettre les parametres rentrés
        ///Method which allows to submit the entered parameters
        public SubmitSettingsCommand(ModalNavigationStore modalNavigationStore, SettingsViewModel settingsViewModel, ProcessusStore processusStore)
        {
            _modalNavigationStore = modalNavigationStore;
            _settingsViewModel = settingsViewModel;
            _processusStore = processusStore;
        }

        public override void Execute(object parameter)
        {
            BackupJobsViewModel.config.LogExtension = _settingsViewModel.LogExtension;
            FileManager.SaveConfig(BackupJobsViewModel.config);
            _modalNavigationStore.Close();
        }
    }
}
