using System.Collections.Generic;

namespace Rover.Domain
{
    public interface IRoverEngine
    {
        Location Move(Location location, Command command);
        Location Move(Location location, IEnumerable<Command> commands);
    }
}
