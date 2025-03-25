using ReactiveUI.Maui;
using RefactorApp.Core.ViewModels;

namespace RefactorApp.UI.Views;

public partial class ReflectionsPage 
{
	public ReflectionsPage(ReflectionsViewModel viewModel)
	{
		InitializeComponent();
        ViewModel = viewModel;
    }
}