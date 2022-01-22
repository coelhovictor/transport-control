using Core.Domain.Entities;
using Core.Domain.Validators;
using FluentAssertions;
using System;
using Xunit;

namespace Core.Domain.Tests
{
    public class TransportTypeUnitTest1
    {
        [Fact(DisplayName = "Create TransportType With Valid State")]
        public void CreateTransportType_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new TransportType("1", "Name", 10, "#ffffff");
            action.Should().NotThrow<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create TransportType With Invalid Id")]
        public void CreateTransportType_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new TransportType("", "Name", 10, "#ffffff");
            action.Should().Throw<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create TransportType Without Name")]
        public void CreateTransportType_MissingNameValue_DomainExceptionRequiredName()
        {
            Action action = () => new TransportType("1", "", 10, "#ffffff");
            action.Should().Throw<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create TransportType Without Color")]
        public void CreateTransportType_MissingColorValue_DomainExceptionRequiredColor()
        {
            Action action = () => new TransportType("1", "Name", 10, "");
            action.Should().Throw<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create TransportType With Name Too Long")]
        public void CreateTransportType_LongNameValue_DomainExceptionNameTooLong()
        {
            Action action = () => new TransportType("1", "abcdefghif12345678910AWDaaaaaaaaaaaa", 10, "#ffffff");
            action.Should().Throw<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create TransportType With Invalid Color")]
        public void CreateTransportType_InvalidColorValue_DomainExceptionInvalidColor()
        {
            Action action = () => new TransportType("1", "abcdefghif123", 10, "aaa");
            action.Should().Throw<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create TransportType With Invalid Color")]
        public void CreateTransportType_InvalidPriceValue_DomainExceptionInvalidPrice()
        {
            Action action = () => new TransportType("1", "Name", -1, "#ffffff");
            action.Should().Throw<DomainExceptionValidation>();
        }
    }
}
