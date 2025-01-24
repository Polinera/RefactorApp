using ReactiveUI;
using RefactorApp.Core.Services;
using RefactorApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace RefactorApp.Core.ViewModels
{
    public class HomeViewModel:ReactiveObject
    {
        private readonly INavigationService _navigationService;
        public ObservableCollection<CardModelMainPage> MainMenuCardItem { get; }

        public ReactiveCommand<string, Unit> NavigateCommand { get; }

        public HomeViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));

            MainMenuCardItem = new ObservableCollection<CardModelMainPage>
        {
            new CardModelMainPage { Image = "historymainimage.jpg", Title = "Histories of powerful people", RouteTo = "history" },
            new CardModelMainPage { Image = "goalmainimage.jpg", Title = "Manage your goals", RouteTo = "goal" }
            
        };

            // Update Command to Take the Entire Model Object
            NavigateCommand = ReactiveCommand.CreateFromTask<string>(NavigateToPage);
        }


        public async Task NavigateToPage(string pageName)
        {
            if (string.IsNullOrEmpty(pageName)) return;

            switch (pageName)
            {
                case "Page1":
                    await Shell.Current.GoToAsync("InspiringHistryView");
                    break;
                case "Page2":
                    await Shell.Current.GoToAsync("GoalView");
                    break;
                case "Page3":
                    await Shell.Current.GoToAsync("PersonalityQuizView");
                    break;
                default:
                    break;
            }
        }
    }
}
