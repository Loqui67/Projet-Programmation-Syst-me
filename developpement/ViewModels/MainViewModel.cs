using AppWPF.developpement.Stores;

namespace AppWPF.developpement.ViewModels
{
    ///Classe héritant de ViewModelBase servant de classe principal
    ///Class inheriting from ViewModelBase serving as main class
    public class MainViewModel : ViewModelBase
    {
        ///Constructeur de la classe MainViewModel
        private readonly ModalNavigationStore _modalNavigationStore;

        ///Variable servant à stocket le nom de la page
        ///Variable used to store the name of the page
        public ViewModelBase CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;
        public bool IsModalOpen => _modalNavigationStore.IsOpen;

        public BackupJobsViewModel BackupJobsViewModel { get; }

        ///Méthode qui permet de changer de page
        ///Method to change page
        public MainViewModel(ModalNavigationStore modalNavigationStore, BackupJobsViewModel backupJobsViewModel)
        {
            _modalNavigationStore = modalNavigationStore;
            BackupJobsViewModel = backupJobsViewModel;

            _modalNavigationStore.CurrentViewModelChanged += ModalNavigationStore_CurrentViewModelChanged;
        }

        protected override void Dispose()
        {
            _modalNavigationStore.CurrentViewModelChanged -= ModalNavigationStore_CurrentViewModelChanged;
            base.Dispose();
        }
        private void ModalNavigationStore_CurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentModalViewModel));
            OnPropertyChanged(nameof(IsModalOpen));
        }
    }
}
