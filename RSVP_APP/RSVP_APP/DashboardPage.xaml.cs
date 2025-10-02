using RSVP_APP.Helpers;
using RSVPApp.Helpers;

namespace RSVPApp;

public partial class DashboardPage : ContentPage
{
    public DashboardPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        WelcomeLabel.Text = $"Welcome, {LoginState.CurrentUserName}";
    }

    private async void OnAddEventClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync("//AddEventPage");

    private async void OnViewEventsClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync("//ViewEventsPage");

    private async void OnLogoutClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync("//LoginPage");
}
