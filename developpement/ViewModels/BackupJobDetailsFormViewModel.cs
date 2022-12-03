using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AppWPF.developpement.ViewModels
{
    public class BackupJobDetailsFormViewModel : ViewModelBase
    {
		private string _name;
		public string Name
		{
			get { return _name; }
			set { 
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

        public bool CanSubmit => 
            !string.IsNullOrWhiteSpace(Name) && 
            Directory.Exists(SourcePath) &&
            Directory.Exists(DestinationPath) && 
            !string.IsNullOrWhiteSpace(Type);
        
        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }
    }
}
