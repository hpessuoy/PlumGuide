using System.Collections.Generic;

namespace Rover.Domain.Models
{
    public interface IObstacleRepository
    {
        bool IsObstacle(Coordinates coordinates);
        IReadOnlyList<Coordinates> All();
    }
}