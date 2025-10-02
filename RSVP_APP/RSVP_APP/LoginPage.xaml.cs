using Microsoft.Extensions.DependencyInjection;
using RSVP_APP.Helpers;
using RSVPApp.Helpers;
using RSVPApp.Services;
using static System.Net.Mime.MediaTypeNames;

namespace RSVPApp;

public partial class LoginPage : ContentPage
{
    private readonly DatabaseService _db;

    public LoginPage()
    {
        InitializeComponent();
        _db = (Application.Current as App).Services.GetService<DatabaseService>();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var email = EmailEntry.Text?.Trim();
        var pw = PasswordEntry.Text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(pw))
        {
            await DisplayAlert("Error", "Please enter email and password", "OK");
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
        LoginState.CurrentUserId = 0;
        LoginState.CurrentUserName = "Guest";

        await Shell.Current.GoToAsync("//DashboardPage");
    }

    private async void OnCreateClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//CreateAccountPage");
    }
}
