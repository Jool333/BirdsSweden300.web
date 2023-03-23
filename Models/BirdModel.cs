namespace BirdsSweden300.web.Models
{
    public class BirdModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public string Genus { get; set; }
        public string Family { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public bool Seen { get; set; }

    }
}