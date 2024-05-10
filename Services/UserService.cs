using MecuryProduct.Data;
using Microsoft.EntityFrameworkCore;

namespace MecuryProduct.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext db;

        public UserService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<ApplicationUser> GetUsersByClaim(string claimType, string claimValue)
        {
            var allClaims = db.UserClaims.Where(c => c.ClaimType == claimType && c.ClaimValue == claimValue).ToList();

            List<ApplicationUser> allUsers = new List<ApplicationUser>();

            foreach (var claim in allClaims)
            {
                ApplicationUser? user = db.Users.Include(u => u.driver_cars).FirstOrDefault(u => u.Id == claim.UserId && u.EmailConfirmed);

                if (user is not null)
                {
                    allUsers.Add(user);
                }
            }
            return allUsers;
        }

        public ApplicationUser? GetUserById(string Id)
        {
            return db.Users.Include(u => u.driver_cars).ThenInclude(c => c.customer).Include(u => u.driver_cars).ThenInclude(c => c.created_by).FirstOrDefault(u => u.Id == Id);
        }

        public void DeleteUser(ApplicationUser user)
        {
            db.Users.Remove(user);
            db.SaveChanges();
        }

        public void SetUserPassword(string id, string password)
        {
            var user = db.Users.FirstOrDefault(u => u.Id == id);
            if(user != null)
            {
                user.password = password;
            }
            db.SaveChanges();
        }

        public void SetUserEmail(string id, string email)
        {
            var user = db.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.Email = email.ToLower();
                user.NormalizedEmail = email.ToUpper();
                user.UserName = email.ToLower();
                user.NormalizedUserName = email.ToUpper();
            }
            db.SaveChanges();
        }
    }
}
