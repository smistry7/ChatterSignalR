using Chatter.BusinessLogic.Models;
using Chatter.WpfClient.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatter.WpfClient.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IObserver<Message>
    {
        ViewModelBase _content;
        private readonly IMessageService _messageService;
        public MainWindowViewModel(IObservable<Message> messageObservable, IMessageService messageService)
        {
            _content = MessageViewModel = new MessageViewModel();
            messageObservable.Subscribe(this);
            _messageService = messageService;
            
        }
        public MessageViewModel MessageViewModel { get; }
        public ViewModelBase Content
        {
            get => _content;
            private set => this.RaiseAndSetIfChanged(ref _content, value);
        }
        public async Task AddItem()
        {
            var vm = new AddMessageViewModel();
            // take the first result from either ok or Cancel and perform the delegate
            Observable.Merge(vm.Ok, vm.Cancel.Select(_ => (Message)null))
               .Take(1)
               .Subscribe(async model =>
               {
                   if (model != null)
                   {
                       await _messageService.SendMessage(model);
                   }

                   Content = MessageViewModel;
               });

            Content = vm;
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(Message value)
        {
            this.MessageViewModel.Messages.Add(value);
        }
    }
}
