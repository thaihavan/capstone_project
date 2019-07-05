using ChatService.Helpers;
using ChatService.Repositories;
using ChatService.Repositories.Interfaces;
using ChatService.Utils;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.PubSub.V1;
using System.Threading;
using System.Text;
using ChatService.Models;
using Newtonsoft.Json;

namespace ChatService.Services.Processes
{
    public class UserPullProcess
    {
        private IUserRepository _userRepository = null;
        private IOptions<PubsubSettings> _pubsubSettings = null;

        public UserPullProcess()
        {
            _pubsubSettings = ReadAppSettings.ReadPubsubSettings();
            _userRepository = new UserRepository(ReadAppSettings.ReadDbSettings());
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
                    User user = JsonConvert.DeserializeObject<User>(json);
                    var result = _userRepository.InsertOrUpdate(user);

                    if (result == null)
                    {
                        return SubscriberClient.Reply.Nack;
                    }
                    return acknowledge ? SubscriberClient.Reply.Ack : SubscriberClient.Reply.Nack;
                });

            //// Run for 3 seconds.
            //await Task.Delay(3000);
            //await subscription.StopAsync(CancellationToken.None);

            return 0;
        }
    }
}
