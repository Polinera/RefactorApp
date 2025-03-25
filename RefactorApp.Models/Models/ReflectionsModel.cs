using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace RefactorApp.Models.Models
{
    public class ReflectionsModel:ReactiveObject
    {
        private string _title;
        private string _routeTo;
        private ReactiveCommand<string, Unit> _navigateCommand;

        public string Title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }

        public string RouteTo
        {
            get => _routeTo;
            set => this.RaiseAndSetIfChanged(ref _routeTo, value);
        }

        public ReactiveCommand<string, Unit> NavigateCommand
        {
            get => _navigateCommand;
            set => this.RaiseAndSetIfChanged(ref _navigateCommand, value);
        }
    }
}
