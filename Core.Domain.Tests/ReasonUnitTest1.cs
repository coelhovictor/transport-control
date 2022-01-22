using Core.Domain.Entities;
using Core.Domain.Validators;
using FluentAssertions;
using System;
using Xunit;

namespace Core.Domain.Tests
{
    public class ReasonUnitTest1
    {
        [Fact(DisplayName = "Create Reason With Valid State")]
        public void CreateReason_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Reason("1", "Name");
            action.Should().NotThrow<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Reason With Invalid Id")]
        public void CreateReason_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Reason("", "Name");
            action.Should().Throw<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Reason Without Name")]
        public void CreateReason_MissingNameValue_DomainExceptionRequiredName()
        {
            Action action = () => new Reason("1", "");
            action.Should().Throw<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Reason With Name Too Long")]
        public void CreateReason_LongNameValue_DomainExceptionNameTooLong()
        {
            Action action = () => new Reason("1", "abcdefghif12345678910AWDaaaaaaaaaaaa");
            action.Should().Throw<DomainExceptionValidation>();
        }
    }
}
