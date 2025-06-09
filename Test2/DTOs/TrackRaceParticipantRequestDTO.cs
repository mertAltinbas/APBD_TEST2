namespace Test2.DTOs;

public class TrackRaceParticipantRequestDTO
{
    public string RaceName { get; set; }
    public string TrackName { get; set; }
    public List<AddParticipationsDTO> Participations { get; set; }
}

public class AddParticipationsDTO
{
    public int RacerId { get; set; }
    public int Position { get; set; }
    public int FinishInTimeSeconds { get; set; }
}