using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MassageStudioSolarisProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<Client>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Service> Services { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ServiceType> ServicesTypes { get; set; }
        public DbSet<Specialist> Specialists { get; set; }
    }
}
