using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            
            _context = context;
        }
        public async Task<User> Login(string username, string password)
        {
            // will get the user from the database
            // if no user exists, FirstOrDefaultAync will return null for username
            // if using FirstAsync, it would return an exception if no matching user is found
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            if (user == null)
                return null;
            
            // comparing passwords
            // method will return true or false
            // if null, will return error 401 not authorized
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        // Passing key in here to compute the hash
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                // this will compute the hash from our password but also using the key, passwordSalt we pass
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                // this computedHash is the same as below in CreatePassword because we're are comparing the password
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }
        
        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {   
            // Setting up PasswordSalts and PasswordHash
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                // Salt will provide random generated key
                passwordSalt = hmac.Key;
                // Computing The hash
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                //Values will be stored in the bytes array above
            }
        }
        //we use aysnc because we are searching all usernames
        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Username == username))
                return true;

            return false;
        }

    }
}