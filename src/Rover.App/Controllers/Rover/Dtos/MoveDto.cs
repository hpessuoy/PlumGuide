using System.ComponentModel.DataAnnotations;

namespace Rover.App.Controllers.Rover.Dtos
{
    public class MoveDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Commands { get; set; }
    }
}
