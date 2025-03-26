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
    public class ReflectionsViewModel : ReactiveObject
    {
        private readonly INavigationService _navigationService;

        public ObservableCollection<ReflectionsModel> ReflectionsModelsItem { get; }
        public ReactiveCommand<string, Unit> NavigateCommand { get; }

        public ReflectionsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ReflectionsModelsItem = new ObservableCollection<ReflectionsModel>();

            NavigateCommand = ReactiveCommand.CreateFromTask<string>(NavigateTo);

            AddEntryToModel(); 
        }

        private void AddEntryToModel()
        {
            ReflectionsModelsItem.Add(new ReflectionsModel
            {
                Title = "Manage goals",
                RouteTo = "goal",
                NavigateCommand = NavigateCommand
            });
            ReflectionsModelsItem.Add(new ReflectionsModel
            {
                Title = "Your mood",
                RouteTo = "moodTracker",
                NavigateCommand = NavigateCommand
            });
            ReflectionsModelsItem.Add(new ReflectionsModel
            {
                Title = "Journal",
                RouteTo = "journal",
                NavigateCommand = NavigateCommand
            });
        }

        private async Task NavigateTo(string routeTo)
        {
            await _navigationService.NavigateToAsync(routeTo);
        }
    }
}