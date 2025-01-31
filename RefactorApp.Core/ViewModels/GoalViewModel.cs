using ReactiveUI;
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
        private string _newGoal;

        public ObservableCollection<string> Goals { get; }

        public string NewGoal
        {
            get => _newGoal;
            set => this.RaiseAndSetIfChanged(ref _newGoal, value);
        }

        public ReactiveCommand<Unit, Unit> AddGoalCommand { get; }

        public GoalViewModel()
        {
            Goals = new ObservableCollection<string>();

            AddGoalCommand = ReactiveCommand.Create(() =>
            {
                if (!string.IsNullOrWhiteSpace(NewGoal))
                {
                    Goals.Add(NewGoal);
                    NewGoal = ""; 
                }
            });
        }
    }
}