namespace Rover.Domain.Models
{
    public interface IObstacleDetector
    {
        bool IsAccessible(Command Command, Location Location);
    }
}