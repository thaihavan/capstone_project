using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Models
{
    public class PostFilter
    {
        public string TimePeriod { get; set; }

        public List<string> Topics { get; set; }
    }
}
