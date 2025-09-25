using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSVP_APP.Helpers
{
    public static class LoginState
    {
        public static bool IsGuest { get; set; } = true;
        public static int CurrentUserId { get; set; } = -1;
        public static string CurrentUserName { get; set; } = "Guest";
    }
}
