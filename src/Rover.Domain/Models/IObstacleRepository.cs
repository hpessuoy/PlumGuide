using System.Collections.Generic;

namespace Rover.Domain.Models
{
    /// <summary>
    /// Obstacle repository.
    /// </summary>
    public interface IObstacleRepository
    {
        /// <summary>
        /// Indicates whether the given coordinate is an obstacle or not.
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        bool IsObstacle(Coordinates coordinates);

        /// <summary>
        /// Gets all obstacles.
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<Coordinates> All();
    }
}