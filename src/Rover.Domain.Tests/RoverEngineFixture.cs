using AutoFixture.Idioms;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Rover.Domain.Tests
{
    public class RoverEngineFixture
    {
        [Test, AutoMoqData]
        public void Guards(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(RoverEngine));
        }

        [Test]
        [InlineAutoMoqData(5, 5, 0, 0, Direction.North, 0, 1, Direction.North)]
        [InlineAutoMoqData(5, 5, 0, 0, Direction.East, 1, 0, Direction.East)]
        [InlineAutoMoqData(5, 4, 2, 3, Direction.South, 2, 2, Direction.South)]
        [InlineAutoMoqData(4, 5, 2, 3, Direction.West, 1, 3, Direction.West)]
        [InlineAutoMoqData(5, 5, 0, 0, Direction.South, 0, 1, Direction.North)]
        [InlineAutoMoqData(5, 5, 0, 0, Direction.West, 1, 0, Direction.East)]
        [InlineAutoMoqData(5, 5, 4, 4, Direction.North, 4, 3, Direction.South)]
        [InlineAutoMoqData(5, 5, 4, 4, Direction.East, 3, 4, Direction.West)]
        public void WhenAForwardCommandIsRequestedTheRoverShouldMoveCorrectly(
            int xMax,
            int yMax,
            int currentX,
            int currentY,
            Direction currentDirection,
            int expectedX,
            int expectedY,
            Direction expectedDirection,
            [Frozen] IGridConfiguration gridConfiguration,
            RoverEngine sut)
        {
            RunCommand(
                xMax,
                yMax,
                currentX,
                currentY,
                currentDirection,
                new Command[] { Command.Forward },
                expectedX,
                expectedY,
                expectedDirection,
                gridConfiguration,
                sut);
        }

        [Test]
        [InlineAutoMoqData(6, 5, 0, 0, Direction.North, 0, 1, Direction.South)]
        [InlineAutoMoqData(4, 4, 0, 0, Direction.East, 1, 0, Direction.West)]
        [InlineAutoMoqData(6, 5, 2, 3, Direction.South, 2, 4, Direction.South)]
        [InlineAutoMoqData(5, 5, 2, 3, Direction.West, 3, 3, Direction.West)]
        [InlineAutoMoqData(6, 5, 0, 0, Direction.South, 0, 1, Direction.South)]
        [InlineAutoMoqData(3, 2, 0, 0, Direction.West, 1, 0, Direction.West)]
        [InlineAutoMoqData(3, 3, 2, 2, Direction.North, 2, 1, Direction.North)]
        [InlineAutoMoqData(6, 6, 4, 3, Direction.East, 3, 3, Direction.East)]
        public void WhenABackwardCommandIsRequestedTheRoverShouldMoveCorrectly(
            int xMax,
            int yMax,
            int currentX,
            int currentY,
            Direction currentDirection,
            int expectedX,
            int expectedY,
            Direction expectedDirection,
            [Frozen] IGridConfiguration gridConfiguration,
            RoverEngine sut)
        {
            RunCommand(
                xMax,
                yMax,
                currentX,
                currentY,
                currentDirection,
                new Command[] { Command.Backward },
                expectedX,
                expectedY,
                expectedDirection,
                gridConfiguration,
                sut);
        }

        [Test]
        [InlineAutoMoqData(6, 5, 0, 0, Direction.North, 0, 0, Direction.East)]
        [InlineAutoMoqData(4, 4, 1, 0, Direction.East, 1, 0, Direction.South)]
        [InlineAutoMoqData(6, 5, 2, 3, Direction.South, 2, 3, Direction.West)]
        [InlineAutoMoqData(5, 5, 2, 2, Direction.West, 2, 2, Direction.North)]
        public void WhenATurnRightCommandIsRequestedTheRoverShouldMoveCorrectly(
            int xMax,
            int yMax,
            int currentX,
            int currentY,
            Direction currentDirection,
            int expectedX,
            int expectedY,
            Direction expectedDirection,
            [Frozen] IGridConfiguration gridConfiguration,
            RoverEngine sut)
        {
            RunCommand(
                xMax,
                yMax,
                currentX,
                currentY,
                currentDirection,
                new Command[] { Command.Right },
                expectedX,
                expectedY,
                expectedDirection,
                gridConfiguration,
                sut);
        }

        [Test]
        [InlineAutoMoqData(6, 5, 0, 0, Direction.North, 0, 0, Direction.West)]
        [InlineAutoMoqData(4, 4, 1, 0, Direction.East, 1, 0, Direction.North)]
        [InlineAutoMoqData(6, 5, 2, 3, Direction.South, 2, 3, Direction.East)]
        [InlineAutoMoqData(5, 5, 2, 2, Direction.West, 2, 2, Direction.South)]
        public void WhenATurnLeftCommandIsRequestedTheRoverShouldMoveCorrectly(
            int xMax,
            int yMax,
            int currentX,
            int currentY,
            Direction currentDirection,
            int expectedX,
            int expectedY,
            Direction expectedDirection,
            [Frozen] IGridConfiguration gridConfiguration,
            RoverEngine sut)
        {
            RunCommand(
                xMax,
                yMax,
                currentX,
                currentY,
                currentDirection,
                new Command[] { Command.Left },
                expectedX,
                expectedY,
                expectedDirection,
                gridConfiguration,
                sut);
        }

        [Test]
        [InlineAutoMoqData(100, 100, 0, 0, Direction.North, new Command[] { Command.Forward, Command.Forward, Command.Right, Command.Forward, Command.Forward }, 2, 2, Direction.East)]
        [InlineAutoMoqData(3, 3, 0, 0, Direction.North, new Command[] { Command.Right, Command.Right, Command.Right, Command.Forward, Command.Backward }, 0, 0, Direction.East)]
        [InlineAutoMoqData(4, 4, 3, 2, Direction.East, new Command[] { Command.Left, Command.Forward, Command.Forward, Command.Left, Command.Forward }, 2, 2, Direction.West)]
        [InlineAutoMoqData(4, 4, 0, 1, Direction.East, new Command[] { Command.Forward, Command.Forward, Command.Left, Command.Forward, Command.Forward, Command.Forward }, 2, 2, Direction.South)]
        public void WhenASerieOfCommandsIsRequestedTheRoverShouldMoveCorrectly(
            int xMax,
            int yMax,
            int currentX,
            int currentY,
            Direction currentDirection,
            Command[] commands,
            int expectedX,
            int expectedY,
            Direction expectedDirection,
            [Frozen] IGridConfiguration gridConfiguration,
            RoverEngine sut)
        {
            RunCommand(
                xMax,
                yMax,
                currentX,
                currentY,
                currentDirection,
                commands,
                expectedX,
                expectedY,
                expectedDirection,
                gridConfiguration,
                sut);
        }

        private void RunCommand(
            int xMax,
            int yMax,
            int currentX,
            int currentY,
            Direction currentDirection,
            Command[] commands,
            int expectedX,
            int expectedY,
            Direction expectedDirection,
            [Frozen] IGridConfiguration gridConfiguration,
            RoverEngine sut)
        {
            // Arrange
            Mock.Get(gridConfiguration)
                .SetupGet(gConf => gConf.XMax)
                .Returns(xMax);
            Mock.Get(gridConfiguration)
                .SetupGet(gConf => gConf.YMax)
                .Returns(yMax);

            var currentLocation = new Location(
                new Coordinates(currentX, currentY),
                currentDirection);

            var expected = new Location(
                new Coordinates(expectedX, expectedY),
                expectedDirection);

            // Act
            var actual = sut.Move(currentLocation, commands);

            // Assert
            actual.Should().Be(expected);
        }
    }
}
