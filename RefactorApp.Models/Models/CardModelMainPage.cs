using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace RefactorApp.Models.Models
{
    public class CardModelMainPage:ReactiveObject
    {
        private string _title;
        private string _image;
        private string _routeTo;
        private ReactiveCommand<string, Unit> _navigateCommand;

        // Title displayed on the card.
        public string Title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }

        // Image source for the card.
        public string Image
        {
            get => _image;
            set => this.RaiseAndSetIfChanged(ref _image, value);
        }

        // The route to navigate to when the card is tapped.
        public string RouteTo
        {
            get => _routeTo;
            set => this.RaiseAndSetIfChanged(ref _routeTo, value);
        }

        // The command that will be executed when the card is tapped.
        public ReactiveCommand<string, Unit> NavigateCommand
        {
            get => _navigateCommand;
            set => this.RaiseAndSetIfChanged(ref _navigateCommand, value);
        }
    }
}