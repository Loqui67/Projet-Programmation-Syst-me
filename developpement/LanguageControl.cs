using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Reflection;
using System.Globalization;
using Projet_Programmation_Système.developpement;
using static Projet_Programmation_Système.developpement.ConsoleManager;

public class LanguageControl
{
    public static ResourceManager? _rm;
    private string language = "EN";

    public LanguageControl()
    {
        _rm = new ResourceManager("Projet_Programmation_Système.Ressources.language", Assembly.GetExecutingAssembly());
    }

    public string? GetLanguageString(string name)
    {
        return _rm?.GetString(name);
    }

    public void ChangeLanguage()
    {
        AskLanguage();
        DisplayEmptyLine();
        try
        {
            CultureInfo cultureInfo = new CultureInfo(language);
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;
            DisplayLanguage("ChangeLanguageSuccess"); 
        }
        catch (Exception)
        {
            DisplayLanguage("ChangeLanguageError");
        }
    }

    private void AskLanguage()
    {
        bool isValid = false;
        language = "EN";
        while (!isValid)
        {
            
            DisplayEmptyLine();
            DisplayArray(new string?[] { GetLanguageString("ChooseLanguage"), "FR : 1 - EN : 2" });
            isValid = true;
            switch (GetInput())
            {
                case "1":
                    Display("FR");
                    language = "fr";
                    break;

                case "2":
                    Display("EN");
                    language = "en";
                    break;
                    
                default:
                    DisplayLanguage("InvalidInput");
                    language = "error";
                    isValid = false;
                    break;
            }
        }
    }
}

