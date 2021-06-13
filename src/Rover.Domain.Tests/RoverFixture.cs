using AutoFixture.NUnit3;
using AutoFixture.Idioms;
using NUnit.Framework;
using FluentAssertions;
using Moq;
using System.Collections.Generic;

namespace Rover.Domain.Tests
{
    public class RoverFixture
    {
        [Test, AutoMoqData]
        public void Guards(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(Rover));
        }

        [Test, AutoMoqData]
        public void WhenACommandIsExecutedTheRoverShouldMoveCorrectly(
            IEnumerable<Command> commands,
            MoveResult expected,
            [Frozen] IRoverEngine roverEngine,
            Rover sut)
        {
            // Arrange
            Mock.Get(roverEngine)
                .Setup(engine => engine.TryMove(It.IsAny<Location>(), commands))
                .Returns(() => expected)
                .Verifiable();

            // Act
            var actual = sut.TryMove(commands);

            // Assert
            actual.Should().Be(expected);
            sut.Location.Should().Be(expected.Current, "The current location of the rover should be updated afer each command.");
            Mock.Get(roverEngine)
                .Verify(engine => engine.TryMove(It.IsAny<Location>(), commands), Times.Once);
        }
    }
}
