using System.Collections.Generic;
using EmailService.Models;

namespace EmailService.Repositories.Interfaces
{
    public interface IEmailRepository
    {
        Email Add(Email param);
        bool Delete(string id);
        IEnumerable<Email> GetAll();
        Email Update(Email param);
    }
}