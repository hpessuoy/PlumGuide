using System;
using System.Collections.Generic;

namespace Rover.Domain.Models
{
    public class Rover
    {
        private readonly IRoverEngine _roverEngine;
        private Location _location;

        public Rover(
            string name,
            Location location,
            IRoverEngine roverEngine)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
            _location = location;
            _roverEngine = roverEngine ?? throw new ArgumentNullException(nameof(roverEngine));
        }

        public string Name { get; }

        public Location Location => _location;

        public MoveResult TryMove(Command command) => TryMove(new Command[] { command });

        public MoveResult TryMove(IEnumerable<Command> commands)
        {
            if (commands is null)
            {
                throw new ArgumentNullException(nameof(commands));
            }

            var result = _roverEngine.TryMove(Location, commands);
            _location = result.Current;
            return result;
        }
    }
}
