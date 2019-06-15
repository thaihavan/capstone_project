using Google.Cloud.PubSub.V1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PostService.Helpers;
using PostService.Repositories;
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
        private PostRepository _postRepository = null;
        private IOptions<PubsubSettings> _pubsubSettings = null;

        public AuthorPullProcess()
        {
            _pubsubSettings = ReadAppSettings.ReadPubsubSettings();
            _postRepository = new PostRepository(ReadAppSettings.ReadDbSettings());
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
                    string text = Encoding.UTF8.GetString(message.Data.ToArray());

                    // TODO: will be removed
                    _postRepository.Add(new Models.Post()
                    {
                        Title = "Test pull from pubsub",
                        Content = text
                    });

                    await Console.Out.WriteLineAsync($"Message {message.MessageId}: {text}");
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
