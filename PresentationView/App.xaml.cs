using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using PresentationView;
using PresentationViewModel;
using PresentationModel;
using BusinessLogic;

namespace PresentationView
{
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;

        public App()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IBallLogic, BallLogic>();
            services.AddSingleton<BallModel>();
            services.AddSingleton<BallPresentationVM>();
            services.AddTransient<MainWindow>();

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            var viewModel = _serviceProvider.GetRequiredService<BallPresentationVM>();
            
            mainWindow.DataContext = viewModel;
            
            mainWindow.Show();
        }
    }
}
