using Rover.Domain.Models;
using System;
using System.Collections.Generic;

namespace Rover.Domain.Service
{
    public class RoverService : IRoverService
    {
        private readonly IRoverEngine _roverEngine;
        private readonly IQuery<Location, string> _roverQuery;
        private readonly IObstacleRepository _obstacleRepository;
        private readonly IRoverRepository _roverRepository;

        public RoverService(
            IRoverEngine roverEngine,
            IQuery<Location, string> roverQuery,
            IObstacleRepository obstacleRepository,
            IRoverRepository roverRepository)
        {
            _roverEngine = roverEngine ?? throw new ArgumentNullException(nameof(roverEngine));
            _roverQuery = roverQuery ?? throw new ArgumentNullException(nameof(roverQuery));
            _obstacleRepository = obstacleRepository ?? throw new ArgumentNullException(nameof(obstacleRepository));
            _roverRepository = roverRepository ?? throw new ArgumentNullException(nameof(roverRepository));
        }

        public Models.Rover GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            var location = _roverQuery.Execute(name);
            var result = new Models.Rover(name, location, _roverEngine);
            return result;
        }

        public IReadOnlyList<Coordinates> GetObstacles()
        {
            return _obstacleRepository.All();
        }

        public MoveResult TryMove(string name, IEnumerable<Command> commands)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            if (commands is null)
            {
                throw new ArgumentNullException(nameof(commands));
            }

            var rover = GetByName(name);

            if (rover.Location == Location.Unknown)
            {
                new MoveResult(MoveStatus.Failure, rover.Location);
            }

            var result = rover.TryMove(commands);

            _roverRepository.Update(rover);

            return result;
        }
    }
}
