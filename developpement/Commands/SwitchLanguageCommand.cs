using AppWPF.developpement.Models;
using AppWPF.developpement.ViewModels;
using System;
using System.Windows;

namespace AppWPF.developpement.Commands
{
    ///Classe qui permet de changer la langue
    ///Class that allows you to change the language
    public class SwitchLanguageCommand : CommandBase
    {
        ///Variable qui permet de contenir la langue
        ///Variable that allows to contain the language
        private string _language;

        ///Méthode qui permet de changer la langue
        ///Method to change the language
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
