using Test2.DTOs;

namespace Test2.Services;

public interface IDbService
{
    Task<GetRacersParticipationDTO> GetRacersParticipationAsync(int racerId);
    Task<(bool Success, string ErrorMessage)> TrackRaceParticipantRequest(TrackRaceParticipantRequestDTO trackRaceParticipantRequestDTO);
}