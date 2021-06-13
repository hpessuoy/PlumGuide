using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rover.App.Controllers.Rover.Dtos;
using Rover.Domain.Models;
using Rover.Domain.Service;
using System;
using System.Linq;

namespace Rover.App.Controllers.Rover
{
    [ApiController]
    [Route("api/rover")]
    public class RoverController : ControllerBase
    {
        private readonly IRoverService _roverService;
        private readonly IMapper _mapper;

        public RoverController(
            IMapper mapper,
            IRoverService roverService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _roverService = roverService ?? throw new ArgumentNullException(nameof(roverService));
        }

        [HttpGet("location")]
        public IActionResult GetLocation([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest($"Please provide a rover name");
            }

            var rover = _roverService.GetByName(name);
            var location = _mapper.Map<LocationDto>(rover.Location);
            return Ok(location);
        }

        [HttpPost("move")]
        public IActionResult Move([FromBody] MoveDto moveDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid commands");
            }

            // TODO: Handle errors. Right now, unknown commands are ignored.
            // TODO: Adds Command size max.
            var commands = moveDto.Commands.Select(cmd => ToCommand(cmd)).Where(cmd => cmd != Command.Unknown);
            if (!commands.Any())
            {
                return Problem($"Empty or unknown commands: {moveDto.Commands}");
            }

            var moveResult = _roverService.TryMove(moveDto.Name, commands);
            //var result = _mapper.Map<MoveStatusDto>(moveResult);
            var result = new MoveResultDto()
            {
                Current = _mapper.Map<LocationDto>(moveResult.Current),
                Obstacle = _mapper.Map<LocationDto>(moveResult.Obstacle),
                Status = _mapper.Map<MoveStatusDto>(moveResult.Status),
            };

            return Ok(result);
        }

        [HttpGet("obstacles")]
        public IActionResult GetObstacles()
        {
            var obstacles = _roverService.GetObstacles();
            var result = _mapper.Map<CoordinatesDto[]>(obstacles);
            return Ok(result);
        }


        private Command ToCommand(char cmd)
        {
            return cmd switch
            {
                'F' => Command.Forward,
                'B' => Command.Backward,
                'R' => Command.Right,
                'L' => Command.Left,
                _ => Command.Unknown,
            };
        }
    }
}
