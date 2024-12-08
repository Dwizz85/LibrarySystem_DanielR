
namespace LibrarySystem_DanielR
{
    public class Author                                 // class for Author
    {
        public int AuthorId {get; set;}                // Primary Key => Id for authors added
        public string FirstName {get; set;}          // First name of author
        public string LastName {get; set;}           // Last name of author

        public ICollection<BookAuthor> BookAuthors {get; set;} = new List<BookAuthor>();
        // collection Many-to-many propertys => Books + Author    
    }
}