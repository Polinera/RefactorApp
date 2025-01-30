using Microsoft.Extensions.Logging;
using RefactorApp.Core.Services;
using RefactorApp.Core.ViewModels;
using RefactorApp.UI.Views;

namespace RefactorApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("TwinkleStar-Regular.ttf", "TwinkleStar");
                    fonts.AddFont("PoiretOne-Regular.ttf", "PoiretOne-Regular");
                });

#if DEBUG
    		builder.Logging.AddDebug();    
#endif
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            builder.Services.AddSingleton<INavigationFlowService, NavigationFlowService>();

            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddTransient<GoalPage>();
            builder.Services.AddTransient<HistoryPage>();
            builder.Services.AddTransient<GoalViewModel>();
            builder.Services.AddTransient<HistoryViewModel>();
            builder.Services.AddTransient<DetailedHistoryPage>();
            builder.Services.AddTransient<DetailedHistoryViewModel>();

            var app = builder.Build();
           
            Services = app.Services;

            return app;
        }
        public static IServiceProvider Services { get; private set; }
    }
}
