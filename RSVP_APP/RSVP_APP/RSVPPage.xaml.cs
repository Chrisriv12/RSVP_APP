using RSVPApp.Models;
using RSVPApp.Services;
using RSVPApp.Helpers;

namespace RSVPApp;

public partial class RSVPPage : ContentPage
{
    private readonly DatabaseService _db;
    public int EventId { get; set; }

    public RSVPPage()
    {
        InitializeComponent();
        _db = (Application.Current as App).Services.GetService<DatabaseService>();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (!LoginState.IsGuest)
        {
            var user = await _db.GetUserByIdAsync(LoginState.CurrentUserId);
            if (user != null)
            {
                NameEntry.Text = user.Name;
                EmailEntry.Text = user.Email;
            }
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var ev = await _db.GetEventByIdAsync(EventId);
        if (ev == null) return;

        // deadline check
        if (DateTime.Now > ev.RsvpDeadline)
        {
            await DisplayAlert("Error", "RSVP deadline passed", "OK");
            return;
        }

        // capacity check
        var count = await _db.GetRsvpCountForEventAsync(ev.Id);
        if (count >= ev.MaxAttendees)
        {
            await DisplayAlert("Error", "Event is full", "OK");
            return;
        }

        // duplicate check
        if (!LoginState.IsGuest)
        {
            var existing = await _db.GetUserRsvpForEventAsync(ev.Id, LoginState.CurrentUserId);
            if (existing != null)
            {
                await DisplayAlert("Error", "Already RSVP’d", "OK");
                return;
            }
        }

        // save RSVP
        var rsvp = new Rsvp
        {
            EventId = ev.Id,
            UserId = LoginState.IsGuest ? 0 : LoginState.CurrentUserId,
            Name = NameEntry.Text?.Trim(),
            Email = EmailEntry.Text?.Trim()
        };

        await _db.AddRsvpAsync(rsvp);
        await DisplayAlert("Success", "RSVP saved!", "OK");
        await Shell.Current.GoToAsync($"//EventDetailsPage?EventId={ev.Id}");
    }

    private async void OnCancelClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync($"//EventDetailsPage?EventId={EventId}");
}
