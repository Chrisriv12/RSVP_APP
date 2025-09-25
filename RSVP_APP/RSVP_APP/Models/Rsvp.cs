using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSVP_APP.Models
{
    public class Rsvp
    {
        public int Id { get; set; }

        public int EventId { get; set; }

        public string EventName { get; set; }

        public string EventEmail { get; set; }

        public DateTime Timestamp { get; set; }

    }
}
