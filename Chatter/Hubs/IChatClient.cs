using Chatter.BusinessLogic.Models;
using System.Threading.Tasks;

namespace Chatter.API.Hubs
{
    public interface IChatClient
    {
        Task RecieveMessage(Message message);
    }

}
