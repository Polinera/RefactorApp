using ReactiveUI;
using RefactorApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace RefactorApp.Core.ViewModels
{
    public class HistoryViewModel : ReactiveObject, INotifyPropertyChanged
    {
        public ReactiveCommand<HistoryModel, Unit> NavigateCommand { get; }

        private string _image;
        private string _name;
        private string _routeTo;

        public string Image
        {
            get => _image;
            set => this.RaiseAndSetIfChanged(ref _image, value);
        }

        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        public string RouteTo
        {
            get => _routeTo;
            set => this.RaiseAndSetIfChanged(ref _routeTo, value);
        }

        public ObservableCollection<HistoryModel> HistoryItem { get; set; }

        public HistoryViewModel()
        {
            NavigateCommand = ReactiveCommand.CreateFromTask<HistoryModel>(async (selectedPerson) =>
            {
                await NavigateToDetailedView(selectedPerson);
            });

            HistoryItem = new ObservableCollection<HistoryModel>();

            LoadJson();
        }

        private async void LoadJson()
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("historyPeople.json");
                using var reader = new StreamReader(stream);
                string json = await reader.ReadToEndAsync();

                Console.WriteLine($"Loaded JSON: {json}");

                var items = JsonConvert.DeserializeObject<List<HistoryModel>>(json);

                if (items != null)
                {
                    foreach (var item in items)
                    {
                        Console.WriteLine($"Adding item: {item.Name}");
                        HistoryItem.Add(item);
                    }
                }

                Console.WriteLine($"Total Items Loaded: {HistoryItem.Count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading JSON: {ex.Message}");
            }
        }

        private string _selectedItem;
        public string SelectedItem
        {
            get => Preferences.Get("SelectedItem", null);
            set
            {
                if (value != _selectedItem)
                {
                    _selectedItem = value;
                    Preferences.Set("SelectedItem", value);
                    OnPropertyChanged(nameof(SelectedItem));
                }
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        private async Task NavigateToDetailedView(HistoryModel selectedPerson)
        {
            if (selectedPerson == null || string.IsNullOrEmpty(selectedPerson.RouteTo))
            {
                Console.WriteLine("NavigateToDetailedView ERROR: selectedPerson is NULL or missing RouteTo!");
                return;
            }

            Console.WriteLine($"Navigating to route: {selectedPerson.RouteTo}");

            try
            {
                await Shell.Current.GoToAsync(selectedPerson.RouteTo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Navigation Error: {ex.Message}");
            }

        }
    }
}