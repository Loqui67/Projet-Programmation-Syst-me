using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave.developpement.Models
{
    public class ExtensionPriority
    {
        public Guid Id { get; }
        public string Name { get; }
        
        public ExtensionPriority(string name)
        {
            this.Name = name;
            Id = Guid.NewGuid();
        }
    }
}
