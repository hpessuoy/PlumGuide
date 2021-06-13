using System;

namespace Rover.Domain.Models
{
    /// <summary>
    /// Grid configuration.
    /// </summary>
    public class GridConfiguration : IGridConfiguration
    {
        public GridConfiguration(
            int xMax,
            int yMax)
        {
            if (xMax <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(xMax));
            }

            if (yMax <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(yMax));
            }

            XMax = xMax;
            YMax = yMax;
        }

        public int XMax { get; }
        public int YMax { get; }
    }
}
