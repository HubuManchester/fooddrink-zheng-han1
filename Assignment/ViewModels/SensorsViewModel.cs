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
    private string sensorStatus = "Not monitoring";

    public SensorsViewModel()
    {
        if (Accelerometer.Default.IsSupported)
            SensorStatus = "Accelerometer available, tap to start";
        else
            SensorStatus = "Device does not support accelerometer";
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
            Shell.Current.DisplayAlert("Error", "Device does not support accelerometer", "OK");
            return;
        }
        try
        {
            Accelerometer.Default.ReadingChanged += OnAccelerometerReadingChanged;
            Accelerometer.Default.Start(SensorSpeed.Game);
            IsMonitoring = true;
            SensorStatus = "Monitoring (real-time)";
        }
        catch (Exception ex)
        {
            Shell.Current.DisplayAlert("Error", $"Cannot start accelerometer: {ex.Message}", "OK");
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
        SensorStatus = "Stopped";
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