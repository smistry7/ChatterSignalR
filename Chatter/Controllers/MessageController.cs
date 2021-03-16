using Chatter.BusinessLogic;
using Chatter.BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MessageController : ControllerBase
    {
        private readonly ChatterContext _chatterContext;
        public MessageController(ChatterContext chatterContext)
        {
            _chatterContext = chatterContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetMessages()
        {
            var messages = _chatterContext.Messages
                .OrderByDescending(x => x.SentDate)
                .Take(10);
            return Ok(messages);
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] Message message)
        {
            _chatterContext.Messages.Add(message);
            await _chatterContext.SaveChangesAsync();
            return Ok();
        }

    }
}
