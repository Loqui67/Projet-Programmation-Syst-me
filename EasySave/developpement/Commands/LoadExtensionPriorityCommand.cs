using EasySave.developpement.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave.developpement.Commands
{
    public class LoadExtensionPriorityCommand : AsyncCommandBase
    {
        private readonly ExtensionPriorityStore _extensionPriorityStore;

        public LoadExtensionPriorityCommand(ExtensionPriorityStore extensionPriorityStore)
        {
            _extensionPriorityStore = extensionPriorityStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _extensionPriorityStore.Load();
            }
            catch (Exception) { }
        }
    }
}
