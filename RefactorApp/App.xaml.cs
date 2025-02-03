using RefactorApp.Core.Services;

namespace RefactorApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

      
    }
}