using System.Text.Json.Serialization;

namespace Rover.App.Controllers.Rover.Dtos
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DirectionDto
    {
        North,
        East,
        South,
        West,
        Unknown
    }
}
