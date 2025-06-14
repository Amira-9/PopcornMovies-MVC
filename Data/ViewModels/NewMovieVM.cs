
using eTickets.Models;
using System.Security.Principal;

namespace eTickets.Models
{
    public class NewMovieVM  
    {
        
        public int Id { get; set; }
        [Display(Name = "Movie Name")]
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
        [Display(Name = "Movie Name")]
        [Required(ErrorMessage = "Name is required")]
        public string? Describtion { get; set; }
        [Display(Name = "Movie Price")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }
        [Display(Name = "Movie Poster URL")]
        [Required(ErrorMessage = "Movie Poster URL is required")]
        public string? ImageURL { get; set; }
        [Display(Name = "Movie Start Date URL")]
        [Required(ErrorMessage = "Movie Start Date is required")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Movie End Date")]
        [Required(ErrorMessage = "Movie End Date is required")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Select Movie Category")]
        [Required(ErrorMessage = "Movie Category is required")]
        public MovieCategory MovieCategory { get; set; }
        
        //Relationships
        [Display(Name = "Select Actor(s)")]
        [Required(ErrorMessage = "Movie Actors is required")]
        public required List<int> ActorIds { get; set; }

        //Cinema
        [Display(Name = "Select Cinema")]
        [Required(ErrorMessage = "Movie Cinema is required")]
        public int CinemaId { get; set; }

        //Producer
        [Display(Name = "Select Producer")]
        [Required(ErrorMessage = "Movie Producer is required")]
        public int ProducerId { get; set; }
    }
}
