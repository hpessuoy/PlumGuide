using Rover.Domain.Models;
using System.Collections.Generic;

namespace Rover.Domain.Service
{
    /// <summary>
    /// Rover service
    /// </summary>
    public interface IRoverService
    {
        /// <summary>
        /// Gets a rover by its name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Models.Rover GetByName(string name);
        
        /// <summary>
        /// Applies a serie of commands to the given rover.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="commands"></param>
        /// <returns></returns>
        MoveResult TryMove(string name, IEnumerable<Command> commands);
        
        /// <summary>
        /// Lists all obstacles.
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<Coordinates> GetObstacles();
    }
}
