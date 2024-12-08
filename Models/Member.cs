
namespace LibrarySystem_DanielR
{
    public class Member     // model for member class
    {

        public int MemberID {get; set;}                 // Primary key => unique => member
        public required string FirstName {get; set;}     // member first name
        public required string LastName {get; set;}     //  member last name
        public required string Email {get; set;}        // member email

        public ICollection<Loan> Loans {get; set;}      // collection to handle loans from member
    }
}