using AppWPF.developpement.Models;
using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;
using System.Threading.Tasks;

namespace AppWPF.developpement.Commands
{
    public class AddProcessCommand : AsyncCommandBase
    {
        private SettingsViewModel _settingsViewModel;
        private ProcessusStore _processusStore;

        public AddProcessCommand(SettingsViewModel settingsViewModel, ProcessusStore processusStore)
        {
            _settingsViewModel = settingsViewModel;
            _processusStore = processusStore;
        }

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