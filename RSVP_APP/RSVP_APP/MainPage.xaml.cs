using Microsoft.Maui.Controls;
using RSVP_APP.Helpers;

namespace RSVP_APP;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("LoginPage");
    }

    private async void OnGuestClicked(object sender, EventArgs e)
    {
        LoginState.IsGuest = true;
        LoginState.CurrentUserId = 0;
        LoginState.CurrentUserName = "Guest";

        await Shell.Current.GoToAsync("//DashboardPage");
    }
}
