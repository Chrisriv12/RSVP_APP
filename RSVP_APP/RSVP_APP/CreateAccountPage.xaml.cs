using System.Threading.Tasks;

namespace RSVP_APP;

public partial class CreateAccountPage : ContentPage
{
	public CreateAccountPage()
	{
		InitializeComponent();
	}

	private async Task OnCreateClicked(object sender, EventArgs e)
	{
		if (string.IsNullOrWhiteSpace(NameEntry.Text) ||
			string.IsNullOrWhiteSpace(EmailEntry.Text) ||
			string.IsNullOrWhiteSpace(PasswordEntry.Text) ||
			string.IsNullOrWhiteSpace(PhoneEntry.Text))

		{
			await DisplayAlert("Error", "All fields must be filled", "OK");
			return;
		}

		await DisplayAlert("Success", "Account Created!", "OK");
		await Shell.Current.GoToAsync("//LoginPage");
	}

	private async Task OnCancelClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("//LoginPage");
    }

    }