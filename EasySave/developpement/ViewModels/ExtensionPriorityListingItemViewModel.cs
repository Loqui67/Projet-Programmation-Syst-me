using EasySave.developpement.Commands;
using EasySave.developpement.Models;
using EasySave.developpement.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EasySave.developpement.ViewModels
{
    public class ExtensionPriorityListingItemViewModel : ViewModelBase
    {
        public ExtensionPriority ExtensionPriority { get; private set; }
        public Guid ExtensionPriorityId => ExtensionPriority.Id;
        public string Name => ExtensionPriority.Name;

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

        public ExtensionPriorityListingItemViewModel(ExtensionPriority extensionPriority, ExtensionPriorityStore extensionPriorityStore)
        {
            ExtensionPriority = extensionPriority;
            DeleteCommand = new DeleteExtensionPriorityCommand(this, extensionPriorityStore);
        }
    }
}
