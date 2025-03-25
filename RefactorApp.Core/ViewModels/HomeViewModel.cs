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

            HabitModelItem = new ObservableCollection<HabitsModel>();
            QuoteModelItem = new ObservableCollection<QuoteModel>();

            FirstQuote = new QuoteModel
            {
                Quote = "sss",
                Author = "Ssss"
            };

            AddEntryToModel();
            LoadQuotesFromJson();
        }

        private void AddEntryToModel()
        {
            HabitModelItem.Add(new HabitsModel
            {
                HabitName = "Exercise",
                IsCompleted = false
            });
            HabitModelItem.Add(new HabitsModel
            {
                HabitName = "Drink water",
                IsCompleted = false
            });
            HabitModelItem.Add(new HabitsModel
            {
                HabitName = "Read book",
                IsCompleted = false
            });
            HabitModelItem.Add(new HabitsModel
            {
                HabitName = "Exercise",
                IsCompleted = false
            });
            HabitModelItem.Add(new HabitsModel
            {
                HabitName = "Drink water",
                IsCompleted = false
            });
            HabitModelItem.Add(new HabitsModel
            {
                HabitName = "Read book",
                IsCompleted = false
            });


        }

        public async Task NavigateToPage(string routeTo)
        {
            if (string.IsNullOrEmpty(routeTo)) return;

            await Shell.Current.GoToAsync($"{routeTo}");
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