using Chatter.BusinessLogic.Models;
using System.Threading.Tasks;

namespace Chatter.ConsoleClient
{
    public interface IMessageService
    {
        Task<bool> SendMessage(Message message);
    }
}