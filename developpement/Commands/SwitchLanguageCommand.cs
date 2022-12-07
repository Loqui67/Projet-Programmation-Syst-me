using AppWPF.developpement.Models;
using AppWPF.developpement.ViewModels;
using System;
using System.Windows;

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
