using auth_API.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace auth_API.DAO.Implementations; 

public class UserDao : IUserDao {
    private AuthenticationContext context;

    public UserDao(AuthenticationContext context) {
        this.context = context;
    }

    public async Task<User> CreateAsync(DTO.User user) {
        var converted = new User {
            Email = user.Email,
            Password = user.Password
        };
        var newUser = await context.Users.AddAsync(converted);
        await context.SaveChangesAsync();
        
        return newUser.Entity;
    }

    public async Task<User> GetAsync(string email) {
        var query = context.Users.AsQueryable();
        var users = await query.Where(u => u.Email.Equals(email)).ToListAsync();

        if (users.Count < 1) {
            throw new NotFoundException($"User with email {email} not found!");
        }

        return users[0];
    }

    public async Task<User> UpdateAsync(long uuid, DTO.User user) {
        var oldUser = await context.Users.FindAsync(uuid);
        oldUser.Email = user.Email;
        oldUser.Password = user.Password;
        context.Update(oldUser);
        await context.SaveChangesAsync();

        return oldUser;
    }
}