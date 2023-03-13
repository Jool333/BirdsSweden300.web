using System.ComponentModel.DataAnnotations;

namespace BirdsSweden300.web.ViewModel
{
    public class BaseViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Namn måste anges")]
        public string Name { get; set; }
        public string Species { get; set; }
        public string Genus { get; set; }
        public string Family { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; } = "no-bird.png";
        public bool Seen { get; set; } =false;
    }
}