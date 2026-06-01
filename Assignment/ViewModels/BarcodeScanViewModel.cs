using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Camera.MAUI;

namespace Assignment.ViewModels;

public partial class BarcodeScanViewModel : ObservableObject
{
    [ObservableProperty]
    private string scannedBarcode;

    [ObservableProperty]
    private string scanStatus = "Waiting for scan...";

    [ObservableProperty]
    private bool isScanning = true;

    public async void OnBarcodeDetected(Camera.MAUI.ZXingHelper.BarcodeEventArgs args)
    {
        if (!IsScanning) return;

        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            IsScanning = false;
            if (args?.Result != null && args.Result.Length > 0)
            {
                ScannedBarcode = args.Result[0].Text;
                ScanStatus = $"Scanned: {ScannedBarcode}";
                try { Vibration.Default.Vibrate(200); } catch { }
                SemanticScreenReader.Default.Announce($"Scan successful, barcode: {ScannedBarcode}");
                await Shell.Current.DisplayAlert("Barcode Scan", $"Barcode: {ScannedBarcode}", "OK");
            }
            else
            {
                ScanStatus = "No barcode detected, please retry";
                IsScanning = true;
                SemanticScreenReader.Default.Announce("No barcode detected, please retry");
            }
        });
    }

    [RelayCommand]
    public async Task LinkToRecipe()
    {
        if (string.IsNullOrWhiteSpace(ScannedBarcode))
        {
            await Shell.Current.DisplayAlert("Hint", "Please scan a barcode first", "OK");
            return;
        }

        var parameters = new Dictionary<string, object> { { "Barcode", ScannedBarcode } };
        await Shell.Current.GoToAsync("///AddEditRecipePage", parameters);
    }
}