using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Camera.MAUI;

namespace Assignment.ViewModels;

public partial class BarcodeScanViewModel : ObservableObject
{
    [ObservableProperty]
    private string scannedBarcode;

    [ObservableProperty]
    private string scanStatus = "等待扫描...";

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
                ScanStatus = $"已扫描: {ScannedBarcode}";
                try { Vibration.Default.Vibrate(200); } catch { }
                SemanticScreenReader.Default.Announce($"扫描成功，条码内容：{ScannedBarcode}");
                await Shell.Current.DisplayAlert("条码扫描", $"条码内容: {ScannedBarcode}", "确定");
            }
            else
            {
                ScanStatus = "未检测到条码，请重试";
                IsScanning = true;
                SemanticScreenReader.Default.Announce("未检测到条码，请重试");
            }
        });
    }

    [RelayCommand]
    public async Task LinkToRecipe()
    {
        if (string.IsNullOrWhiteSpace(ScannedBarcode))
        {
            await Shell.Current.DisplayAlert("提示", "请先扫描条码", "确定");
            return;
        }

        var parameters = new Dictionary<string, object> { { "Barcode", ScannedBarcode } };
        await Shell.Current.GoToAsync("///AddEditRecipePage", parameters);
    }
}