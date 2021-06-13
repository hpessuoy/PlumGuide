using AutoFixture.Idioms;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Rover.Domain.Models;
using System.Collections.Generic;

namespace Rover.Domain.Tests
{
    // TODO: Use customization to simplify input data.
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
            [Frozen] IObstacleDetector obstacleDetector,
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
                obstacleDetector,
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
            [Frozen] IObstacleDetector obstacleDetector,
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
                obstacleDetector,
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
            [Frozen] IObstacleDetector obstacleDetector,
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
                obstacleDetector,
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
            [Frozen] IObstacleDetector obstacleDetector,
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
                obstacleDetector,
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
            [Frozen] IObstacleDetector obstacleDetector,
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
                obstacleDetector,
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
            IGridConfiguration gridConfiguration,
            IObstacleDetector obstacleDetector,
            RoverEngine sut)
        {
            // Arrange
            SetupGridConfiguration(xMax, yMax, gridConfiguration);

            Mock.Get(obstacleDetector)
                .Setup(o => o.IsAccessible(It.IsAny<Command>(), It.IsAny<Location>()))
                .Returns(true);

            var currentLocation = new Location(new Coordinates(currentX, currentY), currentDirection);
            var expected = new Location(new Coordinates(expectedX, expectedY), expectedDirection);

            // Act
            var actual = sut.TryMove(currentLocation, commands);

            // Assert
            actual.Current.Should().Be(expected);
            actual.Status.Should().Be(MoveStatus.Success);
        }

        [Test]
        [InlineAutoMoqData(
            4, 4, new int[] { 0, 2, 1, 2 },
            0, 0, Direction.North, 
            new Command[] { Command.Forward, Command.Forward }, 
            0, 1, Direction.North, MoveStatus.Obstacle, 0, 2, Direction.North)]
        [InlineAutoMoqData(
            5, 5, new int[] { 2, 2, 3, 3 }, 
            1, 0, Direction.North,
            new Command[] { Command.Forward, Command.Forward, Command.Right, Command.Right, Command.Right, Command.Forward },
            0, 2, Direction.West, MoveStatus.Success, -1, -1, Direction.West)]
        public void WhenThereAreObstaclesTheRoverShouldBehaveCorrectly(
            int xMax,
            int yMax,
            int[] obstacles,
            int currentX,
            int currentY,
            Direction currentDirection,
            Command[] commands,
            int expectedX,
            int expectedY,
            Direction expectedDirection,
            MoveStatus expectedMoveStatus,
            int obstacleX,
            int obstacleY,
            Direction obstacleDirection,
            [Frozen] IGridConfiguration gridConfiguration,
            [Frozen] IObstacleDetector obstacleDetector,
            RoverEngine sut)
        {
            // Arrange
            SetupDependencies(xMax, yMax, obstacles, gridConfiguration, obstacleDetector);

            var currentLocation = new Location(new Coordinates(currentX, currentY), currentDirection);
            var expectedLocation = new Location(new Coordinates(expectedX, expectedY), expectedDirection);
            MoveResult expected;
            if (expectedMoveStatus == MoveStatus.Obstacle)
            {
                var expectedObstacleLocation = new Location(new Coordinates(obstacleX, obstacleY), obstacleDirection);
                expected = new MoveResult(expectedMoveStatus, expectedLocation, expectedObstacleLocation);
            }
            else
            {
                expected = new MoveResult(expectedMoveStatus, expectedLocation);
            }

            // Act
            var actual = sut.TryMove(currentLocation, commands);

            // Assert
            actual.Should().Be(expected);
        }

        private void SetupDependencies(
            int xMax,
            int yMax,
            int[] obstacles,
            IGridConfiguration gridConfiguration,
            IObstacleDetector obstacleDetector)
        {
            SetupGridConfiguration(xMax, yMax, gridConfiguration);

            var obstacleCoordinates = new HashSet<Coordinates>();
            var i = 0;
            while (true)
            {
                obstacleCoordinates.Add(new Coordinates(obstacles[i], obstacles[++i]));
                if (i == obstacles.Length - 1)
                {
                    break;
                }
            }

            Mock.Get(obstacleDetector)
                .Setup(o => o.IsAccessible(It.IsAny<Command>(), It.IsAny<Location>()))
                .Returns((Command c, Location loc) => !obstacleCoordinates.Contains(loc.Coordinates));
        }

        private void SetupGridConfiguration(
            int xMax,
            int yMax,
            IGridConfiguration gridConfiguration)
        {
            Mock.Get(gridConfiguration)
                .SetupGet(gConf => gConf.XMax)
                .Returns(xMax);
            Mock.Get(gridConfiguration)
                .SetupGet(gConf => gConf.YMax)
                .Returns(yMax);
        }
    }
}
