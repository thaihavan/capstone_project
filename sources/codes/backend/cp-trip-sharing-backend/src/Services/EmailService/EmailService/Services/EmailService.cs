using EmailService.Helpers;
using EmailService.Models;
using EmailService.Repositories;
using EmailService.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Reflection;
using System.IO;


namespace EmailService.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _apiKey;
        private readonly IOptions<AppSettings> _settings = null;
        private readonly EmailRepository _emailRepository = null;       

        public EmailService(IOptions<AppSettings> settings){
            _settings = settings;
            _apiKey = _settings.Value.ApiKey;
            _emailRepository = new EmailRepository();
        }
        public HttpResponseMessage SendEmail(Email param)
        {
            string emailTemplate = null;
            if (param.EmailType.Equals("ConfirmEmail"))
            {
                emailTemplate = GetTemplate("EmailService.EmailTemplate.VerifyEmailTemplate.html");
            }
            else if (param.EmailType.Equals("ResetPasswordEmail"))
            {
                emailTemplate = GetTemplate("EmailService.EmailTemplate.ResetPasswordEmailTemplate.html");
            }
            
            var emailContent = WriteToTemplate(emailTemplate, param.Url);
            

            var request = new
            {
                personalizations = new[] {
                    new {
                        to = new[]
                        {
                            new {email = param.To}
                        },
                        subject = param.Subject
                    }
                },
                from = new
                {
                    email = _settings.Value.TripSharingEmail
                },
                content = new[] {
                    new {
                    type = "text/html",
                    value = emailContent
                    }
                }
            };

            HttpClient sendgrid3 = new HttpClient();
            sendgrid3.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer", _apiKey);
            string jsonRequest = JsonConvert.SerializeObject(request);
            var result = sendgrid3.PostAsync("https://api.sendgrid.com/v3/mail/send",
                new StringContent(jsonRequest, System.Text.Encoding.UTF8,
                "application/json")).Result;
            return result;
        }

        public string GetTemplate(string fileName)
        {
            string html = null;
            using (var stream = Assembly.GetEntryAssembly().GetManifestResourceStream(fileName))
            using (var reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            return html;
        }

        public string WriteToTemplate(string html, string url)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("{{action_url}}", url);
            var result = parameters.Aggregate(html, (current, parameter) => current.Replace(parameter.Key, parameter.Value));
            return result;
        }
    }
}

