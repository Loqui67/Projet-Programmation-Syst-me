// See https://aka.ms/new-console-template for more information
using Projet_Programmation_Système.developpement;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.IO;
using System;

//Main.Run();

public static class Main {

    public static void Run()
    {
        MainMenuManager mainMenu = new MainMenuManager();
        while (true)
        {
            mainMenu.ChooseItem();
        }
    }

    public static void Exit()
    {
        Environment.Exit(0);
    }
}

