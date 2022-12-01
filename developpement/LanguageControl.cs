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

//Création d'une classe pour gérer les langues.
//Creation of a class to manage languages.
public class LanguageControl
{
    //Création d'une variable pour stocker la langue.
    //Creation of a variable to store the language.
    public static ResourceManager? _rm;
    private string language = "EN";

    public LanguageControl()
    {
        //Recherche du fichier de langue dans le projet.
        //Search for the language file in the project.
        _rm = new ResourceManager("AppWPF.Ressources.language", Assembly.GetExecutingAssembly());
    }

    // Recherche de la variable qui lui correspond en parametre en fonction de la langue.
    // Search for the variable that corresponds to it in parameter depending on the language.
    public string? GetLanguageString(string name)
    {
        return _rm?.GetString(name);
    }
    //Création d'une méthode pour changer la langue.
    //Creation of a method to change the language.
    public void ChangeLanguage()
    {
        AskLanguage();
        //Laisse une ligne vide
        //Leaves an empty line
        ConsoleManager.DisplayEmptyLine();
        try
        {
            CultureInfo cultureInfo = new CultureInfo(language);
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;
            //Affiche si le changement de langue est un succès.
            //Displays if the language change is successful.
            ConsoleManager.DisplayLanguage("ChangeLanguageSuccess"); 
        }
        catch (Exception)
        {
        ConsoleManager.DisplayLanguage("ChangeLanguageError");
            //Affiche si le changement de langue est un echec.
            //Displays if the language change is a failure.
        }
    }
    //Méthode qui vise à obtenir le choix de l'utilisateur pour la langue.
    //Method that aims to obtain the user's choice for the language.
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
                //Si l'utilisateur appuie sur "1", la langue sera le français.
                //If the user presses "1", the language will be French.
                case "1":
                    Display("FR");
                    language = "fr";
                    break;

                //Si l'utilisateur appuie sur "2", la langue sera l'anglais.
                //If the user presses "2", the language will be English.
                case "2":
                    Display("EN");
                    language = "en";
                    break;

                //Si l'utilisateur appuie sur une autre touche, une erreur s'affiche.
                //If the user presses another key, an error appears.
                default:
                    DisplayLanguage("InvalidInput");
                    language = "error";
                    isValid = false;
                    break;
            }
        }
    }
}

