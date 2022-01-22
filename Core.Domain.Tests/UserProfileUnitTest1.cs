using Core.Domain.Entities;
using Core.Domain.Validators;
using FluentAssertions;
using System;
using Xunit;

namespace Core.Domain.Tests
{
    public class UserProfileUnitTest1
    {
        [Fact(DisplayName = "Create UserProfile With Valid State")]
        public void CreateUserProfile_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new UserProfile("1", "test@email", DateTime.Now, "Tester", "Internet");
            action.Should().NotThrow<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create UserProfile With Invalid Id")]
        public void CreateUserProfile_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new UserProfile("", "test@email", DateTime.Now, "Tester", "Internet");
            action.Should().Throw<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create UserProfile Without Email")]
        public void CreateUserProfile_MissingEmailValue_DomainExceptionRequiredEmail()
        {
            Action action = () => new UserProfile("1", "", DateTime.Now, "Tester", "Internet");
            action.Should().Throw<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create UserProfile With Invalid Email")]
        public void CreateUserProfile_WithInvalidEmail_DomainExceptionInvalidEmail()
        {
            Action action = () => new UserProfile("1", "aaa", DateTime.Now, "Tester", "Internet");
            action.Should().Throw<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create UserProfile With Invalid Profession")]
        public void CreateUserProfile_WithInvalidProfession_DomainExceptionInvalidProfession()
        {
            Action action = () => new UserProfile("1", "test@email", DateTime.Now,
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Internet");
            action.Should().Throw<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create UserProfile With Invalid Location")]
        public void CreateUserProfile_WithInvalidLocation_DomainExceptionInvalidLocation()
        {
            Action action = () => new UserProfile("1", "test@email", DateTime.Now,
                "Tester", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            action.Should().Throw<DomainExceptionValidation>();
        }
    }
}
