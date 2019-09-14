using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebHooks.Models
{
    public class ACRPayload
    {
        public string id { get; set; }
        public string timestamp { get; set; }
        public string action { get; set; }
        public Target target { get; set; }
        public Request request { get; set; }

        public string GetImageName()
        {
            var result = $"{target?.repository}:{target?.tag}";
            return result == ":" ? null : result;
        }
    }

    public class Target
    {
        public string mediaType { get; set; }
        public int size { get; set; }
        public string digest { get; set; }
        public int length { get; set; }
        public string repository { get; set; }
        public string tag { get; set; }
    }

    public class Request
    {
        public string id { get; set; }
        public string host { get; set; }
        public string method { get; set; }
        public string useragent { get; set; }
    }

}
