using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityProvider.Models
{
    public class GoogleUser : Model
    {
        public string Id { get; set; }
        public string Email { get; set; }      
        public string Name { get; set; }
        public string Given_name { get; set; }
        public string Picture { get; set; }
        public string Gender { get; set; }       
    }
}
