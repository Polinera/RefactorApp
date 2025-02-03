using RefactorApp.Core.ViewModels;

namespace RefactorApp.UI.Views;

public partial class JournalPage
{
	public JournalPage(JournalViewModel viewModel)
	{
		InitializeComponent();
        ViewModel = viewModel;
    }
}