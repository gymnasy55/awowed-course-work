using System;
using Microsoft.AspNetCore.Identity;

namespace PasswordToHash
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var hasher = new PasswordHasher<IdentityUser>();
            while (true)
            {
                Console.Write("Enter password to has:\n>");
                var password = Console.ReadLine();
                Console.WriteLine(hasher.HashPassword(new IdentityUser(), password));
                
                Console.WriteLine();
            }
        }
    }
}