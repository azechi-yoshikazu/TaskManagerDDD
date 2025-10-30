using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using TaskManager.Infrastructure;

namespace TaskManager.App;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var serviceCollection = new ServiceCollection();
        
        serviceCollection.AddWpfBlazorWebView();
        serviceCollection.AddBlazorWebViewDeveloperTools();

        serviceCollection.AddInfrastructureServices();
        
        
        var mainWindow = new MainWindow();
        mainWindow.Resources.Add("services", serviceCollection.BuildServiceProvider());

        mainWindow.Show();
    }
}

