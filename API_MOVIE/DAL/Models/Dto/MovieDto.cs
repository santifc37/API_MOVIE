namespace API_MOVIE.DAL.Models.Dto
{
    public class MovieDto
    {

        public virtual int id { get; set; }

        public virtual DateTime CreatedDate { get; set; }

        public virtual DateTime? UpdatedDate { get; set; }

        public virtual string name { get; set; }

        public virtual int duration { get; set; }

        public virtual string? description { get; set; } = string.Empty;

        public virtual string Clasification { get; set; }
    }
}
