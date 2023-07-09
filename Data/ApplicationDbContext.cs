using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketAPP.Models;

namespace TicketAPP.Data;

public class ApplicationDbContext : IdentityDbContext
{

    // DbSet<Model>
    public DbSet<Project> Projects { get; set; }
    public DbSet<Models.Task> Tasks { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}
