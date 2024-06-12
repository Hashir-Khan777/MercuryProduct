using MecuryProduct.Data;
using Microsoft.EntityFrameworkCore;
using Radzen;
using System.Text.Json;

namespace MecuryProduct.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext db;
        private readonly NotificationService notificationService;
        private readonly HelperService helperService;

        /// <summary>Initializes a new instance of the UserService class.</summary>
        /// <param name="db">The application's database context.</param>
        /// <param name="notificationService">The notification service used for sending notifications.</param>
        public UserService(ApplicationDbContext db, NotificationService notificationService, HelperService helperService)
        {
            this.db = db;
            this.notificationService = notificationService;
            this.helperService = helperService;
        }

        /// <summary>
        /// Retrieves a list of users based on a specific claim.
        /// </summary>
        /// <param name="claimType">The type of claim to search for.</param>
        /// <param name="claimValue">The value of the claim to search for.</param>
        /// <returns>A list of ApplicationUser objects that match the specified claim.</returns>
        /// <remarks>If no users are found or an exception occurs, null is returned.</remarks>
        public List<ApplicationUser>? GetUsersByClaim(string claimType, string claimValue)
        {
            try
            {
                var allClaims = db.UserClaims.Where(c => c.ClaimType == claimType && c.ClaimValue == claimValue).ToList();

                List<ApplicationUser> allUsers = new List<ApplicationUser>();

                foreach (var claim in allClaims)
                {
                    ApplicationUser? user = db.Users.Include(u => u.driver_cars).Include(c => c.Company).FirstOrDefault(u => u.Id == claim.UserId && u.EmailConfirmed);

                    if (user is not null)
                    {
                        allUsers.Add(user);
                    }
                }
                return allUsers;
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: JsonSerializer.Serialize(ex));
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<ApplicationUser>? GetUsersByClaimByManagerId(string claimType, string claimValue, string ManagerId)
        {
            try
            {
                var allClaims = db.UserClaims.Where(c => c.ClaimType == claimType && c.ClaimValue == claimValue).ToList();

                List<ApplicationUser> allUsers = new List<ApplicationUser>();

                foreach (var claim in allClaims)
                {
                    ApplicationUser? user = db.Users.Include(u => u.driver_cars).Include(c => c.Company).FirstOrDefault(u => u.Id == claim.UserId && u.Company.ManagerId == ManagerId && u.EmailConfirmed);

                    if (user is not null)
                    {
                        allUsers.Add(user);
                    }
                }
                return allUsers;
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: JsonSerializer.Serialize(ex));
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<ApplicationUser>? GetUsersByClaimByCompanyId(string claimType, string claimValue, int? CompanyId)
        {
            try
            {
                var allClaims = db.UserClaims.Where(c => c.ClaimType == claimType && c.ClaimValue == claimValue).ToList();

                List<ApplicationUser> allUsers = new List<ApplicationUser>();

                foreach (var claim in allClaims)
                {
                    ApplicationUser? user = db.Users.Include(u => u.driver_cars).Include(c => c.Company).FirstOrDefault(u => u.Id == claim.UserId && u.CompanyId == CompanyId && u.EmailConfirmed);

                    if (user is not null)
                    {
                        allUsers.Add(user);
                    }
                }
                return allUsers;
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: JsonSerializer.Serialize(ex));
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<ApplicationUser> GetAllUsers()
        {
            try
            {
                return db.Users.Include(u => u.Company).Include(u => u.companies).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: JsonSerializer.Serialize(ex));
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public List<ApplicationUser> GetAllUsersByManagerId(string ManagerId)
        {
            try
            {
                return db.Users.Where(u => u.Company.ManagerId == ManagerId).Include(u => u.Company).Include(u => u.companies).ToList();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: JsonSerializer.Serialize(ex));
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        public string GetUserClaimByUserId(string UserId)
        {
            try
            {
                var claim = db.UserClaims.FirstOrDefault(c => c.UserId == UserId && c.ClaimType == "Role");
                return claim.ClaimValue;
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: JsonSerializer.Serialize(ex));
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        /// <summary>
        /// Retrieves a user by their ID from the database.
        /// </summary>
        /// <param name="Id">The ID of the user to retrieve.</param>
        /// <returns>The ApplicationUser object corresponding to the provided ID, including related entities like driver cars, customers, and creators.</returns>
        /// <remarks>If the user is not found or an exception occurs during the retrieval process, a notification message is sent and null is returned.</remarks>
        public ApplicationUser? GetUserById(string Id)
        {
            try
            {
                return db.Users.Include(u => u.driver_cars).ThenInclude(c => c.customer).Include(u => u.driver_cars).ThenInclude(c => c.created_by).FirstOrDefault(u => u.Id == Id);
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: JsonSerializer.Serialize(ex));
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        /// <summary>Deletes a user from the database.</summary>
        /// <param name="user">The user to be deleted.</param>
        /// <remarks>This method removes the specified user from the database and saves the changes. 
        /// If an exception occurs during the deletion process, an error notification is displayed.</remarks>
        public void DeleteUser(ApplicationUser user)
        {
            try
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: JsonSerializer.Serialize(ex));
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }

        /// <summary>Sets the driver ID for a user in the database.</summary>
        /// <param name="driverId">The driver ID to set for the user.</param>
        /// <remarks>If the driver ID is already assigned to a user, it will increment the maximum driver ID by 1 and assign it to the user. If no driver ID is assigned yet, it will set the driver ID to 1.</remarks>
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
                }
                else
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
                helperService.WriteLog(exception: JsonSerializer.Serialize(ex));
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }

        /// <summary>Sets the old passwords for a user.</summary>
        /// <param name="Id">The user's ID.</param>
        /// <param name="password">The password to set as an old password.</param>
        /// <returns>True if the password was successfully set as an old password, false if the password already exists in the old passwords list, null if the user is not found.</returns>
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
                helperService.WriteLog(exception: JsonSerializer.Serialize(ex));
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
                return null;
            }
        }

        /// <summary>Sets the password for a user with the specified ID.</summary>
        /// <param name="id">The ID of the user.</param>
        /// <param name="password">The new password to set.</param>
        public void SetUserPassword(string id, string password)
        {
            try
            {
                var user = db.Users.FirstOrDefault(u => u.Id == id);
                if (user != null)
                {
                    user.password = password;
                }
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: JsonSerializer.Serialize(ex));
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }

        /// <summary>Sets the email for a user in the database.</summary>
        /// <param name="id">The user's ID.</param>
        /// <param name="email">The email to set for the user.</param>
        /// <remarks>This method updates the email, normalized email, username, and normalized username for the user in the database.</remarks>
        /// <exception cref="Exception">Thrown when an error occurs while setting the email.</exception>
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
                helperService.WriteLog(exception: JsonSerializer.Serialize(ex));
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }

        public void UpdateUser(ApplicationUser user)
        {
            try
            {
                db.Users.Update(user);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                helperService.WriteLog(exception: JsonSerializer.Serialize(ex));
                var notificationMessage = new NotificationMessage { Severity = NotificationSeverity.Error, Detail = ex.Message, Duration = 4000 };
                notificationService.Notify(notificationMessage);
            }
        }
    }
}
