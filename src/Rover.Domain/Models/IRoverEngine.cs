using System.Collections.Generic;

namespace Rover.Domain.Models
{
    public interface IRoverEngine
    {
        MoveResult TryMove(Location location, Command command);
        MoveResult TryMove(Location location, IEnumerable<Command> commands);
    }
}
