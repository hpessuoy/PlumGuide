namespace Rover.App.Controllers.Rover.Dtos
{
    public struct MoveResultDto
    {
        public MoveStatusDto Status { get; set; }
        public LocationDto Current { get; set; }
        public LocationDto Obstacle { get; set; }
    }
}
