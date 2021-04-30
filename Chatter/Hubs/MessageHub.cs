using Chatter.BusinessLogic.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.API.Hubs
{
    public class MessageHub : Hub<IChatClient>
    {
        public async Task SendMessage(Message message)
        {
            await Clients.Group(message.GroupId.ToString()).RecieveMessage(message);
        }
        public async Task JoinGroup(int groupId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId.ToString());
        }
        public async Task LeaveGroup(int groupId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId.ToString());
        }
        
    }

}
