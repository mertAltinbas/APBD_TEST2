namespace Test2.Models;

public class Racer
{
    public int RacerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public ICollection<RaceParticipation> RaceParticipations { get; set; }
}