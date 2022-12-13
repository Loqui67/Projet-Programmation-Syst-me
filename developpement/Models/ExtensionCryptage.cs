using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWPF.developpement.Models
{
    public class ExtensionCryptage
    {
        public Guid Id { get; }
        public string Name { get; }

        public ExtensionCryptage(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
        }
    }
}
