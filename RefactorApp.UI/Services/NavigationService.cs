using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RefactorApp.Core.Services;

namespace RefactorApp.UI.Services
{
    public class NavigationService : INavigationService
    {
        public async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }

        public async Task NavigateToAsync(string route, IDictionary<string, object>? parameters = null)
        {
            if (parameters != null)
            {
                await Shell.Current.GoToAsync(route, parameters);
            }
            else
            {
                await Shell.Current.GoToAsync(route);
            }
        }
    }
}
