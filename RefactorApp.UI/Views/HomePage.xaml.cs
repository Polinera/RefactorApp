using RefactorApp.Core.ViewModels;

namespace RefactorApp.UI.Views;

public partial class HomePage 
{
    public HomeViewModel ViewModel { get; }
    public HomePage(HomeViewModel viewModel)
	{
		InitializeComponent();
        ViewModel = viewModel;
    }
}