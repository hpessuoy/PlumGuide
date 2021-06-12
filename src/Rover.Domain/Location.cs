using Rover.Domain.Helpers;
using System;

namespace Rover.Domain
{
    public class Location : IEquatable<Location>
    {
        public Location(Coordinates coordinates, Direction direction)
        {
            Coordinates = coordinates;
            Direction = direction;
        }

        public Coordinates Coordinates { get; }
        public Direction Direction { get; }

        public Location With(Direction direction) => new Location(Coordinates, direction);

        public bool Equals(Location other) => other is not null
            && other.Coordinates == Coordinates
            && other.Direction == Direction;

        public override bool Equals(object obj) => Equals(obj as Location);

        public override int GetHashCode() => HashCodeHelper.CombineHashCodes(
            Coordinates.GetHashCode(),
            Direction.GetHashCode());

        public static bool operator ==(Location left, Location right) => left.Equals(right);

        public static bool operator !=(Location left, Location right) => !left.Equals(right);
    }
}
