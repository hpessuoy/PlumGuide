using System.Collections.Generic;

namespace Rover.Domain
{
    // TODO: keep one method and use async
    public interface IRoverEngine
    {
        MoveResult TryMove(Location location, Command command);
        MoveResult TryMove(Location location, IEnumerable<Command> commands);
    }
}
