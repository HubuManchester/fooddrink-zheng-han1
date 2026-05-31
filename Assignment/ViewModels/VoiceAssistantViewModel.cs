using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Assignment.ViewModels;

public partial class VoiceAssistantViewModel : ObservableObject
{
    private CancellationTokenSource _cts;

    [ObservableProperty]
    private string textToSpeak = "欢迎使用语音助手，请输入您希望朗读的文本。";

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
            await Shell.Current.DisplayAlert("提示", "请输入要朗读的内容", "确定");
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
                Locale = null   // 使用系统默认语言
            };

            await TextToSpeech.Default.SpeakAsync(TextToSpeak, options, _cts.Token);
        }
        catch (OperationCanceledException)
        {
            // 用户主动取消，静默处理
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("错误", $"语音朗读失败: {ex.Message}", "确定");
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