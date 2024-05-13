using Microsoft.AspNetCore.Identity;

namespace MecuryProduct.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public int? driverId {  get; set; }
        public string password {  get; set; } = string.Empty;
        public List<string> oldThreePasswords { get; set; } = new List<string>();
        public List<CarModel>? cars { get; set; }
        public List<CarModel>? driver_cars { get; set; }
        public List<NoteModel>? notes { get; set; }
        public List<CustomerModel>? customers { get; set; }
    }

}
