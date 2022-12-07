using AppWPF.developpement.Models;
using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.developpement.Commands
{
    public class AddExtensionCryptageCommand : AsyncCommandBase
    {
        private readonly SettingsViewModel _settingsViewModel;
        private readonly ExtensionCryptageStore _extensionCryptageStore;

        public AddExtensionCryptageCommand(SettingsViewModel settingsViewModel, ExtensionCryptageStore extensionCryptageStore)
        {
            _settingsViewModel = settingsViewModel;
            _extensionCryptageStore = extensionCryptageStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                if (!string.IsNullOrEmpty(_settingsViewModel.ExtensionCryptageToAdd) && Path.GetExtension(_settingsViewModel.ExtensionCryptageToAdd) != "") 
                    await _extensionCryptageStore.Add(new ExtensionCryptage(_settingsViewModel.ExtensionCryptageToAdd));
            } catch (Exception) { }
            _settingsViewModel.ExtensionCryptageToAdd = "";
        }
    }
}
