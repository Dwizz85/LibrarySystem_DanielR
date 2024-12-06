using System;
using LibrarySystem_DanielR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

public class AddMember
{

    public static void Run()
    {

        using (var context = new AppDbContext())
        {
            while (true)
            {

                System.Console.WriteLine("Adding New Member:");
                System.Console.WriteLine("__________________\n");
                System.Console.WriteLine("1: Enter First Name:");
                var _firstName = Console.ReadLine()?.Trim();
                System.Console.WriteLine("2: Enter Last Name:");
                var _lastName = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(_firstName ) || string.IsNullOrEmpty(_lastName))
                {
                    System.Console.WriteLine("First and Last name must be entered!");
                    continue;
                }

                var _member = context.Members
                    .FirstOrDefault(b => b.FirstName == _firstName && b.LastName == _lastName);

                if (_member == null)
                {
                    System.Console.WriteLine("Member is not in the database and can be registerd!");
                }

                System.Console.WriteLine("3: Enter Email:");
                var _email = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(_email))
                {
                    System.Console.WriteLine("Please enter Email adress!");
                    continue;
                }

                _member = new Member
                {
                    FirstName = _firstName,
                    LastName = _lastName,
                    Email = _email
                };
                
                context.Members.Add(_member);
                context.SaveChanges();
                System.Console.WriteLine($"Member: '{_firstName} {_lastName}, {_email} registered!");
                System.Console.WriteLine("And so it begins.. The time of the book loaning is here!");
                return;                       
            }
        }
    }
}