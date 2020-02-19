using System.Collections.Generic;
using System.Linq;
using DatingApp.API.Models;
using Newtonsoft.Json;

namespace DatingApp.API.Data
{
    public class Seed
    {
        // we don't want to create a new instance of this class.
        // we're going to pass an instance of our data context inside here
        public static void SeedUsers(DataContext context)
        {
            if (!context.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);
                foreach (var user in users)
                {
                    byte[] passwordhash, passwordSalt;
                    CreatePasswordHash("password", out passwordhash, out passwordSalt);

                    user.PasswordHash = passwordhash;
                    user.PasswordSalt = passwordSalt;
                    user.Username = user.Username.ToLower();
                    context.Users.Add(user);
                }
                // this doesn't have to be aync because it's going to be called when our application starts up and it'll be called once.
                context.SaveChanges();
            }
        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
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
    }
}