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
    public class HomeViewModel : ReactiveObject
    {
        public ReactiveCommand<string, Unit> NavigateCommand { get; }

        public ObservableCollection<CardModelMainPage> CardModelItem { get; }

        public HomeViewModel()
        {
            NavigateCommand = ReactiveCommand.CreateFromTask<string>(NavigateToPage);

            CardModelItem = new ObservableCollection<CardModelMainPage>();

            AddEntryToModel();
        }

        private void AddEntryToModel()
        {
            CardModelItem.Add(new CardModelMainPage
            {
                Title = "Your Goals",
                Image = "goalmainimage",
                RouteTo = "goal",
                NavigateCommand = NavigateCommand
            });

            CardModelItem.Add(new CardModelMainPage
            {
                Title = "History Overview",
                Image = "historymainimage",
                RouteTo = "history",
                NavigateCommand = NavigateCommand
            });

            CardModelItem.Add(new CardModelMainPage
            {
                Title = "Reflections",
                Image = "historymainimage",
                RouteTo = "reflections",
                NavigateCommand = NavigateCommand
            });
        }

        public async Task NavigateToPage(string routeTo)
        {
            if (string.IsNullOrEmpty(routeTo)) return;

            await Shell.Current.GoToAsync($"{routeTo}");
        }
    }
}