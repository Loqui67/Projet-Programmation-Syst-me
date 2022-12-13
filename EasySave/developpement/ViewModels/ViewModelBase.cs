using System.ComponentModel;

namespace EasySave.developpement.ViewModels
{
    ///Classe permettant de notifier les vues des changements de propriétés
    ///Class for notifying views of property changes
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        protected virtual void Dispose()
        {
        }
    }
}
