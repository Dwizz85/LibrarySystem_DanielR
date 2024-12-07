using System;

namespace LibrarySystem_DanielR
{
    public class Book               // class for books in library
    {
        public int BookId {get; set;}                               // Id for book added in library
        public required string Title {get; set;}                 // required title => in library
        public int YearPublished {get; set;}                    // year published
        public bool IsAvailable {get; set;}                     // default set => false
        public ICollection<BookAuthor> BookAuthors {get; set;}       // Many => Many => property => Authors
        public ICollection<Loan> Loans {get; set;}                       // One => Many => property => Loans
    }
}