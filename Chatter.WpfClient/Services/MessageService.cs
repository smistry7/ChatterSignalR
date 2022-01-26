using Chatter.APIClient;
using Chatter.BusinessLogic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.WpfClient.Services
{
    public class MessageService : IMessageService
    {
        private IChatterApi _http;
        public MessageService(string apiUrl)
        {
            var http = new HttpClient()
            {
                BaseAddress = new Uri(apiUrl)
            };
            _http = new ApiClientFactory(http).BuildChatterApi();

        }
        public async Task SendMessage(Message message)
        {
            await _http.SendMessage(message);
        }
        public async Task<IEnumerable<Message>> GetMessages()
        {
            var response = await _http.GetMessages();
            return response;
        }
    }
}

