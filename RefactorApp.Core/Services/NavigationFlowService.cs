using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactorApp.Core.Services
{
    public class NavigationFlowService : INavigationFlowService
    {
        private readonly INavigationService _navigationService;

        public NavigationFlowService(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public async Task InitializeAppAsync()
        {
            await NavigateToHomeAsync();
        }

        public async Task NavigateToGoalAsync()
        {
            await _navigationService.NavigateToAsync("goal");
        }

        public async Task NavigateToHistoryAsync()
        {
            await _navigationService.NavigateToAsync("history");
        }

        public async Task NavigateToHomeAsync()
        {
            await _navigationService.NavigateToAsync("home");
        }
    }
}
