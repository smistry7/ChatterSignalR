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
    public class MainWindowViewModel : ViewModelBase
    {
        ViewModelBase _content;
        public MainWindowViewModel(MessageViewModel messageViewModel)
        {
            _content = MessageViewModel = messageViewModel;
        }
        public MessageViewModel MessageViewModel { get; }
        public ViewModelBase Content
        {
            get => _content;
            private set => this.RaiseAndSetIfChanged(ref _content, value);
        }
    }
}
