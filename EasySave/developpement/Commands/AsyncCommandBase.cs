using System;
using System.Threading.Tasks;

namespace EasySave.developpement.Commands
{
    ///Class qui permet d'éxécuter une commande de maniere asynchrone.
    ///Class which allows to execute a command asynchronously.
    public abstract class AsyncCommandBase : CommandBase
    {
        ///Variable qui permet de vérifier si la méthode est en éxecution
        ///Variable that checks if the method is running
        private bool _isExecuting;

        ///Méthode qui permet de savoir si une commande est en éxecution
        ///Method which allows to know if a command is in execution
        public bool IsExecuting
        {
            get => _isExecuting;
            set
            {
                _isExecuting = value;
                OnCanExecuteChanged();
            }
        }

        ///Méthode qui vérifie si le parametre peut s'éxécuter
        ///Method that checks if the parameter can be executed
        public override bool CanExecute(object parameter)
        {
            return !IsExecuting && base.CanExecute(parameter);
        }

        ///Méthode qui permet d'éxecuter une commande
        ///Method to execute a command
        public override async void Execute(object parameter)
        {
            IsExecuting = true;
            try
            {
                await ExecuteAsync(parameter);
            }
            catch (Exception) { }
            finally
            {
                IsExecuting = false;
            }
        }

        public abstract Task ExecuteAsync(object parameter);
    }
}
