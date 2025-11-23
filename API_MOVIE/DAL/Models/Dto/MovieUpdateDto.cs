using System.ComponentModel.DataAnnotations;

namespace API_MOVIE.DAL.Models.Dto
{
    public class MovieUpdateDto
    {

        [StringLength(100, ErrorMessage = "Máximo permitido: 100 dígitos.")]
        public string name { get; set; }


        [Required(ErrorMessage = "La duración es obligatoria")]
        public int duration { get; set; }


        public string description { get; set; }


        [StringLength(100, ErrorMessage = "Máximo permitido: 10 dígitos.")]
        public string clasification { get; set; }
    }
}
