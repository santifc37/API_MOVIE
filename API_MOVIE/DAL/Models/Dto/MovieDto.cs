using System.ComponentModel.DataAnnotations;

namespace API_MOVIE.DAL.Models.Dto
{
    public class MovieDto
    {
        [Required]
        public virtual int id { get; set; }
        [Required]
        public virtual DateTime CreatedDate { get; set; }

        public virtual DateTime UpdatedDate { get; set; }
        [StringLength(100)]
        public virtual string name { get; set; }
        [Required]
        public virtual int duration { get; set; }
       
        public virtual string description { get; set; } = string.Empty;
        [StringLength(10)]
        public virtual string Clasification { get; set; }
    }
}
