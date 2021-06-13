namespace Rover.Domain
{
    public interface IObstacleRepository
    {
        bool IsObstacle(Coordinates coordinates);
    }
}