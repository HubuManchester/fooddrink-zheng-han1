namespace Assignment.Views;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
    }

    private void OnDarkModeToggled(object sender, ToggledEventArgs e)
    {
        Application.Current.UserAppTheme = e.Value ? AppTheme.Dark : AppTheme.Light;
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