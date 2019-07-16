using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Models
{
    public class UserInfo
    {
        public string Id { get; set; }

        public List<string> Topics { get; set; }

        public List<string> Follows { get; set; }
    }
}
