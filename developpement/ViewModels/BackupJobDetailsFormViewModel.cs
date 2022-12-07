using System.IO;
using System.Windows.Input;

namespace AppWPF.developpement.ViewModels
{
    ///Classer servant à la gestion des données de la fenêtre de détails d'un travail de sauvegarde
    ///File used for data management of the backup job details window
    public class BackupJobDetailsFormViewModel : ViewModelBase
    {
        ///Création des variables servant à la gestion des données de la fenêtre de détails d'un travail de sauvegarde
        ///Creating Variables Used to Manage Backup Job Details Window Data
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }

        private string _sourcePath;
        public string SourcePath
        {
            get { return _sourcePath; }
            set
            {
                _sourcePath = value;
                OnPropertyChanged(nameof(SourcePath));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }

        private string _destinationPath;
        public string DestinationPath
        {
            get { return _destinationPath; }
            set
            {
                _destinationPath = value;
                OnPropertyChanged(nameof(DestinationPath));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }

        private string _type;
        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged(nameof(Type));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }

        private bool _isSubmitting;
        public bool IsSubmitting
        {
            get { return _isSubmitting; }
            set
            {
                _isSubmitting = value;
                OnPropertyChanged(nameof(IsSubmitting));
                OnPropertyChanged(nameof(CanSubmit));
            }
        }

        public bool CanSubmit =>
            !string.IsNullOrWhiteSpace(Name) &&
            Directory.Exists(SourcePath) &&
            Directory.Exists(DestinationPath) &&
            !string.IsNullOrWhiteSpace(Type);

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public BackupJobDetailsFormViewModel(ICommand submitCommand, ICommand cancelCommand)
        {
            SubmitCommand = submitCommand;
            CancelCommand = cancelCommand;
        }
    }
}
