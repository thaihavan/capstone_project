using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserServices.Helpers
{
    public class PubsubSettings
    {
        public string PushTopicId { get; set; }

        public string ProjectId { get; set; }

        public string SubcriptionId { get; set; }
    }
}
