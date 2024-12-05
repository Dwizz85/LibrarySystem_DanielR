using System;

namespace LibrarySystem_DanielR
{
    public class Member
    {

        public int MemberID {get; set;}             // Primary key => unique => member
        public required string FirstName {get; set;}
        public required string LastName {get; set;}
        public required string Email {get; set;}

        public ICollection<Loan> Loans {get; set;}     
    }
}