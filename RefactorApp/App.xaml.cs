using RefactorApp.Core.Services;
using RefactorApp.UI.Views;

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