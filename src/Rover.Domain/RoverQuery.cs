using System;
using System.Collections.Concurrent;

namespace Rover.Domain
{
    public class RoverQuery : IQuery<Location, string>
    {
        private readonly IRoverRepository _roverRepository;
        public RoverQuery(IRoverRepository roverRepository)
        {
            _roverRepository = roverRepository ?? throw new ArgumentNullException(nameof(roverRepository));
        }

        public Location Execute(string parameter)
        {
            if (string.IsNullOrWhiteSpace(parameter))
            {
                throw new ArgumentException($"'{nameof(parameter)}' cannot be null or whitespace.", nameof(parameter));
            }

            var exists = _roverRepository.TryGet(parameter, out var result);
            return exists ? result : Location.Unknown;
        }
    }

    public class CachedRoverQuery : IQuery<Location, string>
    {
        private readonly IQuery<Location, string> _inner;
        private readonly ConcurrentDictionary<string, Location> _cachedResults;
        public CachedRoverQuery(IQuery<Location, string> inner)
        {
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));
            _cachedResults = new ConcurrentDictionary<string, Location>();
        }

        public Location Execute(string parameter)
        {
            if (string.IsNullOrWhiteSpace(parameter))
            {
                throw new ArgumentException($"'{nameof(parameter)}' cannot be null or whitespace.", nameof(parameter));
            }

            return _cachedResults.GetOrAdd(parameter, param => _inner.Execute(param));
        }
    }
}
