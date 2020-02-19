using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    public interface IDatingRepository
    {
        //Type of T and T is going to add type User or add type Photo
         void Add<T>(T entity) where T: class;
        //Type of T and T is going to delete type User or delete type Photo
         void Delete<T>(T entity) where T: class;
         // Checks to saves any changes 
         Task<bool> SaveAll();
         Task<IEnumerable<User>> GetUsers();
         Task<User> GetUser(int id);
    }
}