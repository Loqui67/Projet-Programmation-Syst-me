using System;
//Fichier servant à la mise en place de processus.
//File used to set up processes.
namespace AppWPF.developpement.Models
{
    public class Processus
    {
        public Guid Id { get; }
        public string Name { get; }

        public Processus(string name)
        {
            this.Name = name;
            Id = Guid.NewGuid();
        }
    }
}
