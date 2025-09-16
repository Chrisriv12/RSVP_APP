using System.Threading.Tasks;

namespace RSVP_APP;

public partial class LoginPage : ContentPage
{
	public static bool IsGuest = false;
	public static string CurrentUser = "";
	public LoginPage()
	{
		InitializeComponent();
	}

	private async Task OnLoginClicked(object sender, EventArgs e)
	{
		if (EmailEntry.Text == "user@example.com" && PasswordEntry.Text == "password123")
		{
			IsGuest = false;
			CurrentUser = "John Doe";
			await Shell.Current.GoToAsync("//DashboardPage");
		}
		else
		{
			await DisplayAlert("Login Failed", "Invalid email or password.", "OK");
		}
	}

	private async Task OnGuestClicked(object sender, EventArgs e)
	{
		IsGuest = true;
		CurrentUser = "Guest";
		await Shell.Current.GoToAsync("//DashboardPage");
	}

	private async Task OnCreateClicked(object sender, EventArgs e)
	{
		// Navigate to the account creation page (not implemented in this example)
		await Shell.Current.GoToAsync("//CreateAccountPage");
	}
}