using ReactiveUI;
using RefactorApp.Models.Models;
using System.Collections.ObjectModel;
using System.Reactive;
using Newtonsoft.Json;
using System.ComponentModel;
using Microsoft.Extensions.Logging;
using RefactorApp.Core.Services;

namespace RefactorApp.Core.ViewModels
{
    public class HistoryViewModel : ReactiveObject, INotifyPropertyChanged
    {
        private readonly INavigationService _navigationService;
        private readonly ILogger<HistoryViewModel> _logger;

        private string _selectedItem;
        public string SelectedItem
        {
            get => Preferences.Get(nameof(SelectedItem), string.Empty);
            set
            {
                if (value != _selectedItem)
                {
                    _selectedItem = value;
                    Preferences.Set(nameof(SelectedItem), value);
                    this.RaisePropertyChanged(nameof(SelectedItem));
                }
            }
        }

        private string _image;
        public string Image
        {
            get => _image;
            set => this.RaiseAndSetIfChanged(ref _image, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        private string _routeTo;
        public string RouteTo
        {
            get => _routeTo;
            set => this.RaiseAndSetIfChanged(ref _routeTo, value);
        }

        public ReactiveCommand<HistoryModel, Unit> NavigateCommand { get; }

        public ObservableCollection<HistoryModel> HistoryItem { get; }

        public HistoryViewModel(INavigationService navigationService, ILogger<HistoryViewModel> logger)
        {
            _navigationService = navigationService;
            _logger = logger;

            NavigateCommand = ReactiveCommand.CreateFromTask<HistoryModel>(NavigateToDetailedView);
            HistoryItem = new ObservableCollection<HistoryModel>();

            _ = LoadJsonAsync();
        }

        private async Task LoadJsonAsync()
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("historyPeople.json");
                using var reader = new StreamReader(stream);
                string json = await reader.ReadToEndAsync();

                _logger.LogInformation("Loaded JSON: {Json}", json);

                var items = JsonConvert.DeserializeObject<List<HistoryModel>>(json);
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        _logger.LogInformation("Adding item: {Name}", item.Name);
                        HistoryItem.Add(item);
                    }
                }
                _logger.LogInformation("Total Items Loaded: {Count}", HistoryItem.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading JSON");
            }
        }

        private async Task NavigateToDetailedView(HistoryModel selectedPerson)
        {
            if (selectedPerson == null || string.IsNullOrEmpty(selectedPerson.RouteTo))
            {
                _logger.LogWarning("NavigateToDetailedView error: selectedPerson is null or missing RouteTo!");
                return;
            }

            _logger.LogInformation("Navigating to route: {Route}", selectedPerson.RouteTo);

            try
            {
                await _navigationService.NavigateToAsync(selectedPerson.RouteTo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Navigation error for route: {Route}", selectedPerson.RouteTo);
            }
        }
    }
}