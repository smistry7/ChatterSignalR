﻿using Chatter.BusinessLogic.Models;
using System;
using System.Threading.Tasks;

namespace Chatter.ConsoleClient
{
    public interface IHubConnectionProvider : IObservable<Message>
    {
        Task BuildSignalrConnection();
        Task JoinGroup(int groupId);
    }
}