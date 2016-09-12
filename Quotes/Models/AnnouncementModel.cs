using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quotes.Models
{
    public class AnnouncementModel
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Author { get; set; }
        public string RawHtml { get; set; }
        public string Title { get; set; }
    }
}