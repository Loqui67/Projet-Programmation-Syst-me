using EasySave.developpement.ViewModels;
using System;

namespace EasySave.developpement.Stores
{
    public class ModalNavigationStore
    {
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                CurrentViewModelChanged?.Invoke();
            }
        }

        public void Close()
        {
            CurrentViewModel = null;
        }

        public bool IsOpen => CurrentViewModel != null;
        public event Action CurrentViewModelChanged;
    }
}
