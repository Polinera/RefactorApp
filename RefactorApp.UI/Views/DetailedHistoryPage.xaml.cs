using RefactorApp.Core.ViewModels;

namespace RefactorApp.UI.Views;

public partial class DetailedHistoryPage
{
    private readonly DetailedHistoryViewModel _viewModel;
    public DetailedHistoryPage(DetailedHistoryViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        // Extract route ID from Shell.Current
        string route = Shell.Current.CurrentState.Location.ToString();
        string routeId = route.Split('/').Last(); // Extract "MarieCurie" or "AdaLovelace"

        await _viewModel.LoadPersonData(routeId);
    }
}