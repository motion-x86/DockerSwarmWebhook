using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebHooks.Models
{
    public class ACRConfig
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Guid Token { get; set; }
        public string Registry { get; set; }

        private Dictionary<string, string> _services;
        public Dictionary<string, string> Services
        {
            get => _services;
            set => _services = FixDictionaryKeys(value);
        }

        // since we cannot have colon in config dictionary keys we sub ":" for "!" in the config
        // here we reverse the process
        private Dictionary<string, string> FixDictionaryKeys(Dictionary<string, string> dic)
        {
            return dic.Keys.ToDictionary(k => k.Replace("!", ":"), k => dic[k]);
        }
    }

}
