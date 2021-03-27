﻿using Chatter.BusinessLogic.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Threading;

namespace Chatter.WpfClient.Services
{
    public class MessageObservable : IObservable<Message>
    {
        private List<IObserver<Message>> _observers;
        private HubConnection _hubConnection;
        private CancellationToken _cts { get; set; }
        public MessageObservable()
        {
            _observers = new List<IObserver<Message>>();
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44359/MessageHub")
                .Build();
            _hubConnection.On<Message>("RecieveMessage", OnMessage);
            _hubConnection.StartAsync(_cts).ContinueWith(x=> { });
        }

        public IDisposable Subscribe(IObserver<Message> observer)
        {
            _observers.Add(observer);
            return Disposable.Empty;
        }
        public void OnMessage(Message message)
        {
            foreach(var observer in _observers)
            {
                observer.OnNext(message);
            }
        }
    }
}

