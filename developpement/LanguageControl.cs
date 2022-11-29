using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Reflection;
using System.Globalization;
using Projet_Programmation_Système.developpement;

//Création d'une classe pour gérer les langues.
public class LanguageControl
{
    //Création d'une variable pour stocker la langue.
    public static ResourceManager? _rm;
    private string language = "EN";

    public LanguageControl()
    {
    //Recherche du fichier de langue dans le projet.
        _rm = new ResourceManager("Projet_Programmation_Système.Ressources.language", Assembly.GetExecutingAssembly());
    }

    // Recherche de la variable qui lui correspond en parametre en fonction de la langue.
    public string? GetLanguageString(string name)
    {
        return _rm?.GetString(name);
    }
    //Création d'une méthode pour changer la langue.
    public void ChangeLanguage()
    {
        AskLanguage();
        //Laisse une ligne vide
        ConsoleManager.DisplayEmptyLine();
        try
        {
            CultureInfo cultureInfo = new CultureInfo(language);
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;
            //Affiche "ChangeLanguageSuccess" si le changement de langue est un succès.
            ConsoleManager.DisplayLanguage("ChangeLanguageSuccess"); 
        }
        catch (Exception)
        {
        ConsoleManager.DisplayLanguage("ChangeLanguageError");
            //Affiche "ChangeLanguageError" si le changement de langue est un echec.
        }
    }
    //Méthode qui vise à obtenir le choix de l'utilisateur pour la langue.
    private void AskLanguage()
    {
        bool isValid = false;
        language = "EN";
        while (!isValid)
        {
            ConsoleManager.DisplayEmptyLine();
            ConsoleManager.DisplayArray(new string?[] { GetLanguageString("ChooseLanguage"), "FR : 1 - EN : 2" });
            isValid = true;
            switch (ConsoleManager.GetInput())
            {
                //Si l'utilisateur appuie sur "1", la langue sera le français.
                case "1":
                    ConsoleManager.Display("FR");
                    language = "fr";
                    break;

                //Si l'utilisateur appuie sur "2", la langue sera l'anglais.
                case "2":
                    ConsoleManager.Display("EN");
                    language = "en";
                    break;

                //Si l'utilisateur appuie sur une autre touche, une erreur s'affiche.
                default:
                    ConsoleManager.DisplayLanguage("InvalidInput");
                    language = "error";
                    isValid = false;
                    break;
            }
        }
    }
}

