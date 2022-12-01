// See https://aka.ms/new-console-template for more information
using Projet_Programmation_Système.developpement;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System;

Main.Run();

//Création d'une class "Main" pour lancer le programme.
//Creation of a "Main" class to launch the program.
public static class Main {

    public static void Run()
    {
        while (true)
        {
            MainMenuManager.ChooseItem();
        }
    }

    public static void Exit()
    {
        Environment.Exit(0);
    }
}

