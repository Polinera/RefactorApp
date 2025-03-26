﻿using RefactorApp.UI.Views;

namespace RefactorApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("home", typeof(HomePage));
            Routing.RegisterRoute("onboarding", typeof(OnboardingPage));
            Routing.RegisterRoute("goal", typeof(GoalPage));
            Routing.RegisterRoute("history", typeof(HistoryPage));
            Routing.RegisterRoute("journal", typeof(JournalPage));
            Routing.RegisterRoute("reflections", typeof(ReflectionsPage));
            Routing.RegisterRoute("moodTracker", typeof(MoodTrackerPage));
            Routing.RegisterRoute("detailedHistoryView", typeof(DetailedHistoryPage));
            Routing.RegisterRoute("detailedHistoryView/MarieCurie", typeof(DetailedHistoryPage));
            Routing.RegisterRoute("detailedHistoryView/AdaLovelace", typeof(DetailedHistoryPage)); 
            Routing.RegisterRoute("detailedHistoryView/NelsonMandela", typeof(DetailedHistoryPage));
            Routing.RegisterRoute("detailedHistoryView/AlbertEinstein", typeof(DetailedHistoryPage));
            Routing.RegisterRoute("detailedHistoryView/FridaKahlo", typeof(DetailedHistoryPage));
            Routing.RegisterRoute("detailedHistoryView/MalalaYousafzai", typeof(DetailedHistoryPage));
            Routing.RegisterRoute("detailedHistoryView/LeonardoDaVinci", typeof(DetailedHistoryPage));
            Routing.RegisterRoute("detailedHistoryView/RosaParks", typeof(DetailedHistoryPage));
            Routing.RegisterRoute("detailedHistoryView/StephenHawking", typeof(DetailedHistoryPage));
            Routing.RegisterRoute("detailedHistoryView/AmeliaEarhart", typeof(DetailedHistoryPage));
            Routing.RegisterRoute("detailedHistoryView/MahatmaGandhi", typeof(DetailedHistoryPage));
            Routing.RegisterRoute("detailedHistoryView/KatherineJohnson", typeof(DetailedHistoryPage));
            Routing.RegisterRoute("detailedHistoryView/HelenKeller", typeof(DetailedHistoryPage));
            Routing.RegisterRoute("detailedHistoryView/SteveJobs", typeof(DetailedHistoryPage));
            Routing.RegisterRoute("detailedHistoryView/JaneGoodall", typeof(DetailedHistoryPage));
            Routing.RegisterRoute("inspiring", typeof(InspiringView));

        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            bool isFirstTimeUser = Preferences.Get("IsFirstTimeUser", true);
            if (isFirstTimeUser)
            {
                await Shell.Current.GoToAsync("onboarding");
            }
        }
    }
}
