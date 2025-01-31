using Newtonsoft.Json;
using ReactiveUI;
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
    public class GoalViewModel : ReactiveObject
    {
        private string _newGoalTitle;
        private string _newGoalDescription;

        public ObservableCollection<GoalModel> Goals { get; set; }

        public string NewGoalTitle
        {
            get => _newGoalTitle;
            set => this.RaiseAndSetIfChanged(ref _newGoalTitle, value);
        }

        public string NewGoalDescription
        {
            get => _newGoalDescription;
            set => this.RaiseAndSetIfChanged(ref _newGoalDescription, value);
        }

        public ReactiveCommand<Unit, Unit> AddGoalCommand { get; }

        private string GoalsFolderPath => FileSystem.AppDataDirectory;

        public GoalViewModel()
        {
            Goals = LoadGoals(); 

            AddGoalCommand = ReactiveCommand.Create(() =>
            {
                if (!string.IsNullOrWhiteSpace(NewGoalTitle))
                {
                    var goal = new GoalModel
                    {
                        Title = NewGoalTitle,
                        Description = NewGoalDescription
                    };

                    Goals.Add(goal);
                    SaveGoalToFile(goal); 
                    NewGoalTitle = "";
                    NewGoalDescription = "";
                }
            });
        }

        private void SaveGoalToFile(GoalModel goal)
        {
            string safeFileName = GetSafeFileName(goal.Title);
            string filePath = Path.Combine(GoalsFolderPath, $"{safeFileName}.json");

            if (!File.Exists(filePath))
            {
                string json = JsonConvert.SerializeObject(goal, Formatting.Indented);
                File.WriteAllText(filePath, json);
            }
        }

        private ObservableCollection<GoalModel> LoadGoals()
        {
            var goals = new ObservableCollection<GoalModel>();

            if (!Directory.Exists(GoalsFolderPath))
                return goals;

            foreach (var file in Directory.GetFiles(GoalsFolderPath, "*.json"))
            {
                try
                {
                    string json = File.ReadAllText(file);
                    var goal = JsonConvert.DeserializeObject<GoalModel>(json);
                    if (goal != null)
                        goals.Add(goal);
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Skipping file: {file} due to error: {ex.Message}");
                    continue;
                }
            }

            return goals;
        }

        private string GetSafeFileName(string title)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                title = title.Replace(c, '_');
            }
            return title;
        }
    }
}