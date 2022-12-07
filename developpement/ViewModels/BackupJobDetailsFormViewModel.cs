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

        ///Variable privée permettant de vérifier le chemin d'accès du dossier source
        ///Private variable to check the source folder path
        private string _sourcePath;
        ///Variable publique permettant de vérifier le chemin d'accès du dossier source
        ///Public variable to check the source folder path
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

        ///Variable privée permettant de vérifier le chemin d'accès du dossier de destination
        ///Private variable to check the destination folder path
        private string _destinationPath;
        ///Variable publique permettant de vérifier le chemin d'accès du dossier de destination
        ///Public variable to check the destination folder path
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

        ///Variable privée permettant de vérifier le type de fichier
        ///Variable private to check the file type
        private string _type;
        ///Variable publique permettant de vérifier le type de fichier
        ///Public variable to check the file type
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

        ///Variable privée permettant de vérifier si le fichier est bien parvenue 
        ///Private variable to check if the file has arrived
        private bool _isSubmitting;
        ///Variable privée permettant de vérifier si le fichier est bien parvenue 
        ///Private variable to check if the file has arrived
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

        ///Variable permettant de vérifier si le fichier peut etre envoyé
        ///Variable to check if the file can be sent
        public bool CanSubmit =>
            !string.IsNullOrWhiteSpace(Name) &&
            Directory.Exists(SourcePath) &&
            Directory.Exists(DestinationPath) &&
            !string.IsNullOrWhiteSpace(Type);

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        ///Méthode permettant de donner les détails du travail de sauvegarde
        ///Method to give the details of the backup job
        public BackupJobDetailsFormViewModel(ICommand submitCommand, ICommand cancelCommand)
        {
            SubmitCommand = submitCommand;
            CancelCommand = cancelCommand;
        }
    }
}
