using System.Collections.Generic;

namespace Rover.Domain
{
    public interface IObstacleRepository
    {
        bool IsObstacle(Coordinates coordinates);
        IReadOnlyList<Coordinates> All();
    }
}