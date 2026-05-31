using Assignment.ViewModels;
using Camera.MAUI;
using System.Diagnostics;

namespace Assignment.Views;

public partial class BarcodeScanPage : ContentPage
{
    private readonly BarcodeScanViewModel _viewModel;

    public BarcodeScanPage(BarcodeScanViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    private void OnBarcodeDetected(object sender, Camera.MAUI.ZXingHelper.BarcodeEventArgs args)
    {
        _viewModel.OnBarcodeDetected(args);
    }

    private async void CameraViewCamerasLoaded(object sender, EventArgs e)
    {
        Debug.WriteLine("CameraViewCamerasLoaded called");
        if (cameraView.Cameras.Count > 0)
        {
            cameraView.Camera = cameraView.Cameras.First();
            await cameraView.StartCameraAsync();
        }
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        try
        {
            await Shell.Current.GoToAsync("///MainPage");
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Back failed: {ex.Message}");
            await DisplayAlert("Error", $"Cannot go back: {ex.Message}", "OK");
        }
    }
}