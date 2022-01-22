using Core.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Core.Domain.Account
{
    public interface IAuthenticate
    {
        Task<bool> Authenticate(string email, string password);

        Task<RegisterAttempt> RegisterUser(string email, string password, string firstName, string lastName,
            DateTime? birthDate, string profession, string location);

        Task Logout();
    }
}
