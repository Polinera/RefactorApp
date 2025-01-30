using RefactorApp.Core.Services;

namespace RefactorApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = new Window(new AppShell());

            // Resolve the AppFlowService from Dependency Injection (DI)
            var appFlow = MauiProgram.Services.GetService<INavigationFlowService>();

            if (appFlow != null)
            {
                _ = InitializeAppFlowAsync(appFlow);  // Call async method safely
            }

            return window;
        }

        private async Task InitializeAppFlowAsync(INavigationFlowService appFlow)
        {
            await appFlow.InitializeAppAsync(); 
        }
    }
}