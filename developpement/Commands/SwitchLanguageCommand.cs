using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AppWPF.developpement.Models;
using AppWPF.developpement.ViewModels;
using AppWPF.developpement.Views;

namespace AppWPF.developpement.Commands
{
    public class SwitchLanguageCommand : CommandBase
    {
        private string _language;

        public SwitchLanguageCommand(string language)
        {
            _language = language;
        }

        public override void Execute(object parameter)
        {
            ResourceDictionary dictionary = new ResourceDictionary();
            switch (_language)
            {
                case "en":
                    dictionary.Source = new Uri("../../Ressources/Language/DictionaryResources.en.xaml", UriKind.Relative);
                    BackupJobsViewModel.config.DefaultLanguage = "en";
                    FileManager.SaveConfig(BackupJobsViewModel.config);
                    break;
                case "fr":
                    dictionary.Source = new Uri("../../Ressources/Language/DictionaryResources.fr.xaml", UriKind.Relative);
                    BackupJobsViewModel.config.DefaultLanguage = "fr";
                    FileManager.SaveConfig(BackupJobsViewModel.config);
                    break;
                default:
                    dictionary.Source = new Uri("../../Ressources/Language/DictionaryResources.en.xaml", UriKind.Relative);
                    BackupJobsViewModel.config.DefaultLanguage = "en";
                    FileManager.SaveConfig(BackupJobsViewModel.config);
                    break;
            }
            Application.Current.Resources.MergedDictionaries.Add(dictionary);
        }
        
    }
}
