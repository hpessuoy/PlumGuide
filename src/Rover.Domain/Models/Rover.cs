using System;
using System.Collections.Generic;

namespace Rover.Domain.Models
{
    /// <summary>
    /// Represents a Rover
    /// </summary>
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

        /// <summary>
        /// The name of the rover.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The location of the rover.
        /// </summary>
        public Location Location => _location;

        /// <summary>
        /// Applies the given command on the rover.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public MoveResult TryMove(Command command) => TryMove(new Command[] { command });

        /// <summary>
        /// Applies a serie of commands to the rover.
        /// </summary>
        /// <param name="commands"></param>
        /// <returns></returns>
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
