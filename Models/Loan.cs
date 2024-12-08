namespace LibrarySystem_DanielR
{
    public class Loan                           // model class for Loans
    {
        public int LoanID {get; set;}           // Primary Key => unique => loans
        public int BookID {get; set;}           // Foreign Key => entity => Book
        public int MemberID {get; set;}         // Foreign Key => entity => Member
        public DateTime LoanDate {get; set;}    // Date for loan 
        public DateTime ReturnDate {get; set;}  //  Date for return loan
        public bool IsReturned {get; set;}      // defualt => false

        public Book Book {get; set;}            // property => entity => Book
        public Member Member {get; set;}        // property => entity => Member
    }
}