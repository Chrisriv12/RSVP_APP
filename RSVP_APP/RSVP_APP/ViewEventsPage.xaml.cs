using RSVPApp.Services;
using RSVPApp.Helpers;

namespace RSVPApp;

public partial class ViewEventsPage : ContentPage
{
    private readonly DatabaseService _db;

    public ViewEventsPage()
    {
        InitializeComponent();
        _db = (Application.Current as App).Services.GetService<DatabaseService>();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        EventsList.ItemsSource = await _db.GetAllEventsAsync();
    }

    private async void OnAllClicked(object sender, EventArgs e) =>
        EventsList.ItemsSource = await _db.GetAllEventsAsync();

    private async void OnAttendingClicked(object sender, EventArgs e)
    {
        if (LoginState.IsGuest) return;
        EventsList.ItemsSource = await _db.GetEventsUserAttendingAsync(LoginState.CurrentUserId);
    }

    private async void OnHostingClicked(object sender, EventArgs e)
    {
        if (LoginState.IsGuest) return;
        EventsList.ItemsSource = await _db.GetEventsByHostAsync(LoginState.CurrentUserId);
    }

    private async void OnEventSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is EventModel ev)
            await Shell.Current.GoToAsync($"//EventDetailsPage?EventId={ev.Id}");
    }

    private async void OnBackClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync("//DashboardPage");
}
