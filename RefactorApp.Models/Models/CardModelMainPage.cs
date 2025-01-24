using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace RefactorApp.Models.Models
{
    public class CardModelMainPage
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public string RouteTo { get; set; }
        public ReactiveCommand<string, Unit> NavigateCommand { get; set; }
    }
}
