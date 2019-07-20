using Google.Cloud.PubSub.V1;
using Google.Protobuf;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PostService.Helpers;
using PostService.Models;
using PostService.Services.Interfaces;
using PostService.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.Services
{
    public class PublishToTopic : IPublishToTopic
    {
        private IOptions<PubsubSettings> _pubsubSettings = null;

        public PublishToTopic()
        {
            _pubsubSettings = ReadAppSettings.ReadPubsubSettings();
        }

        public async void PublishCP(IncreasingCP increasingCP)
        {
            var topicName = new TopicName(_pubsubSettings.Value.ProjectId, _pubsubSettings.Value.TopicCP);

            PublisherClient publisher = await PublisherClient.CreateAsync(topicName);

            var json = JsonConvert.SerializeObject(increasingCP);
            var message = new PubsubMessage()
            {
                Data = ByteString.CopyFromUtf8(json)
            };

            // Publish it
            var response = publisher.PublishAsync(message);
        }
    }
}
