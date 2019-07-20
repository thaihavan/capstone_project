using Google.Cloud.PubSub.V1;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserServices.Helpers;
using UserServices.Models;
using UserServices.Reponsitories;
using UserServices.Reponsitories.Interfaces;
using UserServices.Utils;

namespace UserServices.Services.Processes
{
    public class PullIncreasingCPProcess
    {
        private IUserRepository _userRepository = null;
        private IOptions<PubsubSettings> _pubsubSettings = null;

        public PullIncreasingCPProcess()
        {
            _pubsubSettings = ReadAppSettings.ReadPubsubSettings();
            _userRepository = new UserRepository(ReadAppSettings.ReadDbSettings());
        }

        public void Start()
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            PullMessagesAsync(_pubsubSettings.Value.ProjectId, _pubsubSettings.Value.SubcriptionId, true);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
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
                    IncreasingCP increasingCP = JsonConvert.DeserializeObject<IncreasingCP>(json);
                    _userRepository.IncreaseContributionPoint(increasingCP.UserId, increasingCP.Point);

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
