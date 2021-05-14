using Chatter.BusinessLogic.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.WebClient.Services
{
    public class HubConnectionProvider : IHubConnectionProvider
    {
        private HubConnection _hubConnection;
        private readonly string _apiUrl;
        private List<IObserver<Message>> _observers = new();
        public HubConnectionProvider(HttpClient configuration)
        {
            _apiUrl = configuration.BaseAddress.AbsoluteUri;
            BuildSignalrConnection().ContinueWith(x => { });
        }
        public async Task BuildSignalrConnection()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(_apiUrl + "MessageHub")
                .Build();
            _hubConnection.On<Message>("RecieveMessage", OnMessage);
            _hubConnection.Closed += OnClosed;
            await _hubConnection.StartAsync().ConfigureAwait(false);
        }

        public async Task JoinGroup(int groupId)
        {
            if (_hubConnection.State == HubConnectionState.Connected)
            {
                await _hubConnection.SendAsync("JoinGroup", groupId).ConfigureAwait(false);
            }
        }
        private void OnMessage(Message message)
        {
            //do something
            foreach (var observer in _observers)
            {
                observer.OnNext(message);
            }
        }
        private async Task OnClosed(Exception e)
        {
            await _hubConnection.StartAsync();
        }

        public IDisposable Subscribe(IObserver<Message> observer)
        {
            _observers.Add(observer);
            return Disposable.Empty;
        }
    }
}
