using Chatter.BusinessLogic.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.ConsoleClient
{
    class Program
    {

        static async Task Main(string[] args)
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri("https://localhost:44359");

            HubConnection _connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44359/MessageHub")
                .Build();

            _connection.Closed += async (error) =>
            {
                Console.WriteLine("CorBlimey");
                await _connection.StartAsync();
            };

            _connection.On<Message>("RecieveMessage", (message) =>
            {
                Console.WriteLine($"{message.SentBy}: {message.Text}");
            });
            await _connection.StartAsync();

            while(true)
            {
                var message = new Message() { SentBy = "Shyam", GroupId = 1, SentDate = DateTime.Now };
                message.Text = Console.ReadLine();
                var json = JsonConvert.SerializeObject(message);
                var response = await http.PostAsync("/Message/SendMessage", new StringContent(json, Encoding.UTF8, "application/json"));
            }
        }
        
    }
}
