using System.ComponentModel.DataAnnotations;

namespace WebBlazorWebApp.Models
{
    public class Team
    {

        public string TeamName { get; set; }
        [Required(ErrorMessage = "Name is Required"), MaxLength(50, ErrorMessage = "Cannot exceed 50 characters")]
        public string NameOfCoach { get; set; }
        public int Ranking { get; set; }
        public ICollection<Player> Players { get; set; }
    }
}
