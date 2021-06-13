using Rover.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Rover.Infra
{
    public class InMemoryObstacleRepository : IObstacleRepository
    {
        private readonly static HashSet<Coordinates> _obstacles = new HashSet<Coordinates>()
        {
            new Coordinates(7, 6),
            new Coordinates(10, 7),
            new Coordinates(20, 20),
        };

        public IReadOnlyList<Coordinates> All()
        {
            return _obstacles.ToList().AsReadOnly();
        }

        public bool IsObstacle(Coordinates coordinates)
        {
            return _obstacles.Contains(coordinates);
        }
    }
}
