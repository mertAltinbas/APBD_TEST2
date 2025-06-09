namespace Test2.Models;

public class Race
{
    public int RaceId { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public DateTime Date { get; set; }
    
    public ICollection<TrackRace> TrackRaces { get; set; }
}