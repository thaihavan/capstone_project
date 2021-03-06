﻿using IdentityProvider.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityProvider.Utils
{
    public class ReadAppSettings
    {
        public static IOptions<PubsubSettings> ReadPubsubSettings()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            var pubsubSettings = configurationBuilder.Build().GetSection("PubsubSettings");

            var settings = Options.Create(new PubsubSettings()
            {
                ProjectId = pubsubSettings.GetSection("ProjectId").Value,
                TopicId = pubsubSettings.GetSection("TopicId").Value
            });

            return settings;
        }
    }
}
