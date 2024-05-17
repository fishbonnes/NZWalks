using Microsoft.EntityFrameworkCore;
using NZWalks2.API.Data;
using NZWalks2.API.Models.Domain;

namespace NZWalks2.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NZWalks2DBContext nZWalks2DBContext;

        public UserRepository(NZWalks2DBContext nZWalks2DBContext)
        {
            this.nZWalks2DBContext = nZWalks2DBContext;
        }
        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await nZWalks2DBContext.Users
                .FirstOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower() && x.Password == password);

            if (user == null) 
            {
                return null;
            }

            var userRoles = await nZWalks2DBContext.User_Roles.Where(x=> x.UserId == user.Id).ToListAsync();

            if (userRoles.Any()) 
            {
                user.Roles = new List<string>();
                foreach (var userRole in userRoles) 
                {
                    var role = await nZWalks2DBContext.Roles.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);
                    if (role != null)
                    {
                        user.Roles.Add(role.Name);
                    }
                }
            }

            user.Password = password;
            return user;
        }
    }
}
