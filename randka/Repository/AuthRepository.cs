using randka.data;
using randka.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace randka.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly datacontext _context;

        #region method public 
        public AuthRepository(datacontext context)
        {
            _context = context;
        }

        public Task<User> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] PasswordHash, PasswordSalt;
            CreatePasswordHashSalt(password,out PasswordHash, out PasswordSalt);

            user.PasswordHash = PasswordHash;
            user.PasswordSalt = PasswordSalt;

            await _context.users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }
        public Task<bool> UserExitst(string username)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region metod private
        private void CreatePasswordHashSalt(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512()) // wykorzystujemy metody z hmac dla hasla dla salt i hash
            {
                passwordSalt = hmac.Key; // klucz do kodowania
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); // haszujemy haslo ktore przyszlo do nas 
            }
            
        }
        #endregion

    }
}
