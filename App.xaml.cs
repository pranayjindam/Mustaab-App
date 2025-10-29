namespace MustaabApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // Set MainPage directly to your WebView page
        MainPage = new MainPage();
    }
}
