using AppWPF.developpement.Commands;
using AppWPF.developpement.Models;
using AppWPF.developpement.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppWPF.developpement.ViewModels
{
    public class ExtensionCryptageListingItemViewModel : ViewModelBase
    {
        public ExtensionCryptage ExtensionCryptage { get; private set; }
        public Guid ExtensionCryptageId => ExtensionCryptage.Id;
        public string Name => ExtensionCryptage.Name;

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

        public ExtensionCryptageListingItemViewModel(ExtensionCryptage extensionCryptage, ExtensionCryptageStore extensionCryptageStore)
        {
            ExtensionCryptage = extensionCryptage;
            DeleteCommand = new DeleteExtensionCryptageCommand(this, extensionCryptageStore);
        }
    }
}
