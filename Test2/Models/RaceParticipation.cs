namespace Test2.Models;

public class RaceParticipation
{
    public int RacerId { get; set; }
    public int TrackRaceId { get; set; }
    public int FinishTimeInSeconds { get; set; }
    public int Position { get; set; }
    
    public Racer Racer { get; set; }
    public TrackRace TrackRace { get; set; }
}