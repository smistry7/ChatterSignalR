using Chatter.BusinessLogic.Models;
using System;
using System.Threading.Tasks;

namespace Chatter.ConsoleClient
{
    public class ChatClient : IObserver<Message>
    {
        private readonly IHubConnectionProvider _hubConnectionProvider;
        private readonly IMessageService _messageService;

        public ChatClient(IHubConnectionProvider hubConnectionProvider, IMessageService messageService)
        {
            _hubConnectionProvider = hubConnectionProvider;
            _messageService = messageService;
            _hubConnectionProvider.Subscribe(this);

        }
        public async Task JoinGroup(int groupId)
        {
            await _hubConnectionProvider.JoinGroup(groupId);
        }

        public void OnCompleted()
        {
            Console.WriteLine("completed?");
        }

        public void OnError(Exception error)
        {
            throw new Exception(error.Message);
        }

        public void OnNext(Message message)
        {
            Console.WriteLine(message);
        }

        public async Task SendMessage(string messageText, int groupId)
        {
            var message = new Message()
            {
                Text = messageText,
                SentBy = "Console",
                GroupId = groupId,
                SentDate = DateTime.Now
            };
            await _messageService.SendMessage(message).ConfigureAwait(false);
        }
        
    }
}
