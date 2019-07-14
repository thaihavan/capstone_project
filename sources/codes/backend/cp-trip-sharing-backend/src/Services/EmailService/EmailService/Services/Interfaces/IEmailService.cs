using EmailService.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmailService.Services.Interfaces
{
    public interface IEmailService
    {
        Task<HttpResponseMessage> SendEmailAsync(Email param);
    }
}
