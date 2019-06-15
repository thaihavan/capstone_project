using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityProvider.Models
{
    public class Mail
    {
        public string Subject { get; set; }
        public string To { get; set; }
        public string Url { get; set; }
        public string EmailType { get; set; }
    }
}
