using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BirdsSweden300.web.ViewModel
{
    public class BaseViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Namn måste anges")]
        [DisplayName("Namn")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Art måste anges")]
        [DisplayName("Artnamn")]
        public string Species { get; set; }
        [Required(ErrorMessage = "Släkte måste anges")]
        [DisplayName("Släkte")]
        public string Genus { get; set; }
        [Required(ErrorMessage = "Familj måste anges")]
        [DisplayName("Familj")]
        public string Family { get; set; }
        [DisplayName("Beskrivning")]
        public string Description { get; set; }
        public string ImageUrl { get; set; } = "no-bird.png";
        public bool Seen { get; set; } =false;
    }
}