using AppWPF.developpement.Commands;
using AppWPF.developpement.Models;
using AppWPF.developpement.Stores;
using System;
using System.Windows.Input;

namespace AppWPF.developpement.ViewModels
{
    public class ProcessusListingItemViewModel : ViewModelBase
    {
        public Processus Processus { get; private set; }
        public Guid ProcessusId => Processus.Id;
        public string Name => Processus.Name;

        private bool _isDeleting;
        public bool IsDeleting
        {
            get => _isDeleting;
            set
            {
                _isDeleting = value;
                OnPropertyChanged(nameof(IsDeleting));
            }
        }

        public ICommand DeleteCommand { get; }

        public ProcessusListingItemViewModel(Processus processus, ProcessusStore processusStore)
        {
            Processus = processus;
            DeleteCommand = new DeleteProcessusCommand(this, processusStore);
        }
    }
}
