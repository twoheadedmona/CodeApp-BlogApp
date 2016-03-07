using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Repository
{
    public enum AuthStatus { Done, Error }
    public class AuthenticationFactory
    {
        private UserRepository userRepo = new UserRepository();
        public AuthStatus LogIn(string username, string password)
        {
            var user = userRepo.GetAll().FirstOrDefault(x => x.Username == username && x.Password == password);
            if (user == null) return AuthStatus.Error;
            return AuthStatus.Done;

        }
    }
}
