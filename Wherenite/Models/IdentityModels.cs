using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Wherenite.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Event> Events { get; set; }

        public DbSet<Venue> Venues { get; set; }

        public DbSet<VenueType> VenueTypes { get; set; }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<TicketCategory> TicketCategories { get; set; }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


    }
}