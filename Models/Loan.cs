using System;

namespace LibrarySystem_DanielR
{
    public class Loan
    {
        public int LoanID {get; set;}           // Primary Key => unique => loans
        public int BookID {get; set;}           // Foreign Key => entity => Book
        public int MemberID {get; set;}         // Foreign Key => entity => Member
        public DateTime LoanDate {get; set;}
        public DateTime ReturnDate {get; set;}
        public bool IsReturned {get; set;}      // defualt => false

        public Book Book {get; set;}            // property => entity => Book
        public Member Member {get; set;}        // property => entity => Member
    }
}