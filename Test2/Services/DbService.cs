using Microsoft.EntityFrameworkCore;
using Test2.Data;
using Test2.DTOs;
using Test2.Models;

namespace Test2.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }

    public Task<GetRacersParticipationDTO> GetRacersParticipationAsync(int racerId)
    {
        var racerParticipation = _context.Racers
            .Where(r => r.RacerId == racerId)
            .Select(r => new GetRacersParticipationDTO
            {
                FirstName = r.FirstName,
                LastName = r.LastName,
                Participations = r.RaceParticipations.Select(rp => new ParticipationsDTO
                {
                    Race = new RaceDTO
                    {
                        Name = rp.TrackRace.Race.Name,
                        Location = rp.TrackRace.Race.Location,
                        Date = rp.TrackRace.Race.Date
                    },
                    Track = new TrackDTO
                    {
                        Name = rp.TrackRace.Track.Name,
                        LengthInKm = rp.TrackRace.Track.LengthInKm
                    },
                    Laps = rp.TrackRace.Laps,
                    FinishTimeInSeconds = rp.FinishTimeInSeconds,
                    Position = rp.Position
                }).ToList()
            }).FirstOrDefaultAsync();

        return racerParticipation;
    }

    public async Task<(bool Success, string ErrorMessage)> TrackRaceParticipantRequest(
        TrackRaceParticipantRequestDTO dto)
    {
        if (dto == null || string.IsNullOrWhiteSpace(dto.RaceName) || string.IsNullOrWhiteSpace(dto.TrackName) ||
            dto.Participations == null)
            return (false, "Invalid data.");

        var race = await _context.Races.FirstOrDefaultAsync(r => r.Name == dto.RaceName);
        if (race == null)
            return (false, "Racer could not found.");

        var track = await _context.Tracks.FirstOrDefaultAsync(t => t.Name == dto.TrackName);
        if (track == null)
            return (false, "Track could not found.");

        foreach (var participation in dto.Participations)
        {
            var racer = await _context.Racers.FirstOrDefaultAsync(r => r.RacerId == participation.RacerId);
            if (racer == null)
                return (false, $"Racer could not found: {participation.RacerId}");

            var trackRace = await _context.TrackRaces
                .FirstOrDefaultAsync(tr => tr.RaceId == race.RaceId && tr.TrackId == track.TrackId);

            if (trackRace == null)
                return (false, "Track race could not found.");

            var existingParticipation = await _context.RaceParticipations
                .FirstOrDefaultAsync(rp => rp.TrackRaceId == trackRace.TrackRaceId && rp.RacerId == participation.RacerId);

            if (existingParticipation == null)
            {
                var newParticipation = new RaceParticipation
                {
                    TrackRaceId = trackRace.TrackRaceId,
                    RacerId = participation.RacerId,
                    FinishTimeInSeconds = participation.FinishInTimeSeconds,
                    Position = participation.Position
                };
                _context.RaceParticipations.Add(newParticipation);
            }
            else
            {
                if (participation.FinishInTimeSeconds < existingParticipation.FinishTimeInSeconds)
                {
                    existingParticipation.FinishTimeInSeconds = participation.FinishInTimeSeconds;
                    existingParticipation.Position = participation.Position;
                    _context.RaceParticipations.Update(existingParticipation);
                }
            }
        }

        await _context.SaveChangesAsync();
        return (true, null);
    }
}