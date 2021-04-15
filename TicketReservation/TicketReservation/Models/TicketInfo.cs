using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TicketReservation.Models
{
    public class TicketInfo
    {
        [PrimaryKey, AutoIncrement]
        public int TicketID { get; set; }

        public string DepCity { get; set; }

        public string DestCity { get; set; }

        public TimeSpan FlightTime { get; set; }

        public DateTime FlightDate { get; set; }

        public int UserID { get; set; }
    }
}
