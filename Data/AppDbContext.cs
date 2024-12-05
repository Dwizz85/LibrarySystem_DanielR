using Microsoft.EntityFrameworkCore;
using LibrarySystem_DanielR;


public class AppDbContext : DbContext
{
    public DbSet<Author> Authors {get; set;}                // create collection for Authors in library and allows CRUD
    public DbSet<Book> Books {get; set;}                    // create collection for Book in library and allows CRUD
    public DbSet<BookAuthor> BookAuthors {get; set;}        // create collection for Book & Author in library and allows CRUD
    public DbSet<Member> Members {get; set;}                // create collection for Member in library and allows CRUD
    public DbSet<Loan> Loans {get; set;}                    // create collection for Loan in library and allows CRUD

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)           // connection to server
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=LibrarySystem;Trusted_Connection=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookAuthor>()
        .HasKey(baid => baid.BookAuthorID);     // composite key connecting bridge table

        modelBuilder.Entity<BookAuthor>()
        .HasOne(b => b.Book)                    // relation => BookAuthor relates to one Author.
        .WithMany(ba => ba.BookAuthors)         // relation => Book can have many BookAuthors.
        .HasForeignKey(bid => bid.BookID);      // Foreign Key => BookID.

        modelBuilder.Entity<BookAuthor>()
        .HasOne(a => a.Author)                  // BookAuthor => one Author
        .WithMany(ba => ba.BookAuthors)         // Author => several BookAuthors
        .HasForeignKey(aid => aid.AuthorID);    // Foreign Key => AuthorID

        modelBuilder.Entity<Loan>()
        .HasKey(l => l.LoanID);                 // Primary Key => Loans
    }
}