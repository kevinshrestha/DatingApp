using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _context;

        public DatingRepository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            // when we add into our context, this is going to be saved in memory until we save our changes in our DB
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            // when we delete into our context, this is going to be saved in memory until we save our changes in our DB
            _context.Remove(entity);
        }
        // we pass in the id VVV        
        public async Task<User> GetUser(int id)
        {
            // then the id is used to extract the first or default from our DB that matches the id we're passing
            // but if we don't find a user, we return Default and it'll be null
            var user = await  _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);

            // If we do find a user, we return it
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            // inclused photos 
            var users = await _context.Users.Include(p => p.Photos).ToListAsync();

            return users;
        }

        public async Task<bool> SaveAll()
        {
            // if it returns more than zero, it'll return true
            // if it returns zero, it'll return false
            return await _context.SaveChangesAsync() > 0;
        }
    }
}