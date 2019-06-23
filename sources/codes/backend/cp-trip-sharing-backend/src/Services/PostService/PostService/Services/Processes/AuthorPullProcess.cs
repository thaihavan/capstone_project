using Google.Cloud.PubSub.V1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PostService.Helpers;
using PostService.Models;
using PostService.Repositories;
using PostService.Repositories.Interfaces;
using PostService.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PostService.Services.Processes
{
    public class AuthorPullProcess
    {
        private IAuthorRepository _authorRepository = null;
        private IOptions<PubsubSettings> _pubsubSettings = null;

        public AuthorPullProcess()
        {
            _pubsubSettings = ReadAppSettings.ReadPubsubSettings();
            _authorRepository = new AuthorRepository(ReadAppSettings.ReadDbSettings());
        }

        public void Start()
        {
            PullMessagesAsync(_pubsubSettings.Value.ProjectId, _pubsubSettings.Value.SubscriptionId, true);
        }

        private async Task<object> PullMessagesAsync(string projectId, string subscriptionId, bool acknowledge)
        {
            var subscriptionName = new SubscriptionName(projectId, subscriptionId);
            var subscription = await SubscriberClient.CreateAsync(subscriptionName);

            await subscription.StartAsync(
                async (PubsubMessage message, CancellationToken cancel) =>
                {
                    string json = Encoding.UTF8.GetString(message.Data.ToArray());

                    await Console.Out.WriteLineAsync($"Message {message.MessageId}: {json}");

                    // Insert or update author
                    Author author = JsonConvert.DeserializeObject<Author>(json);
                    var result = _authorRepository.InsertOrUpdate(author);
                    
                    if (result == null)
                    {
                        return SubscriberClient.Reply.Nack;
                    }
                    return acknowledge ? SubscriberClient.Reply.Ack : SubscriberClient.Reply.Nack;
                });

            //// Run for 3 seconds.
            //await Task.Delay(3000);
            //await subscription.StopAsync(CancellationToken.None);

            // [END pubsub_subscriber_async_pull]
            return 0;
        }
    }
}
