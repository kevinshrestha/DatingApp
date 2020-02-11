namespace DatingApp.API.Models
{
    public class User
    {   
        public int Id { get; set; }
        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        //Salt will act as a key so that we're able to recreate the hash and compare it's like for like against the password the user types in
        public byte[] PasswordSalt { get; set; }
    }
}