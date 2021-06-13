using Rover.Domain.Helpers;
using System;

namespace Rover.Domain.Models
{
    // TODO: Base clase for value types.
    public struct Coordinates : IEquatable<Coordinates>
    {
        public static Coordinates Unknown = new Coordinates(true);

        public Coordinates(int x, int y)
        {
            if (x < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(x));
            }

            if (y < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(y));
            }

            X = x;
            Y = y;
        }

        private Coordinates(bool fake)
        {
            X = -1;
            Y = -1;
        }

        public int X { get; }
        public int Y { get; }

        public Coordinates WithX(int x) => new Coordinates(x, Y);
        public Coordinates WithY(int y) => new Coordinates(X, y);

        public bool Equals(Coordinates other) => other.X == X && other.Y == Y;

        public override bool Equals(object obj) => obj is Coordinates ? Equals((Coordinates)obj) : false;

        public override int GetHashCode() => HashCodeHelper.CombineHashCodes(X, Y);

        public static bool operator ==(Coordinates left, Coordinates right) => left.Equals(right);

        public static bool operator !=(Coordinates left, Coordinates right) => !left.Equals(right);
    }
}
