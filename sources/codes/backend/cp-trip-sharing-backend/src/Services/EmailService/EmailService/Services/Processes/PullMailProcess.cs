using EmailService.Helpers;
using EmailService.Models;
using EmailService.Repositories;
using EmailService.Repositories.Interfaces;
using EmailService.Utils;
using Google.Api;
using Google.Cloud.PubSub.V1;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EmailService.Services.Processes
{
    public class PullMailProcess
    {
        private IOptions<PubsubSettings> _pubsubSettings = null;
        private IOptions<AppSettings> _appSettings = null;
        private readonly EmailService _emailService = null;
        private readonly IEmailRepository _emailRepository = null;

        public PullMailProcess()
        {
            _pubsubSettings = ReadAppSettings.ReadPubsubSettings();
            _appSettings = ReadAppSettings.ReadMailSettings();
            _emailService = new EmailService(_appSettings);
            _emailRepository = new EmailRepository(_appSettings);
        }

        public async void StartAsync()
        {
            await PullMessagesAsync(_pubsubSettings.Value.ProjectId, _pubsubSettings.Value.SubscriptionId, true);
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

                    // Handle received message. 
                    Email email = JsonConvert.DeserializeObject<Email>(json);
                    email.Id = ObjectId.GenerateNewId().ToString();

                    var result = _emailRepository.Add(email);

                    if (result != null)
                    {
                        _emailService.SendEmail(email);
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
