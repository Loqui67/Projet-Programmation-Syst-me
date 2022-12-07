using AppWPF.developpement.Models;
using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;

namespace AppWPF.developpement.Commands
{
    public class SubmitSettingsCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly SettingsViewModel _settingsViewModel;
        private readonly ProcessusStore _processusStore;

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
