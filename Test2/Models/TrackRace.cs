namespace Test2.Models;

public class TrackRace
{
    public int TrackRaceId { get; set; }
    public int TrackId { get; set; }
    public int RaceId { get; set; }
    public int Laps { get; set; }
    public int? BestLapTimeInSeconds { get; set; }
    
    public Track Track { get; set; }
    public Race Race { get; set; }
    
    public ICollection<RaceParticipation> RaceParticipations { get; set; }
}