using System.ComponentModel.DataAnnotations;

namespace API_MOVIE.DAL.Models.Dto
{
    public class MovieCreateDto
    {

        [Required(ErrorMessage = "El nombre de la categoria es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo permitido: 100 dígitos.")]
        public string name { get; set; }


        [Required(ErrorMessage = "La duración es obligatoria")]
        public int duration { get; set; }


        public string description { get; set; }


        [Required(ErrorMessage = "La clasification es obligatorio")]
        [StringLength(100, ErrorMessage = "Máximo permitido: 10 dígitos.")]
        public string clasification { get; set; }

        public virtual string genero { get; set; }

        public virtual int año { get; set; }
    }
}
