using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Reflection;
using System.Globalization;
using Projet_Programmation_Système.developpement;

public class LanguageControl
    {
        //private ConsoleManager consoleManager;
        public static ResourceManager? _rm;
        private string language = "EN";

        public LanguageControl()
        {
        //ConsoleManager.languageControl = this;
        //consoleManager = Main.consoleManager;
        _rm = new ResourceManager("Projet_Programmation_Système.Ressources.language", Assembly.GetExecutingAssembly());
        }

        public string? GetString(string name)
        {
            return _rm?.GetString(name);
        }

        public void ChangeLanguage()
        {
            AskLanguage();
            try
            {
                var cultureInfo = new CultureInfo(language);
                CultureInfo.CurrentCulture = cultureInfo;
                CultureInfo.CurrentUICulture = cultureInfo;
                ConsoleManager.DisplayLanguage("ChangeLanguageSuccess"); 
            }
            catch (Exception e)
            {
                ConsoleManager.DisplayLanguage("ChangeLanguageError");
            }
        }

        private void AskLanguage()
        {
            bool isValid = false;
            language = "EN";
            while (!isValid)
            {
                ConsoleManager.DisplayArray(new string?[] { GetString("ChooseLanguage"), "FR : 1 - EN : 2" });
                switch (ConsoleManager.GetInput())
                {
                    case "1":
                        ConsoleManager.Display("FR");
                        language = "fr";
                        isValid = true;
                        break;

                    case "2":
                        ConsoleManager.Display("EN");
                        language = "en";
                        isValid = true;
                        break;

                    default:
                        ConsoleManager.DisplayLanguage("InvalidInput");
                        language = "error";
                        break;
                }
            }
        }
    }

