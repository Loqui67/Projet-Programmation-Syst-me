using System;

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
