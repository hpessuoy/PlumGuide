using Rover.Domain.Helpers;
using System;

namespace Rover.Domain
{
    public struct MoveResult : IEquatable<MoveResult>
    {
        public MoveResult(
            MoveStatus status, 
            Location current)
        {
            Status = status;
            Current = current;
            Obstacle = Location.Unknown;
        }

        public MoveResult(
            MoveStatus status,
            Location current,
            Location obstacle)
        {
            Status = status;
            Current = current;
            Obstacle = obstacle;
        }

        public MoveStatus Status { get; }
        public Location Current { get; } 
        public Location Obstacle { get; }

        public bool Equals(MoveResult other) => other.Status == Status
            && other.Current == Current
            && other.Obstacle == Obstacle;

        public override bool Equals(object obj) => obj is MoveResult ? Equals((MoveResult)obj) : false;

        public override int GetHashCode() => HashCodeHelper.CombineHashCodes(
            Status.GetHashCode(),
            Current.GetHashCode(),
            Obstacle.GetHashCode());

        public static bool operator ==(MoveResult left, MoveResult right) => left.Equals(right);

        public static bool operator !=(MoveResult left, MoveResult right) => !left.Equals(right);
    }
}
