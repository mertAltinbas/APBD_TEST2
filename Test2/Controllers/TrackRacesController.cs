using Microsoft.AspNetCore.Mvc;
using Test2.DTOs;
using Test2.Services;

namespace Test2.Controllers;

[Route("api/track-races")]
[ApiController]
public class TrackRacesController : ControllerBase
{
    private readonly IDbService _dbService;

    public TrackRacesController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpPost("participants")]
    public async Task<IActionResult> TrackRaceParticipantRequest(
        [FromBody] TrackRaceParticipantRequestDTO trackRaceParticipantRequestDTO)
    {
        if (trackRaceParticipantRequestDTO == null)
            return BadRequest("Request body cannot be null.");

        try
        {
            var (success, errorMessage) = await _dbService.TrackRaceParticipantRequest(trackRaceParticipantRequestDTO);
            if (!success)
                return BadRequest(errorMessage);

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}