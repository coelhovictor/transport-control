using Core.Domain.Entities;
using Core.Domain.Validators;
using FluentAssertions;
using System;
using Xunit;

namespace Core.Domain.Tests
{
    public class TagUnitTest1
    {
        [Fact(DisplayName = "Create Tag With Valid State")]
        public void CreateTag_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Tag("1", "Name", "#ffffff");
            action.Should().NotThrow<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Tag With Invalid Id")]
        public void CreateTag_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Tag("", "Name", "#ffffff");
            action.Should().Throw<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Tag Without Name")]
        public void CreateTag_MissingNameValue_DomainExceptionRequiredName()
        {
            Action action = () => new Tag("1", "", "#ffffff");
            action.Should().Throw<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Tag Without Color")]
        public void CreateTag_MissingColorValue_DomainExceptionRequiredColor()
        {
            Action action = () => new Tag("1", "Name", "");
            action.Should().Throw<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Tag With Name Too Long")]
        public void CreateTag_LongNameValue_DomainExceptionNameTooLong()
        {
            Action action = () => new Tag("1", "abcdefghif12345678910AWDaaaaaaaaaaaa", "#ffffff");
            action.Should().Throw<DomainExceptionValidation>();
        }

        [Fact(DisplayName = "Create Tag With Invalid Color")]
        public void CreateTag_InvalidColorValue_DomainExceptionInvalidColor()
        {
            Action action = () => new Tag("1", "abcdefghi", "aaa");
            action.Should().Throw<DomainExceptionValidation>();
        }
    }
}
