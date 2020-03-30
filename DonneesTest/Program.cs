using System;
using NancyTest;

namespace DonneesTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new Context())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var user1 = new User { Name = "Mushu", Password = "1234" };
                var user2 = new User { Name = "Krokmou", Password = "1234" };
                var user3 = new User { Name = "Smaug", Password = "1234" };


                context.AddRange(user1);
                context.AddRange(user2);
                context.AddRange(user3);

                context.SaveChanges();
            }
        }
    }
}
