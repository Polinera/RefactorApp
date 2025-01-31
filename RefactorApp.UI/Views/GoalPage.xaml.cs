using ReactiveUI;
using RefactorApp.Core.ViewModels;

namespace RefactorApp.UI.Views;

public partial class GoalPage 
{
    public GoalPage(GoalViewModel viewModel)
	{
		InitializeComponent();
		ViewModel = viewModel;
    }
}