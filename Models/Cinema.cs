
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Cinema : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Cinema Logo is required")]
        [StringLength(250, ErrorMessage = "Cinema Logo must be less than 250 characters")]
        [Display(Name = "Cinema Logo")]
        public string Logo { get; set; }


        [Required(ErrorMessage = "Cinema Name is required")]
        [StringLength(100, ErrorMessage = "Cinema Name must be less than 100 characters")]
        [Display(Name = "Cinema Name")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Cinema Description is required")]
        [Display(Name = "Description")]
        public string Describtion { get; set; }

        public List<Movie> Movies { get; set; } = new List<Movie>();
    }
}
