namespace FootballManagement.Model
{
    public class Spieler
    {
        public int Id { get; set; }

        public string? Nachname { get; set; }

        public string? Vorname { get; set; }


        public List<Fussballmannschaft>? Fussballmannschaft { get; set; }
    }
}
