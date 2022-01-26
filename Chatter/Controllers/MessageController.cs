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
        public ActionResult<IEnumerable<Message>> GetMessages()
        {
            var messages = _chatterContext.Messages
                .OrderByDescending(x => x.SentDate)
                .Take(10);
            return Ok(messages.ToList());
        }
        [HttpGet]
        public async Task<ActionResult<Message>> GetMessage([FromQuery] int id)
        {
            var message = await _chatterContext.Messages.FindAsync(id).ConfigureAwait(false);
            return Ok(message);
        }
        [HttpPost]
        public async Task<ActionResult<int>> SendMessage([FromBody] Message message)
        {
            var res = _chatterContext.Messages.Add(message);
            await _chatterContext.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("RecieveMessage", message);
            return Ok(res.Entity.MessageId);
        }

    }
}
