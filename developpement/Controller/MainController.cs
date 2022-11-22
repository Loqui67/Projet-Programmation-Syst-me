using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Programmation_Système.developpement.View;
using Programmation_Système.developpement.Model;
using System.Collections.Concurrent;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace Programmation_Système.developpement.Controller
{
    public class MainController
    {
        private MainModel model;
        private MainView view;
        private static ResourceManager? _rm;
        private string language = "en";

        public MainController()
        {
            _rm = new ResourceManager("Projet_Programmation_Système.Ressources.language", Assembly.GetExecutingAssembly());
            view = new MainView();
            model = new MainModel();
            AskLanguage();
            model.ChangeLanguage(language);
        }
        
        public string? GetString(string name)
        {
            return _rm?.GetString(name);
        }

        private void AskLanguage()
        {
            bool isValid = false;
            language = "en";
            while (!isValid)
            {
                view.DisplayArray(new string[] { "Choose a language: ", "FR : 1 - EN : 2" });
                switch (view.GetInput())
                {
                    case "1":
                        view.Display("FR");
                        language = "fr";
                        isValid = true;
                        break;

                    case "2":
                        view.Display("EN");
                        language = "en";
                        isValid = true;
                        break;

                    default:
                        view.Display("Invalid input");
                        language = "error";
                        break;
                }
            }
        }
    }
}
