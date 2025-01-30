using RefactorApp.Core.ViewModels;

namespace RefactorApp.UI.Views;

public partial class HistoryPage 
{
    public HistoryPage(HistoryViewModel viewModel)
	{
		InitializeComponent();
		ViewModel = viewModel;
    }
}