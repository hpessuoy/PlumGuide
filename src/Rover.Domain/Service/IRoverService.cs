using Rover.Domain.Models;
using System.Collections.Generic;

namespace Rover.Domain.Service
{
    public interface IRoverService
    {
        Models.Rover Get(string name);
        MoveResult TryMove(string name, IEnumerable<Command> commands);
        IReadOnlyList<Coordinates> GetObstacles();
    }
}
