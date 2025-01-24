using RefactorApp.Core.ViewModels;

namespace RefactorApp.UI.Views;

public partial class HistoryPage 
{
    public HistoryViewModel ViewModel { get; }
    public HistoryPage(HistoryViewModel viewModel)
	{
		InitializeComponent();
	}
}