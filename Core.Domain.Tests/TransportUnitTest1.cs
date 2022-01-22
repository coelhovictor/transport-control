using Core.Domain.Entities;
using Core.Domain.Validators;
using FluentAssertions;
using System;
using Xunit;

namespace Core.Domain.Tests
{
    public class TransportUnitTest1
    {
        [Fact(DisplayName = "Create Transport With Valid State")]
        public void CreateTransport_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Transport("1", "Name", 10, DateTime.Now, null);
            action.Should().NotThrow<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Transport With Invalid Id")]
        public void CreateTransport_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Transport("", "Name", 10, DateTime.Now, null);
            action.Should().Throw<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Transport Without Name")]
        public void CreateTransport_MissingNameValue_ResultObjectValidState()
        {
            Action action = () => new Transport("1", "", 10, DateTime.Now, null);
            action.Should().NotThrow<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Transport With Name Too Long")]
        public void CreateTransport_LongNameValue_DomainExceptionNameTooLong()
        {
            Action action = () => new Transport("1", "abcdefghif12345678910AWDaaaaaaaaaaaa", 10, DateTime.Now, null);
            action.Should().Throw<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Transport With Invalid Color")]
        public void CreateTransport_InvalidPriceValue_DomainExceptionInvalidPrice()
        {
            Action action = () => new Transport("1", "Name", -1, DateTime.Now, null);
            action.Should().Throw<DomainExceptionValidation>();
        }
    }
}
