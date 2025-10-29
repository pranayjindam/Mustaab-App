namespace MustaabApp;
using Microsoft.Maui.Controls;
using System;

public partial class MainPage : ContentPage
{
    private bool _canExit = false;
    private string _currentUrl = "";

    public MainPage()
    {
        InitializeComponent();
    }

    private void WebView_Navigated(object sender, WebNavigatedEventArgs e)
    {
        _currentUrl = e.Url;
    }

    private void WebView_Navigating(object sender, WebNavigatingEventArgs e)
    {
        _canExit = false;
    }

    protected override bool OnBackButtonPressed()
    {
        try
        {
            // Inject JavaScript to check React Router history
            MainWebView.EvaluateJavaScriptAsync(@"
                if (window.history.length > 1) {
                    window.history.back();
                    true;
                } else {
                    false;
                }
            ").ContinueWith(async t =>
            {
                var result = await t;
                if (result == "false" || string.IsNullOrEmpty(result))
                {
                    // No more history inside React app
                    // You can either exit app or show toast
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        if (_canExit)
                        {
                            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                        }
                        else
                        {
                            _canExit = true;
                            await Application.Current.MainPage.DisplayAlert("Exit", "Press back again to exit", "OK");
                            await Task.Delay(2000);
                            _canExit = false;
                        }
                    });
                }
            });
            return true;
        }
        catch
        {
            return base.OnBackButtonPressed();
        }
    }
}
