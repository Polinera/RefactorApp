using ReactiveUI;
using RefactorApp.Core.ViewModels;

namespace RefactorApp.UI.Views;

public partial class GoalPage 
{
    public GoalViewModel ViewModel { get; }
    public GoalPage(GoalViewModel viewModel)
	{
		InitializeComponent();
		ViewModel = viewModel;
	}
}