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
        #region Properties
       
        private bool _isAddGoalVisible;
        public bool IsAddGoalVisible
        {
            get => _isAddGoalVisible;
            set => this.RaiseAndSetIfChanged(ref _isAddGoalVisible, value);
        }

        private string _newGoalTitle;
        public string NewGoalTitle
        {
            get => _newGoalTitle;
            set => this.RaiseAndSetIfChanged(ref _newGoalTitle, value);
        }

        private string _newGoalDescription;
        public string NewGoalDescription
        {
            get => _newGoalDescription;
            set => this.RaiseAndSetIfChanged(ref _newGoalDescription, value);
        }

        private ObservableCollection<GoalModel> _goals;
        public ObservableCollection<GoalModel> Goals
        {
            get => _goals;
            set => this.RaiseAndSetIfChanged(ref _goals, value);
        }

        private ObservableCollection<GoalModel> _filteredGoals;
        public ObservableCollection<GoalModel> FilteredGoals
        {
            get => _filteredGoals;
            set => this.RaiseAndSetIfChanged(ref _filteredGoals, value);
        }

        private string _selectedFilter;
        public string SelectedFilter
        {
            get => _selectedFilter;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedFilter, value);
                UpdateFilteredGoals();
            }
        }

        #endregion

        #region Commands

        public ReactiveCommand<Unit, Unit> ToggleAddGoalCommand { get; }

        public ReactiveCommand<Unit, Unit> AddGoalCommand { get; }

        public ReactiveCommand<GoalModel, Unit> OpenGoalCommand { get; }

        public ReactiveCommand<GoalModel, Unit> MarkCompleteCommand { get; }

        public ReactiveCommand<GoalModel, Unit> DeleteGoalCommand { get; }
  
        public ReactiveCommand<string, Unit> SetFilterCommand { get; }

        #endregion

        #region Constructor

        public GoalViewModel()
        {
            IsAddGoalVisible = false;
            NewGoalTitle = string.Empty;
            NewGoalDescription = string.Empty;

            Goals = LoadGoals();
            FilteredGoals = new ObservableCollection<GoalModel>(Goals);
            SelectedFilter = "Ongoing"; 

            ToggleAddGoalCommand = ReactiveCommand.Create(() =>
            {
                IsAddGoalVisible = !IsAddGoalVisible;
            });

            AddGoalCommand = ReactiveCommand.Create(AddGoal);

            OpenGoalCommand = ReactiveCommand.Create<GoalModel>(OpenGoal);

            MarkCompleteCommand = ReactiveCommand.Create<GoalModel>(MarkComplete);

            DeleteGoalCommand = ReactiveCommand.Create<GoalModel>(DeleteGoal);

            SetFilterCommand = ReactiveCommand.Create<string>(filter =>
            {
                SelectedFilter = filter;
            });
        }

        #endregion

        #region Methods

        private void AddGoal()
        {
            if (!string.IsNullOrWhiteSpace(NewGoalTitle))
            {
                var goal = new GoalModel
                {
                    Title = NewGoalTitle,
                    Description = NewGoalDescription,
                    IsCompleted = false
                };

                Goals.Add(goal);
                SaveGoalToFile(goal);
                UpdateFilteredGoals();

                NewGoalTitle = "";
                NewGoalDescription = "";
                IsAddGoalVisible = false;
            }
        }

        private void OpenGoal(GoalModel goal)
        {
            if (goal == null)
                return;
            Shell.Current.GoToAsync($"goalDetail?title={goal.Title}");
        }

        private void MarkComplete(GoalModel goal)
        {
            if (goal == null)
                return;

            goal.IsCompleted = true;
            UpdateGoalFile(goal);
            UpdateFilteredGoals();
        }

        private void DeleteGoal(GoalModel goal)
        {
            if (goal == null)
                return;

            Goals.Remove(goal);
            DeleteGoalFile(goal);
            UpdateFilteredGoals();
        }

        private void UpdateFilteredGoals()
        {
            var filtered = Goals.AsEnumerable();
            if (SelectedFilter.Equals("Ongoing", StringComparison.OrdinalIgnoreCase))
                filtered = Goals.Where(g => !g.IsCompleted);
            else if (SelectedFilter.Equals("Completed", StringComparison.OrdinalIgnoreCase))
                filtered = Goals.Where(g => g.IsCompleted);

            FilteredGoals = new ObservableCollection<GoalModel>(filtered);
        }

        private string GoalsFolderPath => FileSystem.AppDataDirectory;

        private void SaveGoalToFile(GoalModel goal)
        {
            string safeFileName = GetSafeFileName(goal.Title);
            string filePath = Path.Combine(GoalsFolderPath, $"{safeFileName}.json");

            string json = JsonConvert.SerializeObject(goal, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        private void UpdateGoalFile(GoalModel goal)
        {
            string safeFileName = GetSafeFileName(goal.Title);
            string filePath = Path.Combine(GoalsFolderPath, $"{safeFileName}.json");

            string json = JsonConvert.SerializeObject(goal, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        private void DeleteGoalFile(GoalModel goal)
        {
            string safeFileName = GetSafeFileName(goal.Title);
            string filePath = Path.Combine(GoalsFolderPath, $"{safeFileName}.json");

            if (File.Exists(filePath))
                File.Delete(filePath);
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
                catch (Exception ex)
                {
                    Console.WriteLine($"Skipping file: {file} due to error: {ex.Message}");
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

        #endregion
    }
}