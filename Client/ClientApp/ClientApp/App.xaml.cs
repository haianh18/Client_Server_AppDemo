using System.Windows;

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public string server = "127.0.0.1";
        public int port = 1500;
        public ServerConnection serverConnection = new ServerConnection();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            serverConnection.ConnectServer(server, port);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            serverConnection.DisconnectServer();
        }
    }

}
