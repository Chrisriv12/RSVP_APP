using Microsoft.Maui.Controls;

namespace RSVP_APP
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("DashboardPage", typeof(DashboardPage));
            Routing.RegisterRoute("AddEventPage", typeof(AddEventPage));
            Routing.RegisterRoute("ViewEventsPage", typeof(ViewEventsPage));
            Routing.RegisterRoute("LoginPage", typeof(LoginPage));

        }
    }
}
