using RefactorApp.Core.ViewModels;

namespace RefactorApp.UI.Views;

public partial class InspiringView
{
	public InspiringView(InspiringViewModel viewModel)
	{
		InitializeComponent();
		ViewModel = viewModel;
    }
}