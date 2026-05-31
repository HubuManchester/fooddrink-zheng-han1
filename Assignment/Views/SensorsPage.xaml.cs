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
            System.Diagnostics.Debug.WriteLine($"返回失败: {ex.Message}");
            await DisplayAlert("错误", $"无法返回: {ex.Message}", "确定");
        }
    }
}