using System.ComponentModel.DataAnnotations;

namespace WebBlazorWebApp.Models
{
    public class Player
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required"), MaxLength(50, ErrorMessage = "Cannot exceed 50 characters")]
        public string Name { get; set; }
        [Range(1, 99, ErrorMessage = "must be between 1 - 99")]
        public int ShirtNumber { get; set; }
        public decimal Salary { get; set; }
        public int GoalsThisSeason { get; set; }
        [Required(ErrorMessage = "Position is Required")]
        public string Position { get; set; }
    }
}
