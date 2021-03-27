using Chatter.BusinessLogic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chatter.WpfClient.Services
{
    public interface IMessageService
    {
        Task<IEnumerable<Message>> GetMessages();
        Task SendMessage(Message message);
    }
}