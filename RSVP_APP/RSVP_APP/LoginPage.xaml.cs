using RSVPApp.Services;

namespace RSVP_APP
{
public partial class LoginPage : ContentPage
{
    DatabaseService _db;

    public LoginPage()
    {
        InitializeComponent();
        _db = (Application.Current as App).Services.GetService<DatabaseService>();
        // or use: MauiApplication.Current.Services.GetService<DatabaseService>()
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text?.Trim();
        string pw = PasswordEntry.Text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pw))
        {
            await DisplayAlert("Error", "Enter email & password", "OK");
            return;
        }

        var user = await _db.GetUserByEmailAsync(email);
        if (user != null && user.Password == pw)
        {
            LoginState.IsGuest = false;
            LoginState.CurrentUserId = user.Id;
            LoginState.CurrentUserName = user.Name;
            await Shell.Current.GoToAsync("//DashboardPage");
        }
        else
        {
            await DisplayAlert("Error", "Invalid credentials", "OK");
        }
    }

    private async void OnGuestClicked(object sender, EventArgs e)
    {
        LoginState.IsGuest = true;
        LoginState.CurrentUserName = "Guest";
        await Shell.Current.GoToAsync("//DashboardPage");
    }

    private async void OnCreateClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//CreateAccountPage");
    }
}
}

