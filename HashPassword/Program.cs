using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashPassword
{
    class Program
    {
        static void Main(string[] args)
        {
            PasswordHasher pHasher = new PasswordHasher();
            var HashedPWD = pHasher.HashPassword("123456");

            var isCurrentHashValid = pHasher.VerifyHashedPassword(HashedPWD, "123456");
            //var isOlderHashValid = pHasher.VerifyHashedPassword("AO7kszlVq1gUsEN6eEwH9WcbppmJlG0qtZpmG65xdklCa89AalTbiA+uXXCOVjzDXw==", "test");

            System.Console.WriteLine($"Hashed Password:\n{HashedPWD}\nIs Valid: {isCurrentHashValid}");
        }
    }
}
