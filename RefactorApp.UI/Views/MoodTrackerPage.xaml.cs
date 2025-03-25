using RefactorApp.Core.ViewModels;

namespace RefactorApp.UI.Views;

public partial class MoodTrackerPage 
{
	public MoodTrackerPage(MoodTrackerViewModel viewModel)
	{
		InitializeComponent();
		ViewModel = viewModel;
    }
}