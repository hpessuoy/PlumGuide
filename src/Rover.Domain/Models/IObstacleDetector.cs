namespace Rover.Domain.Models
{
    /// <summary>
    /// Obstacle detector.
    /// </summary>
    public interface IObstacleDetector
    {
        /// <summary>
        /// Indicates whether the given location is accessible or not.
        /// </summary>
        /// <param name="Command"></param>
        /// <param name="Location"></param>
        /// <returns></returns>
        bool IsAccessible(Command Command, Location Location);
    }
}