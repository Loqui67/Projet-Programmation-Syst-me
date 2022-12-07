using AppWPF.developpement.Commands;
using AppWPF.developpement.Models;
using AppWPF.developpement.Stores;
using System;
using System.Windows.Input;

namespace AppWPF.developpement.ViewModels
{
    ///Classe permettant de gérer les données d'un processus
    ///Class used to manage the data of a process
    public class ProcessusListingItemViewModel : ViewModelBase
    {
        ///Instance du processus
        ///Process instance
        public Processus Processus { get; private set; }
        ///Variable permettant de transférer à la vue l'id du processus
        ///Variable used to transfer the process id to the view
        public Guid ProcessusId => Processus.Id;
        ///Variable permettant de transférer à la vue le nom du processus
        ///Variable used to transfer the process name to the view
        public string Name => Processus.Name;
        ///Variable privée qui contiens l'information de si un processus est entrain de se supprimer ou pas
        ///Private variable that contains the information of if a process is being deleted or not
        private bool _isDeleting;
        ///Variable public qui contiens l'information de si un processus est entrain de se supprimer ou pas
        ///Public variable that contains the information of if a process is being deleted or not
        public bool IsDeleting
        {
            get => _isDeleting;
            set
            {
                _isDeleting = value;
                OnPropertyChanged(nameof(IsDeleting));
            }
        }

        ///Variable qui permet de savoir si un processus est entrain d'être supprimé ou non
        ///Variable that allows to know if a process is being deleted or not
        public ICommand DeleteCommand { get; }

        public ProcessusListingItemViewModel(Processus processus, ProcessusStore processusStore)
        {
            Processus = processus;
            DeleteCommand = new DeleteProcessusCommand(this, processusStore);
        }
    }
}
