using System;
using System.Windows.Input;

namespace IHM_Client.Commands
{
    ///Class qui permet de mettre en place la base des commandes
    ///Class which allows to set up the base of the commands
    public abstract class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;

        ///Méthode qui vérifie si la commande est en éxecution.
        ///Method that checks if the command is running.
        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public abstract void Execute(object parameter);

        protected virtual void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
