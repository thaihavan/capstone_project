using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityProvider.Models
{
    public class GoogleUser:Model
    {
        public string id { get; set; }
        public string email { get; set; }      
        public string name { get; set; }
        public string given_name { get; set; }
        public string picture { get; set; }
        public string gender { get; set; }       
    }
}
