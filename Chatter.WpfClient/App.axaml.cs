using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Chatter.BusinessLogic.Models;
using Chatter.WpfClient.Services;
using Chatter.WpfClient.ViewModels;
using Chatter.WpfClient.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace Chatter.WpfClient
{
    public class App : Application
    {
        private ServiceProvider _serviceProvider;
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow()
                {
                    DataContext = _serviceProvider.GetService<MainWindowViewModel>()
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IObservable<Message>, MessageObservable>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<MainWindow>();
            
        }
       
    }
}
