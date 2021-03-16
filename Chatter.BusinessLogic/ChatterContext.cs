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
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        public string Text { get; set; }
        public DateTime SentDate { get; set; }
        public string SentBy { get; set; }
        public int GroupId { get; set; }

    }
}
