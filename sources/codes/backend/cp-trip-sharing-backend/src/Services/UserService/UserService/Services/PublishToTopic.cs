using Google.Cloud.PubSub.V1;
using Google.Protobuf;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServices.Helpers;
using UserServices.Models;
using UserServices.Services.Interfaces;
using UserServices.Utils;

namespace UserServices.Services
{
    public class PublishToTopic : IPublishToTopic
    {
        private IOptions<PubsubSettings> _pubsubSettings = null;

        public PublishToTopic()
        {
            _pubsubSettings = ReadAppSettings.ReadPubsubSettings();
        }

        public async void PublishAuthor(Author author)
        {
            var topicName = new TopicName(_pubsubSettings.Value.ProjectId, _pubsubSettings.Value.PushTopicId);

            PublisherClient publisher = await PublisherClient.CreateAsync(topicName);

            var json = JsonConvert.SerializeObject(author);
            var message = new PubsubMessage()
            {
                Data = ByteString.CopyFromUtf8(json)
            };

            // Publish it
            var response = publisher.PublishAsync(message);
        }
    }
}
