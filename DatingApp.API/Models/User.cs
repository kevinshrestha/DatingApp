using System;
using System.Collections.Generic;

namespace DatingApp.API.Models
{
    public class User
    {   
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        //Salt will act as a key so that we're able to recreate the hash and compare it's like for like against the password the user types in
        public byte[] PasswordSalt { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string LookingFor { get; set; }
        public string Introduction { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        // Collection of Photos. Here we are saying single user can have many photos
        public ICollection<Photo> Photos { get; set; }
    }
}