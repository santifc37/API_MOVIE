using Class_Programmation.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Class_Programmation.DAL
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options) 
        {
        
        }    
        public DbSet<Category> Categories { get; set; }
            
        }


    }

