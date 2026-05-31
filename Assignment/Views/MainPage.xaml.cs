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
        // 延迟并安全设置焦点，确保屏幕阅读器从页面顶部开始朗读
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
            await DisplayAlert("错误", $"无法打开添加页面: {ex.Message}", "确定");
        }
    }

    private void OnScanClicked(object sender, EventArgs e) => Shell.Current.GoToAsync("///BarcodeScanPage");
    private void OnVoiceClicked(object sender, EventArgs e) => Shell.Current.GoToAsync("///VoiceAssistantPage");
    private void OnSensorsClicked(object sender, EventArgs e) => Shell.Current.GoToAsync("///SensorsPage");
    private void OnSettingsClicked(object sender, EventArgs e) => Shell.Current.GoToAsync("///SettingsPage");
    private void OnCategoryChanged(object sender, EventArgs e) => _viewModel.FilterByCategoryCommand.Execute(((Picker)sender).SelectedItem as string);
}