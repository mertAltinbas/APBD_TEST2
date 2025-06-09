using Microsoft.AspNetCore.Mvc;
using Test2.Services;

namespace Test2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RacersController : ControllerBase
{
    private readonly IDbService _dbService;
    
    public RacersController(IDbService dbService)
    {
        _dbService = dbService;
    }
    
    [HttpGet("{racerId}/participations")]
    public async Task<IActionResult> GetRacersParticipation(int racerId)
    {
        if (racerId <= 0)
            return BadRequest("Invalid racer ID.");
        
        try
        {
            var racerParticipation = await _dbService.GetRacersParticipationAsync(racerId);
            if (racerParticipation == null)
                return NotFound($"Racer with ID {racerId} not found.");
            
            return Ok(racerParticipation);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    
}