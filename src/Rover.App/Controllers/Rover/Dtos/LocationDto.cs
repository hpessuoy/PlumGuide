namespace Rover.App.Controllers.Rover.Dtos
{
    public struct LocationDto
    {
        public int X { get; set; }
        public int Y { get; set; }
        public DirectionDto Direction { get; set; }
    }
}
