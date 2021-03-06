﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailService.Helpers
{
    public class AppSettings
    {
        public string ApiKey { get; set; }

        public string TripSharingEmail { get; set; }

        public string Secret { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}
