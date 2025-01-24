using RefactorApp.Core.ViewModels;

namespace RefactorApp.UI.Views;

public partial class HomePage 
{
    public HomePage(HomeViewModel viewModel)
	{
		InitializeComponent();
        ViewModel = viewModel;
    }
}