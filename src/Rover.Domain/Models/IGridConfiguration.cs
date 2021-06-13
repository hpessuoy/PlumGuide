namespace Rover.Domain.Models
{
    /// <summary>
    /// Grid configuration.
    /// </summary>
    public interface IGridConfiguration
    {
        int XMax { get; }
        int YMax { get; }
    }
}