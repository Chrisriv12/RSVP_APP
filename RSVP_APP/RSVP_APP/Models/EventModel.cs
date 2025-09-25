using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSVP_APP.Models
{
    public class EventModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        public int HostUserId { get; set; }

        public string EventName { get; set; }

        public string Address { get; set; }

        public int MaxAttendees { get; set; }

        public DateTime EventDate { get; set; }

        public DateTime RsvpDeadline { get; set; }
    }
}
