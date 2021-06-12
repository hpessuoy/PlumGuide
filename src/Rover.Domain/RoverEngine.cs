using System;
using System.Collections.Generic;

namespace Rover.Domain
{
    public class RoverEngine : IRoverEngine
    {
        private readonly IGridConfiguration _gridConfiguration;

        public RoverEngine(IGridConfiguration gridConfiguration)
        {
            _gridConfiguration = gridConfiguration ??
                throw new ArgumentNullException(nameof(gridConfiguration));
        }

        public Location Move(
            Location location,
            IEnumerable<Command> commands)
        {
            if (location is null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            if (commands is null)
            {
                throw new ArgumentNullException(nameof(commands));
            }

            var result = location;
            foreach (var command in commands)
            {
                result = Move(result, command);
            }

            return result;
        }

        public Location Move(
            Location location,
            Command command)
        {
            if (location is null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            switch (command)
            {
                case Command.Forward:
                    return MoveForward(location);
                case Command.Backward:
                    return MoveBackward(location);
                case Command.Right:
                    return TurnRight(location);
                case Command.Left:
                    return TurnLeft(location);
                default:
                    break;
            }
            throw new NotSupportedException();
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
