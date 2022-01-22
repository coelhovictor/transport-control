using Core.Domain.Validators;
using Microsoft.AspNetCore.Identity;

namespace Core.Infra.Data.Identity
{
    public sealed class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Picture { get; set; }

        public ApplicationUser()
        {
        }

        public ApplicationUser(string userName, string email, string firstName, string lastName)
        {
            ValidateDomain(userName, email, firstName, lastName);
        }

        private void ValidateDomain(string userName, string email, string firstName, string lastName)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(firstName), "First name is Required");
            DomainExceptionValidation.When(firstName.Length > 25, "First name is too long");
            if (!string.IsNullOrEmpty(lastName)) DomainExceptionValidation.When(lastName.Length > 25, "Last name is too long");

            UserName = userName;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
