using Rover.Domain.Helpers;
using System;

namespace Rover.Domain
{
    public struct Location : IEquatable<Location>
    {
        public static Location Unknown = new Location(Coordinates.Unknown, Direction.Unknown);

        public Location(Coordinates coordinates, Direction direction)
        {
            Coordinates = coordinates;
            Direction = direction;
        }

        public Coordinates Coordinates { get; }
        public Direction Direction { get; }

        public Location With(Direction direction) => new Location(Coordinates, direction);

        public bool Equals(Location other) => other.Coordinates == Coordinates
            && other.Direction == Direction;

        public override bool Equals(object obj) => obj is Location ? Equals((Location)obj) : false;

        public override int GetHashCode() => HashCodeHelper.CombineHashCodes(
            Coordinates.GetHashCode(),
            Direction.GetHashCode());

        public static bool operator ==(Location left, Location right) => left.Equals(right);

        public static bool operator !=(Location left, Location right) => !left.Equals(right);
    }
}
