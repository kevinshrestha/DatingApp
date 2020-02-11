using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    public interface IAuthRepository
    {
        //creating new user
        Task<User> Register(User user, string password);
        //logging with username and password
        Task<User> Login(string username, string password);
        //checking if username exists
        Task<bool> UserExists(string username);

    }
}