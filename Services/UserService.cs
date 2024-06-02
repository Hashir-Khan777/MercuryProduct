using MecuryProduct.Data;
using Microsoft.EntityFrameworkCore;
using Radzen;

namespace MecuryProduct.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext db;
        private readonly NotificationService notificationService;

        public UserService(ApplicationDbContext db, NotificationService notificationService)
        {
            this.db = db;
            this.notificationService = notificationService;
        }

        public List<ApplicationUser>? GetUsersByClaim(string claimType, string claimValue)
        {
            try
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
            catch (Exception ex)
            {
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public ApplicationUser? GetUserById(string Id)
        {
            try
            {
                return db.Users.Include(u => u.driver_cars).ThenInclude(c => c.customer).Include(u => u.driver_cars).ThenInclude(c => c.created_by).FirstOrDefault(u => u.Id == Id);
            }
            catch (Exception ex)
            {
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public void DeleteUser(ApplicationUser user)
        {
            try
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }

        public void SetDriverId(string driverId)
        {
            try
            {
                int? maxId = db.Users.Max(u => u.driverId);
                var user = db.Users.FirstOrDefault(u => u.Id == driverId);
                if (maxId != null)
                {
                    if (user != null)
                    {
                        user.driverId = maxId.Value + 1;
                        db.SaveChanges();
                    }
                } else
                {
                    if (user != null)
                    {
                        user.driverId = 1;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }

        public bool? SetOldThreePasswords(string Id, string password)
        {
            try
            {
                var user = db.Users.FirstOrDefault(u => u.Id == Id);
                if (user != null)
                {
                    if (user.oldThreePasswords.IndexOf(password) < 0)
                    {
                        if (user.oldThreePasswords.Count() < 3)
                        {
                            user.oldThreePasswords.Add(password);
                            db.SaveChanges();
                        }
                        else
                        {
                            user.oldThreePasswords.RemoveAt(0);
                            user.oldThreePasswords.Add(password);
                            db.SaveChanges();
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public void SetUserPassword(string id, string password)
        {
            try
            {
                var user = db.Users.FirstOrDefault(u => u.Id == id);
                if(user != null)
                {
                    user.password = password;
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }

        public void SetUserEmail(string id, string email)
        {
            try
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
            catch (Exception ex)
            {
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }
    }
}
