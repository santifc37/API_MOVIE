using System.ComponentModel.DataAnnotations;

namespace API_MOVIE.DAL.Models
{
    public class Movie
    {

        [Key]
        public virtual int id { get; set; }

        public virtual DateTime CreatedDate { get; set; }

        public virtual DateTime? UpdatedDate { get; set; }
        [StringLength(100)]
        public virtual string name { get; set; }

        public virtual int duration { get; set; }

        public virtual string? description { get; set; } = string.Empty;
        [StringLength(10)]
        public virtual string clasification { get; set; }
    }
}
