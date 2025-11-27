using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Class_Programmation.DAL.Models.Dtos
{
    public class CategoryDto
    {

        public virtual int id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime ModifiedDate { get; set; }
    }
}
