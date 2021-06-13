using System;
using System.Collections.Generic;
using System.Linq;

namespace Rover.Domain.Models
{
    public class RoverEngine : IRoverEngine
    {
        private readonly IGridConfiguration _gridConfiguration;
        private readonly IObstacleDetector _obstacleDetector;

        public RoverEngine(
            IGridConfiguration gridConfiguration,
            IObstacleDetector obstacleDetector)
        {
            _gridConfiguration = gridConfiguration ?? throw new ArgumentNullException(nameof(gridConfiguration));
            _obstacleDetector = obstacleDetector ?? throw new ArgumentNullException(nameof(obstacleDetector));
        }

        public MoveResult TryMove(
            Location location,
            IEnumerable<Command> commands)
        {
            if (commands is null || !commands.Any())
            {
                throw new ArgumentNullException(nameof(commands));
            }

            MoveResult result = default;
            var nextLocation = location;
            foreach (var command in commands)
            {
                result = TryMove(nextLocation, command);
                if (result.Status != MoveStatus.Success)
                {
                    break;
                }
                nextLocation = result.Current;
            }

            return result;
        }

        public MoveResult TryMove(
            Location location,
            Command command)
        {
            try
            {
                var nextLocation = command switch
                {
                    Command.Forward => MoveForward(location),
                    Command.Backward => MoveBackward(location),
                    Command.Right => TurnRight(location),
                    Command.Left => TurnLeft(location),
                    _ => throw new NotSupportedException(),
                };

                if (_obstacleDetector.IsAccessible(command, nextLocation))
                {
                    return new MoveResult(MoveStatus.Success, nextLocation);
                }

                return new MoveResult(MoveStatus.Obstacle, location, nextLocation);
            }
            catch (NotSupportedException)
            {
                throw;
            }
            catch (Exception)
            {
                return new MoveResult(MoveStatus.Failure, location);
            }
        }

        private Location MoveForward(Location location)
        {
            Coordinates coordinates;
            var direction = location.Direction;
            switch (location.Direction)
            {
                case Direction.North:
                    var y = location.Coordinates.Y + 1;
                    if (y >= _gridConfiguration.YMax)
                    {
                        y = _gridConfiguration.YMax - 2;
                        direction = Direction.South;
                    }
                    coordinates = location.Coordinates.WithY(y);
                    break;
                case Direction.East:
                    var x = location.Coordinates.X + 1;
                    if (x >= _gridConfiguration.XMax)
                    {
                        x = _gridConfiguration.XMax - 2;
                        direction = Direction.West;
                    }
                    coordinates = location.Coordinates.WithX(x);
                    break;
                case Direction.South:
                    y = location.Coordinates.Y - 1;
                    if (y < 0)
                    {
                        y = 1;
                        direction = Direction.North;
                    }
                    coordinates = location.Coordinates.WithY(y);
                    break;
                case Direction.West:
                    x = location.Coordinates.X - 1;
                    if (x < 0)
                    {
                        x = 1;
                        direction = Direction.East;
                    }
                    coordinates = location.Coordinates.WithX(x);
                    break;
                default:
                    throw new NotSupportedException();
            }

            return new Location(coordinates, direction);
        }

        private Location MoveBackward(Location location)
        {
            Coordinates coordinates;
            var direction = location.Direction;
            switch (location.Direction)
            {
                case Direction.North:
                    var y = location.Coordinates.Y - 1;
                    if (y < 0)
                    {
                        y = 1;
                        direction = Direction.South;
                    }
                    coordinates = location.Coordinates.WithY(y);
                    break;
                case Direction.East:
                    var x = location.Coordinates.X - 1;
                    if (x < 0)
                    {
                        x = 1;
                        direction = Direction.West;
                    }
                    coordinates = location.Coordinates.WithX(x);
                    break;
                case Direction.South:
                    y = location.Coordinates.Y + 1;
                    if (y >= _gridConfiguration.YMax)
                    {
                        y = _gridConfiguration.YMax - 2;
                        direction = Direction.North;
                    }
                    coordinates = location.Coordinates.WithY(y);
                    break;
                case Direction.West:
                    x = location.Coordinates.X + 1;
                    if (x >= _gridConfiguration.XMax)
                    {
                        x = _gridConfiguration.XMax - 2;
                        direction = Direction.East;
                    }
                    coordinates = location.Coordinates.WithX(x);
                    break;
                default:
                    throw new NotSupportedException();
            }

            return new Location(coordinates, direction);
        }

        private Location TurnRight(Location location)
        {
            return location.Direction switch
            {
                Direction.North => location.With(Direction.East),
                Direction.East => location.With(Direction.South),
                Direction.South => location.With(Direction.West),
                Direction.West => location.With(Direction.North),
                _ => throw new NotSupportedException(),
            };
        }

        private Location TurnLeft(Location location)
        {
            return location.Direction switch
            {
                Direction.North => location.With(Direction.West),
                Direction.East => location.With(Direction.North),
                Direction.South => location.With(Direction.East),
                Direction.West => location.With(Direction.South),
                _ => throw new NotSupportedException(),
            };
        }

    }
}
