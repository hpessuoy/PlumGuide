using System.Collections.Generic;

namespace Rover.Domain.Models
{
    /// <summary>
    /// Rover engine.
    /// </summary>
    public interface IRoverEngine
    {
        /// <summary>
        /// Applies the given command to the given location. 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        MoveResult TryMove(Location location, Command command);

        MoveResult TryMove(Location location, IEnumerable<Command> commands);
    }
}
