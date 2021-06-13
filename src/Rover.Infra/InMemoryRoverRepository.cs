using Rover.Domain.Models;
using System;
using System.Collections.Generic;

namespace Rover.Infra
{
    public class InMemoryRoverRepository : IRoverRepository
    {
        private readonly static Dictionary<string, Location> _roverLocations =
            new Dictionary<string, Location>(StringComparer.OrdinalIgnoreCase)
            {
                { "Pluto", new Location(new Coordinates(0, 0), Direction.North) },
                { "Pluto2", new Location(new Coordinates(6, 5), Direction.North) },
                { "Pluto3", new Location(new Coordinates(10, 6), Direction.East) },
            };

        public bool TryGetByName(string name, out Location location)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            return _roverLocations.TryGetValue(name, out location);
        }

        public void Update(Domain.Models.Rover rover)
        {
            if (rover is null)
            {
                throw new ArgumentNullException(nameof(rover));
            }

            _roverLocations[rover.Name] = rover.Location;
        }
    }
}
