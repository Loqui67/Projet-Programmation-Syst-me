// See https://aka.ms/new-console-template for more information
using Projet_Programmation_Système.developpement;
using System.Globalization;
using System.Reflection;
using System.Resources;

new Main();

public class Main {
    public Main()
    {
        ConsoleManager.languageControl = new LanguageControl();
        Run();
    }

    public static void Run()
    {
        ChangeLanguage();
        while (true)
        {
            new MainMenuManager();
        }
    }

    public static void ChangeLanguage()
    {
        ConsoleManager.languageControl.ChangeLanguage();
    }
}

