namespace Rover.Domain
{
    public interface IObstacleDetector
    {
        bool IsAccessible(Command Command, Location Location);
    }
}