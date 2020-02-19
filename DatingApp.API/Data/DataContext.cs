using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DataContext : DbContext
    {
        //Constructor
        //DataConent -> Type/Class
        public DataContext(DbContextOptions<DataContext> options) : base(options){}

        public DbSet<Value> Values { get; set; }
        //whenever we make a change inside our models either we create a new class or modify the properties inside the class in the models
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}