using Google.Cloud.PubSub.V1;
using Google.Protobuf;
using IdentityProvider.Helpers;
using IdentityProvider.Models;
using IdentityProvider.Services.Interfaces;
using IdentityProvider.Utils;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityProvider.Services
{
    public class PublishToTopic : IPublishToTopic
    {
        private IOptions<PubsubSettings> _pubsubSettings = null;

        public PublishToTopic()
        {
            _pubsubSettings = ReadAppSettings.ReadPubsubSettings();
        }

        public async void PublishEmail(Mail mail)
        {
            var topicName = new TopicName(_pubsubSettings.Value.ProjectId, _pubsubSettings.Value.TopicId);

            PublisherClient publisher = await PublisherClient.CreateAsync(topicName);

            var json = JsonConvert.SerializeObject(mail);
            var message = new PubsubMessage()
            {
                Data = ByteString.CopyFromUtf8(json)
            };

            // Publish it
            var response = publisher.PublishAsync(message);
        }
    }
}
