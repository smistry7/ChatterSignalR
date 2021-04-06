using Chatter.BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;

namespace Chatter.BusinessLogic
{
    public class ChatterContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public ChatterContext(DbContextOptions<ChatterContext> options) : base(options)
        {
        }
    }
}
