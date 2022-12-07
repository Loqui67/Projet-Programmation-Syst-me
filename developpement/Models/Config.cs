using System.Collections.Generic;

namespace AppWPF.developpement.Models
{
    public class Config
    {
        public string DefaultLanguage { get; set; }
        public string LogExtension { get; set; }
        public List<Processus> AllProcessus { get; set; }
    }
}
