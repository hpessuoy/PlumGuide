using System;
using System.Collections.Concurrent;

namespace Rover.Domain.Models
{
    /// <summary>
    /// Obstacle query.
    /// </summary>
    public class ObstacleQuery : IQuery<bool, Coordinates>
    {
        private readonly IObstacleRepository _obstacleRepository;
        public ObstacleQuery(IObstacleRepository obstacleRepository)
        {
            _obstacleRepository = obstacleRepository ?? throw new ArgumentNullException(nameof(obstacleRepository));
        }

        /// <summary>
        /// Indicates whether the given position is an obstacle or not.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool Execute(Coordinates parameter)
        {
            return _obstacleRepository.IsObstacle(parameter);
        }
    }

    /// <summary>
    /// Cached obstacle query
    /// </summary>
    public class CachedObstacleQuery : IQuery<bool, Coordinates>
    {
        private readonly IQuery<bool, Coordinates> _inner;

        private readonly ConcurrentDictionary<Coordinates, bool> _cachedResults;

        public CachedObstacleQuery(IQuery<bool, Coordinates> inner)
        {
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));
            _cachedResults = new ConcurrentDictionary<Coordinates, bool>();
        }

        public bool Execute(Coordinates parameter)
        {
            return _cachedResults.GetOrAdd(parameter, p => _inner.Execute(p));
        }
    }
}