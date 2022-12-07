using AppWPF.developpement.Stores;
using AppWPF.developpement.ViewModels;
using System;
using System.Threading.Tasks;

namespace AppWPF.developpement.Commands
{
    public class LoadProcessusCommand : AsyncCommandBase
    {
        private readonly ProcessusStore _processusStore;

        public LoadProcessusCommand(ProcessusStore processusStore)
        {
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
