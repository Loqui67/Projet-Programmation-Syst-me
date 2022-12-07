using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;
using System;
using System.Threading.Tasks;

namespace AppWPF.developpement.Commands
{
    public class LoadProcessusCommand : AsyncCommandBase
    {
        private readonly SettingsViewModel _settingsViewModel;
        private readonly ProcessusStore _processusStore;

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
