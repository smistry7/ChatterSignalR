using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Chatter.BusinessLogic.Models;
using Chatter.WpfClient.Services;
using Chatter.WpfClient.ViewModels;
using Chatter.WpfClient.Views;
using Microsoft.Extensions.Configuration;
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
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection, config);
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
        private void ConfigureServices(IServiceCollection services, IConfiguration config) 
        {
            services.AddSingleton<IObservable<Message>>(x=> new MessageObservable(config["api_url"]));
            services.AddTransient<IMessageService>(x => new MessageService(config["api_url"]));
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<MessageViewModel>();
            services.AddSingleton<MainWindow>();
            
        }
       
    }
}
