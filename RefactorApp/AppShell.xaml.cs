using RefactorApp.UI.Views;

namespace RefactorApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("home", typeof(HomePage));
            Routing.RegisterRoute("goal", typeof(GoalPage));
            Routing.RegisterRoute("history", typeof(HistoryPage));
            Routing.RegisterRoute("detailedHistoryView", typeof(DetailedHistoryPage));
            Routing.RegisterRoute("DetailedHistoryView/MarieCurie", typeof(DetailedHistoryPage));
            Routing.RegisterRoute("DetailedHistoryView/AdaLovelace", typeof(DetailedHistoryPage));
        }


    }
}
