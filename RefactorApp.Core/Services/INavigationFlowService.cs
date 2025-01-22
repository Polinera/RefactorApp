using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactorApp.Core.Services
{
    public interface INavigationFlowService
    {
        Task InitializeAppAsync();
        Task NavigateToHomeAsync();
        Task NavigateToHistoryAsync();
        Task NavigateToGoalAsync();
    }
}
