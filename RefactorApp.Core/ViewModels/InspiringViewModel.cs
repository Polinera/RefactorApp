using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive;

namespace RefactorApp.Core.ViewModels
{
    public class InspiringViewModel
    {
        public ReactiveCommand<string, Unit> NavigateCommand { get; }

        public InspiringViewModel()
        {
            NavigateCommand = ReactiveCommand.CreateFromTask<string>(NavigateToPage);
        }

        public async Task NavigateToPage(string routeTo)
        {
            if (string.IsNullOrEmpty(routeTo)) return;

            await Shell.Current.GoToAsync($"{routeTo}");
        }
    }
}
