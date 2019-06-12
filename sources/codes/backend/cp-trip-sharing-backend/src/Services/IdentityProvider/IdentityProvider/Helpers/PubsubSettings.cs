using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityProvider.Helpers
{
    public class PubsubSettings
    {
        public string VerificationToken { get; set; }
        public string TopicId { get; set; }
        public string SubscriptionId { get; set; }
        public string ProjectId { get; set; }

        private static readonly string[] s_badProjectIds =
            new string[] { "your-project-id - should not subscript itself", "", null };

        public bool HasGoodProjectId() =>
            !s_badProjectIds.Contains(ProjectId);
    }
}
