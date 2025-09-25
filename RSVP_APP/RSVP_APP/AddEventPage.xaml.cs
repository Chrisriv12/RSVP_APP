using RSVPApp.Helpers;
using RSVPApp.Models;
using RSVPApp.Services;
using System.Formats.Tar;
using System.Net;

namespace RSVPApp.Views
{
    public partial class AddEventPage : ContentPage
    {
        DatabaseService _db;

        public AddEventPage()
        {
            InitializeComponent();
            _db = (Application.Current as App).Services.GetService<DatabaseService>();
            HostEntry.Text = LoginState.IsGuest ? "Guest" : LoginState.CurrentUserName;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameEntry.Text) ||
                string.IsNullOrWhiteSpace(AddressEntry.Text) ||
                string.IsNullOrWhiteSpace(MaxEntry.Text) ||
                !int.TryParse(MaxEntry.Text, out int max))
            {
                await DisplayAlert("Error", "All fields must be filled and Max must be number", "OK");
                return;
            }

            var ev = new EventModel
            {
                HostUserId = LoginState.IsGuest ? 0 : LoginState.CurrentUserId,
                Name = NameEntry.Text.Trim(),
                Address = AddressEntry.Text.Trim(),
                MaxAttendees = max,
                EventDate = DateEntry.Date,
                RsvpDeadline = DeadlineEntry.Date
            };

            await _db.AddEventAsync(ev);
            await DisplayAlert("Success", "Event saved", "OK");
            await Shell.Current.GoToAsync("//DashboardPage");
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//DashboardPage");
        }
    }
}

