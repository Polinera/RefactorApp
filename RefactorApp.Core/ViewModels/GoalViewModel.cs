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

        // Controls whether the add-goal panel is visible.
        private bool _isAddGoalVisible;
        public bool IsAddGoalVisible
        {
            get => _isAddGoalVisible;
            set => this.RaiseAndSetIfChanged(ref _isAddGoalVisible, value);
        }

        // Input for a new goal title.
        private string _newGoalTitle;
        public string NewGoalTitle
        {
            get => _newGoalTitle;
            set => this.RaiseAndSetIfChanged(ref _newGoalTitle, value);
        }

        // Input for a new goal description.
        private string _newGoalDescription;
        public string NewGoalDescription
        {
            get => _newGoalDescription;
            set => this.RaiseAndSetIfChanged(ref _newGoalDescription, value);
        }

        // The master collection of goals.
        private ObservableCollection<GoalModel> _goals;
        public ObservableCollection<GoalModel> Goals
        {
            get => _goals;
            set => this.RaiseAndSetIfChanged(ref _goals, value);
        }

        // A filtered view of the goals (bound to the UI list).
        private ObservableCollection<GoalModel> _filteredGoals;
        public ObservableCollection<GoalModel> FilteredGoals
        {
            get => _filteredGoals;
            set => this.RaiseAndSetIfChanged(ref _filteredGoals, value);
        }

        // The current filter: "All", "Ongoing", or "Completed".
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

        // Toggles the add-goal input panel.
        public ReactiveCommand<Unit, Unit> ToggleAddGoalCommand { get; }

        // Adds a new goal.
        public ReactiveCommand<Unit, Unit> AddGoalCommand { get; }

        // Opens a detailed view of a goal.
        public ReactiveCommand<GoalModel, Unit> OpenGoalCommand { get; }

        // Marks a goal as complete.
        public ReactiveCommand<GoalModel, Unit> MarkCompleteCommand { get; }

        // Deletes a goal.
        public ReactiveCommand<GoalModel, Unit> DeleteGoalCommand { get; }

        // Sets the current filter.
        public ReactiveCommand<string, Unit> SetFilterCommand { get; }

        #endregion

        #region Constructor

        public GoalViewModel()
        {
            // Initialize properties.
            IsAddGoalVisible = false;
            NewGoalTitle = string.Empty;
            NewGoalDescription = string.Empty;

            Goals = LoadGoals();
            FilteredGoals = new ObservableCollection<GoalModel>(Goals);
            SelectedFilter = "Ongoing"; // Default filter.

            // Initialize commands.
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

                // Clear the inputs and hide the add-goal panel.
                NewGoalTitle = "";
                NewGoalDescription = "";
                IsAddGoalVisible = false;
            }
        }

        private void OpenGoal(GoalModel goal)
        {
            if (goal == null)
                return;

            // For example, navigate to a detail page (using query parameters).
            // You can customize this to open a modal or expand an inline detail.
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

        // Updates the FilteredGoals collection based on the SelectedFilter.
        private void UpdateFilteredGoals()
        {
            var filtered = Goals.AsEnumerable();
            if (SelectedFilter.Equals("Ongoing", StringComparison.OrdinalIgnoreCase))
                filtered = Goals.Where(g => !g.IsCompleted);
            else if (SelectedFilter.Equals("Completed", StringComparison.OrdinalIgnoreCase))
                filtered = Goals.Where(g => g.IsCompleted);
            // If "All", no filter is applied.
            FilteredGoals = new ObservableCollection<GoalModel>(filtered);
        }

        // Returns the folder path where goal JSON files are stored.
        private string GoalsFolderPath => FileSystem.AppDataDirectory;

        // Saves a new goal as a JSON file.
        private void SaveGoalToFile(GoalModel goal)
        {
            string safeFileName = GetSafeFileName(goal.Title);
            string filePath = Path.Combine(GoalsFolderPath, $"{safeFileName}.json");

            string json = JsonConvert.SerializeObject(goal, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        // Updates an existing goal's JSON file.
        private void UpdateGoalFile(GoalModel goal)
        {
            string safeFileName = GetSafeFileName(goal.Title);
            string filePath = Path.Combine(GoalsFolderPath, $"{safeFileName}.json");

            string json = JsonConvert.SerializeObject(goal, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        // Deletes a goal's JSON file.
        private void DeleteGoalFile(GoalModel goal)
        {
            string safeFileName = GetSafeFileName(goal.Title);
            string filePath = Path.Combine(GoalsFolderPath, $"{safeFileName}.json");

            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        // Loads existing goals from JSON files.
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

        // Replaces invalid filename characters.
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