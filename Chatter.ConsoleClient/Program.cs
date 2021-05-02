using Chatter.BusinessLogic.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chatter.ConsoleClient
{
    class Program
    {
        private static ServiceProvider _serviceProvider;
        static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(config);
            services.AddTransient<IHubConnectionProvider, HubConnectionProvider>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddSingleton<ChatClient>();
            _serviceProvider = services.BuildServiceProvider();
            var client = _serviceProvider.GetService<ChatClient>();

            Console.WriteLine("please enter the id of the group you want to join");
            int groupId;
            while (!int.TryParse(Console.ReadLine(), out groupId))
            {
                Console.WriteLine("please enter a valid id");
            }
            Console.WriteLine("Joining...");
            await client.JoinGroup(groupId).ContinueWith(x => 
            {
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                Console.WriteLine("joined! please enter a message and hit enter");
            });
            while (true)
            {
                var text = Console.ReadLine();
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                await client.SendMessage(text, groupId);
            }
        }

    }
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
