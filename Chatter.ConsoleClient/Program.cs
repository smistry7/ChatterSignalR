using Chatter.BusinessLogic.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
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
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            var apiUrl = config["api_url"];

            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(apiUrl);

            HubConnection _connection = new HubConnectionBuilder()
                .WithUrl(apiUrl + "/MessageHub")
                .Build();

            _connection.Closed += async (error) =>
            {
                Console.WriteLine("CorBlimey");
                await _connection.StartAsync();
            };

            _connection.On<Message>("RecieveMessage", (message) =>
            {
                Console.WriteLine(message);
            });
            await _connection.StartAsync();

            Console.WriteLine("please enter the id of the group you want to join");
            var groupId = int.Parse(Console.ReadLine());
            await _connection.SendAsync("JoinGroup", groupId);

            while (true)
            {
                var message = new Message() { SentBy = "Console", GroupId = groupId, SentDate = DateTime.Now };
                message.Text = Console.ReadLine();
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                var json = JsonConvert.SerializeObject(message);
                var response = await http.PostAsync("/Message/SendMessage",
                    new StringContent(json, Encoding.UTF8, "application/json"));
            }
        }

    }
}
