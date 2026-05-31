using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Assignment.ViewModels;

public partial class VoiceAssistantViewModel : ObservableObject
{
    private CancellationTokenSource _cts;

    [ObservableProperty]
    private string textToSpeak = "Welcome to voice assistant, please enter the text to read.";

    [ObservableProperty]
    private double speechVolume = 1.0;

    [ObservableProperty]
    private double speechPitch = 1.0;

    [ObservableProperty]
    private bool isSpeaking = false;

    [RelayCommand]
    private async Task Speak()
    {
        if (IsSpeaking)
        {
            _cts?.Cancel();
            await Task.Delay(100);
        }

        if (string.IsNullOrWhiteSpace(TextToSpeak))
        {
            await Shell.Current.DisplayAlert("Hint", "Please enter text to read", "OK");
            return;
        }

        try
        {
            IsSpeaking = true;
            _cts = new CancellationTokenSource();

            var options = new SpeechOptions
            {
                Volume = (float)SpeechVolume,
                Pitch = (float)SpeechPitch,
                Locale = null
            };

            await TextToSpeech.Default.SpeakAsync(TextToSpeak, options, _cts.Token);
        }
        catch (OperationCanceledException)
        {
            // user cancelled
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Speech failed: {ex.Message}", "OK");
        }
        finally
        {
            IsSpeaking = false;
            _cts?.Dispose();
            _cts = null;
        }
    }

    [RelayCommand]
    private void Cancel()
    {
        _cts?.Cancel();
    }
}