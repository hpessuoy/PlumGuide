using AutoFixture.NUnit3;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using NUnit.Framework;
using AutoFixture;
using System;
using FluentAssertions;

namespace Rover.Domain.Tests
{
    public class RoverFixture
    {
        [Test, AutoMoqData]
        public void Guards(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(Rover));
        }

        [Test, InlineAutoData]
        public void WhenThen(
            Command command,
            Rover sut)
        {
            // Act
            var actual = sut.Move(command);

            // Assert
            throw new NotImplementedException();
        }
    }
}
