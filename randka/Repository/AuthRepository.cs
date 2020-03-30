
using Microsoft.EntityFrameworkCore;
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

        public async Task<User> Login(string username, string password)
        {
            var user = await _context.users.FirstOrDefaultAsync(x => x.Username == username); // pobieramy uzytk z bd

            if (user == null) // sprawdzamy czy jest
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)) // weryfikujemy haslo ze strony z db
                return null;

            return user;
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
        public async Task<bool> UserExitst(string username)
        {
            if (await _context.users.AnyAsync(x => x.Username == username))
                return true;

            return false;
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

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))  // dodajemy nasz klucz zahashowany
            {
                
                var computedhash= hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); // kodujemy haslo tak jak powinno byc zahashowane w db

                for (int i = 0; i < computedhash.Length; i++) // sprawdzamy czy pasuje z haslem db
                {
                    if (computedhash[i] != passwordHash[i])
                        return false;
                }
                return true;
            }
        }


        #endregion

    }
}
