using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityProvider.Models
{
    public class ChangePasswordModel:Model
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}
