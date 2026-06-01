using Assignment.ViewModels;

namespace Assignment.Views;

public partial class SensorsPage : ContentPage
{
    public SensorsPage(SensorsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Dispatcher.Dispatch(() =>
        {
            if (PageTitle != null && PageTitle.Handler != null)
                PageTitle.SetSemanticFocus();
        });
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        try
        {
            await Shell.Current.GoToAsync("///MainPage");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Back failed: {ex.Message}");
            await DisplayAlert("Error", $"Cannot go back: {ex.Message}", "OK");
        }
    }
}