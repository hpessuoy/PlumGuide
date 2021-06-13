using System;

namespace Rover.Domain.Models
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
}
