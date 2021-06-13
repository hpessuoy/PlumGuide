using System;
using System.Collections.Generic;

namespace Rover.Domain
{
    public class ObstacleDetector : IObstacleDetector
    {
        private static HashSet<Command> _commands = new HashSet<Command>()
        {
            Command.Forward,
            Command.Backward
        };

        private readonly IQuery<bool, Coordinates> _obstacleQuery;

        public ObstacleDetector(IQuery<bool, Coordinates> obstacleQuery)
        {
            _obstacleQuery = obstacleQuery ?? throw new ArgumentNullException(nameof(obstacleQuery));
        }

        public bool IsAccessible(Command Command, Location Location)
        {          
            if (!_commands.Contains(Command))
            {
                return true;
            }

            return !_obstacleQuery.Execute(Location.Coordinates);
        }
    }
}