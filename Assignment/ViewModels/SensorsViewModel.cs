using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Devices.Sensors;

namespace Assignment.ViewModels;

public partial class SensorsViewModel : ObservableObject
{
    [ObservableProperty]
    private double x;

    [ObservableProperty]
    private double y;

    [ObservableProperty]
    private double z;

    [ObservableProperty]
    private bool isMonitoring = false;

    [ObservableProperty]
    private string sensorStatus = "未监控";

    public SensorsViewModel()
    {
        if (Accelerometer.Default.IsSupported)
            SensorStatus = "加速计可用，点击启动";
        else
            SensorStatus = "设备不支持加速计";
    }

    [RelayCommand]
    private void ToggleSensor()
    {
        if (IsMonitoring) StopSensor();
        else StartSensor();
    }

    private void StartSensor()
    {
        if (!Accelerometer.Default.IsSupported)
        {
            Shell.Current.DisplayAlert("错误", "设备不支持加速计", "确定");
            return;
        }
        try
        {
            Accelerometer.Default.ReadingChanged += OnAccelerometerReadingChanged;
            Accelerometer.Default.Start(SensorSpeed.Game);
            IsMonitoring = true;
            SensorStatus = "监控中 (实时)";
        }
        catch (Exception ex)
        {
            Shell.Current.DisplayAlert("错误", $"无法启动加速计: {ex.Message}", "确定");
        }
    }

    private void StopSensor()
    {
        if (Accelerometer.Default.IsMonitoring)
        {
            Accelerometer.Default.ReadingChanged -= OnAccelerometerReadingChanged;
            Accelerometer.Default.Stop();
        }
        IsMonitoring = false;
        SensorStatus = "已停止";
        X = Y = Z = 0;
    }

    private void OnAccelerometerReadingChanged(object sender, AccelerometerChangedEventArgs e)
    {
        var data = e.Reading.Acceleration;
        MainThread.BeginInvokeOnMainThread(() =>
        {
            X = data.X;
            Y = data.Y;
            Z = data.Z;
        });
    }
}