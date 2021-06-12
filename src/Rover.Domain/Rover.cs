using System;
using System.Collections.Generic;

namespace Rover.Domain
{
    public class Rover
    {
        public Rover(
            string name,
            Location location)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
            Location = location ?? throw new ArgumentNullException(nameof(location));
        }

        public string Name { get; }

        public Location Location { get; }

        public Location Move(Command command) =>
            Move(new Command[] { command });

        public Location Move(
            IEnumerable<Command> commands)
        {
            if (commands is null)
            {
                throw new ArgumentNullException(nameof(commands));
            }

            throw new NotImplementedException();
        }
    }
}
