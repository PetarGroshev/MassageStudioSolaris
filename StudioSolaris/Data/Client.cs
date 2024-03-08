using Microsoft.AspNetCore.Identity;

namespace StudioSolaris.Data
{
    public class Client: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateModified { get; set; } = DateTime.Now;
        public ICollection<Reservation> Reservations { get; set; }
        
    }
}
