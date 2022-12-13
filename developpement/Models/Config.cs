using System.Collections.Generic;

namespace AppWPF.developpement.Models
{
    ///Class de configuration
    public class Config
    {
        public string DefaultLanguage { get; set; }
        public string LogExtension { get; set; }
        public List<Processus> AllProcessus { get; set; }
        public List<ExtensionCryptage> AllExtensionCryptage { get; set; }
        public List<ExtensionPriority> AllExtensionPriority { get; set; }
    }
}
