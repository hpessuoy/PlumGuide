using System;
using System.Collections.Generic;

namespace Rover.Domain.Service
{
    public interface IRoverService
    {
        Rover Get(string name);
        IReadOnlyList<Coordinates> GetObstacles();
    }

    public class RoverService : IRoverService
    {
        private readonly IRoverEngine _roverEngine;
        private readonly IQuery<Location, string> _roverQuery;
        private readonly IObstacleRepository _obstacleRepository;
        public RoverService(
            IRoverEngine roverEngine,
            IQuery<Location, string> roverQuery,
            IObstacleRepository obstacleRepository)
        {
            _roverEngine = roverEngine ?? throw new ArgumentNullException(nameof(roverEngine));
            _roverQuery = roverQuery ?? throw new ArgumentNullException(nameof(roverQuery));
            _obstacleRepository = obstacleRepository ?? throw new ArgumentNullException(nameof(obstacleRepository));
        }

        public Rover Get(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            var location = _roverQuery.Execute(name);
            var result = new Rover(name, location, _roverEngine);
            return result;
        }

        public IReadOnlyList<Coordinates> GetObstacles()
        {
            return _obstacleRepository.All();
        }
    }
}
