using EasySave.developpement.Stores;
using EasySave.developpement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave.developpement.Commands
{
    public class LoadExtensionCryptageCommand : AsyncCommandBase
    {
        private readonly ExtensionCryptageStore _extensionCryptageStore;

        public LoadExtensionCryptageCommand(ExtensionCryptageStore extensionCryptageStore)
        {
            _extensionCryptageStore = extensionCryptageStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _extensionCryptageStore.Load();
            }
            catch (Exception) { }
        }
    }
}
