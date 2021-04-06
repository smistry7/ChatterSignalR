using Chatter.API.Hubs;
using Chatter.BusinessLogic;
using Chatter.BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        private readonly IHubContext<MessageHub> _hubContext;
        public MessageController(ChatterContext chatterContext, IHubContext<MessageHub> hubContext)
        {
            _chatterContext = chatterContext;
            _hubContext = hubContext;
        }
        [HttpGet]
        public IActionResult GetMessages()
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
            await _hubContext.Clients.All.SendAsync("RecieveMessage", message);
            return Ok();
        }

    }
}
