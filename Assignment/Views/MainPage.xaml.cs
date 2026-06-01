using Assignment.ViewModels;

namespace Assignment.Views;

public partial class MainPage : ContentPage
{
    private readonly RecipeViewModel _viewModel;

    public MainPage(RecipeViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Dispatcher.Dispatch(() =>
        {
            if (MainTitle != null && MainTitle.Handler != null)
                MainTitle.SetSemanticFocus();
        });
    }

    private async void OnAddRecipeClicked(object sender, EventArgs e)
    {
        try
        {
            _viewModel.AddRecipeCommand.Execute(null);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Cannot open add page: {ex.Message}", "OK");
        }
    }

    private void OnScanClicked(object sender, EventArgs e) => Shell.Current.GoToAsync("///BarcodeScanPage");
    private void OnVoiceClicked(object sender, EventArgs e) => Shell.Current.GoToAsync("///VoiceAssistantPage");
    private void OnSensorsClicked(object sender, EventArgs e) => Shell.Current.GoToAsync("///SensorsPage");
    private void OnSettingsClicked(object sender, EventArgs e) => Shell.Current.GoToAsync("///SettingsPage");
    private void OnCategoryChanged(object sender, EventArgs e) => _viewModel.FilterByCategoryCommand.Execute(((Picker)sender).SelectedItem as string);
}