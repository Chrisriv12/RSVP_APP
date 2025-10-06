using RSVP_APP.Models;
using RSVP_APP.Services;

namespace RSVP_APP;

public partial class CreateAccountPage : ContentPage
{
    private readonly DatabaseService _db;

    public CreateAccountPage()
    {
        InitializeComponent();
        _db = (Application.Current as App).Services.GetService<DatabaseService>();
    }

    private async void OnCreateClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameEntry.Text) ||
            string.IsNullOrWhiteSpace(EmailEntry.Text) ||
            string.IsNullOrWhiteSpace(PasswordEntry.Text) ||
            string.IsNullOrWhiteSpace(PhoneEntry.Text))
        {
            await DisplayAlert("Error", "All fields are required", "OK");
            return;
        }

        var existing = await _db.GetUserByEmailAsync(EmailEntry.Text.Trim());
        if (existing != null)
        {
            await DisplayAlert("Error", "Email already registered", "OK");
            return;
        }

        var user = new User
        {
            Name = NameEntry.Text.Trim(),
            Email = EmailEntry.Text.Trim(),
            Password = PasswordEntry.Text,
            Phone = PhoneEntry.Text.Trim()
        };

        await _db.AddUserAsync(user);
        await DisplayAlert("Success", "Account created!", "OK");
        await Shell.Current.GoToAsync("//LoginPage");
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//LoginPage");
    }
}
