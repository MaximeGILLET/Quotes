using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Quotes.Models
{
    public class CalendarModel
    {
        public int success { get; set; }
        public List<CalendarItem> result { get; set; }
    }

    public class CalendarItem
    {
        [JsonProperty(Order = 1)]
        public string id { get; set; }
        [JsonProperty(Order = 2)]
        public string title { get; set; }
        [JsonProperty(Order = 3)]
        public string url { get; set; }
        [JsonProperty(PropertyName = "class", Order = 4)]
        public string type { get; set; }
        [JsonProperty(Order = 5)]
        public string start { get; set; }
        [JsonProperty(Order = 6)]
        public string end { get; set; }

    }
}