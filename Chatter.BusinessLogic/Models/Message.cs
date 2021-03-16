using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.BusinessLogic.Models
{
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
