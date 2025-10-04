using RSVP_APP.Models;
using RSVP_APP.Services;
using RSVP_APP.Helpers;

namespace RSVPApp;

public partial class AddEventPage : ContentPage
{
    private readonly DatabaseService _db;

    public AddEventPage()
    {
        InitializeComponent();
        _db = (Application.Current as App).Services.GetService<DatabaseService>();
        HostEntry.Text = LoginState.CurrentUserName;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameEntry.Text) ||
            string.IsNullOrWhiteSpace(AddressEntry.Text) ||
            string.IsNullOrWhiteSpace(MaxEntry.Text) ||
            !int.TryParse(MaxEntry.Text, out int max))
        {
            await DisplayAlert("Error", "All fields are required", "OK");
            return;
        }

        var ev = new EventModel
        {
            HostUserId = LoginState.CurrentUserId,
            EventName = NameEntry.Text.Trim(),
            Address = AddressEntry.Text.Trim(),
            MaxAttendees = max,
            EventDate = DateEntry.Date,
            RsvpDeadline = DeadlineEntry.Date
        };

        await _db.AddEventAsync(ev);
        await DisplayAlert("Success", "Event created!", "OK");
        await Shell.Current.GoToAsync("//DashboardPage");
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//DashboardPage");
    }
}
