using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    public class PeopleContext : DbContext
    {
        #region Constructors
        public PeopleContext (DbContextOptions<PeopleContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Person { get; set; }
        #endregion

        #region Seeding Data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasData(new Person("Hans", "Wurst"));
            modelBuilder.Entity<Person>().HasData(new Person("Max", "Mustermann"));
            modelBuilder.Entity<Person>().HasData(new Person("Adriana", "Lima"));
        }
        #endregion
    }
}
