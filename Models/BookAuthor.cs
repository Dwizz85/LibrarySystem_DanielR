using System;

namespace LibrarySystem_DanielR
{
    public class BookAuthor  // Bridge table <= Book <= Author
    {
            public int BookAuthorID {get; set;}     // Primary Key => unique ID
            public int BookID {get; set;}           // Foreign Key => entity => Book
            public int AuthorID {get; set;}         // Foreign Key => entity => Author

            public Book Book {get; set;}            // Propertys => entity => Book
            public Author Author {get; set;}        // Propertys => entity => Author

    }
}