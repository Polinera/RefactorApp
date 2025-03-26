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
using Newtonsoft.Json;

namespace RefactorApp.Core.ViewModels
{
    public class HomeViewModel : ReactiveObject
    {
        public ReactiveCommand<HabitsModel, Unit> ToggleHabitCommand { get; }
        private QuoteModel _firstQuote;
        public QuoteModel FirstQuote
        {
            get => _firstQuote;
            set => this.RaiseAndSetIfChanged(ref _firstQuote, value);
        }
        private readonly Random _random = new();

        public ReactiveCommand<string, Unit> NavigateCommand { get; }

        public ObservableCollection<HabitsModel> HabitModelItem { get; }
        public ObservableCollection<QuoteModel> QuoteModelItem { get; }

        public HomeViewModel()
        {
            
            

            NavigateCommand = ReactiveCommand.CreateFromTask<string>(NavigateToPage);

            ToggleHabitCommand = ReactiveCommand.Create<HabitsModel>(habit =>
            {
                if (habit != null)
                {
                    habit.IsCompleted = !habit.IsCompleted;
                    
                    this.RaisePropertyChanged(nameof(HabitModelItem));
                }
            });

            HabitModelItem = new ObservableCollection<HabitsModel>();
            QuoteModelItem = new ObservableCollection<QuoteModel>();

            LoadHabitsFromJson();
            LoadQuotesFromJson();

            Console.WriteLine("Loaded habits:");
            foreach (var habit in HabitModelItem)
            {
                Console.WriteLine($"Habit: {habit.Name}");
            }
        }

        public async Task NavigateToPage(string routeTo)
        {
            if (string.IsNullOrEmpty(routeTo)) return;

            await Shell.Current.GoToAsync($"{routeTo}");
        }

        private async Task LoadHabitsFromJson()
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("habits.json");
                using var reader = new StreamReader(stream);
                string jsonContent = await reader.ReadToEndAsync();

                var habits = JsonConvert.DeserializeObject<List<HabitsModel>>(jsonContent);
                if (habits != null && habits.Any())
                {
                    foreach (var habit in habits)
                        HabitModelItem.Add(habit);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load habits: {ex.Message}");
            }
        }

        private async Task LoadQuotesFromJson()
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("quoteMainPage.json");
                using var reader = new StreamReader(stream);
                string jsonContent = await reader.ReadToEndAsync();

                var quotes = JsonConvert.DeserializeObject<List<QuoteModel>>(jsonContent);
                if (quotes != null && quotes.Any())
                {
                    foreach (var quote in quotes)
                        QuoteModelItem.Add(quote);

                    SetRandomQuote();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load quotes: {ex.Message}");
            }
        }

        private void SetRandomQuote()
        {
            if (QuoteModelItem.Count > 0)
            {
                var randomIndex = _random.Next(QuoteModelItem.Count);
                FirstQuote = QuoteModelItem[randomIndex];
            }

            else
            {
                FirstQuote = new QuoteModel
                {
                    Quote = "No quotes available",
                    Author = "Unknown"
                };
            }

        }
    }
}