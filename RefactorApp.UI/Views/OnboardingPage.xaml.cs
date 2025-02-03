using RefactorApp.Core.ViewModels;

namespace RefactorApp.UI.Views;

public partial class OnboardingPage : ContentPage
{
	public OnboardingPage()
	{
		InitializeComponent();
		BindingContext = new OnboardingViewModel();
    }

}