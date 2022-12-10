using AppWPF.developpement.Models;
using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.developpement.Commands
{
    public class AddExtensionPriorityCommand : AsyncCommandBase
    {
        private readonly SettingsViewModel _settingsViewModel;
        private readonly ExtensionPriorityStore _extensionPriorityStore;

        public AddExtensionPriorityCommand(SettingsViewModel settingsViewModel, ExtensionPriorityStore extensionPriorityStore)
        {
            _settingsViewModel = settingsViewModel;
            _extensionPriorityStore = extensionPriorityStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                if (!string.IsNullOrEmpty(_settingsViewModel.ExtensionPriorityToAdd) && Path.GetExtension(_settingsViewModel.ExtensionPriorityToAdd) != "")
                    await _extensionPriorityStore.Add(new ExtensionPriority(_settingsViewModel.ExtensionPriorityToAdd));
            }
            catch (Exception) { }
            _settingsViewModel.ExtensionPriorityToAdd = "";
        }
    }
}
