using Microsoft.EntityFrameworkCore;

namespace BookManagementFinalTest.Models
{
    public class AppDbContext : DbContext
    {
        /// <summary>Initializes a new instance of the <see cref="AppDbContext"/> class.</summary>
        /// <param name="options">The options.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        /// <summary>Gets or sets the books.</summary>
        /// <value>The books.</value>
        public DbSet<Book> Books { get; set; }

        /// <summary>Gets or sets the authors.</summary>
        /// <value>The authors.</value>
        public DbSet<Author> Authors { get; set; }
    }
}
