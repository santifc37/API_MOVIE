using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Class_Programmation.DAL.Models
{
    public class Auditbases
    {
        [Key]
        public virtual int id { get; set; }
        public virtual string name { get; set; }
        public virtual DateTime CreatedDate { get; set; } 
        public virtual DateTime? ModifiedDate { get; set; }
    }
}
