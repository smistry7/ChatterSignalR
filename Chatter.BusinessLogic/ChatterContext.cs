using Chatter.BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

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
