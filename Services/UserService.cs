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

        public void DeleteUser(ApplicationUser user)
        {
            db.Users.Remove(user);
            db.SaveChanges();
        }
    }
}
