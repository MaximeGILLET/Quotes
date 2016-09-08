using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quotes.Models
{
    public class CalendarModel
    {
        public int success { get; set; }
        public List<CalendarItem> result { get; set; }
    }

    public class CalendarItem
    {
        public int id { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public long start { get; set; }
        public long end { get; set; }
    }
}