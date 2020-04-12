using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApp.Service
{

    public interface IHashingService
    {
        bool CheckPassword(string input, string dataFromDb);
        string HashPassword(string password);
    }

    public class HasingService : IHashingService
    {
        public bool CheckPassword(string input, string passwordHashing)
        {
            return BCrypt.Net.BCrypt.Verify(input, passwordHashing);
        }

        public string HashPassword(string password)
        {
            string hash = BCrypt.Net.BCrypt.HashPassword(password);
            Console.WriteLine(hash);
            return hash;
        }
    }
}
