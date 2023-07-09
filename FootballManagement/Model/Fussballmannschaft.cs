namespace FootballManagement.Model
{
    public class Fussballmannschaft
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public List<Spieler> Spieler { get; set; } = new List<Spieler>();
    }
}
