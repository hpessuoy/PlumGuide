using System.Text.Json.Serialization;

namespace Rover.App.Controllers.Rover.Dtos
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MoveStatusDto
    {
        Success,
        Failure,
        Obstacle
    }
}
