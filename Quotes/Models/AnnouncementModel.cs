using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quotes.Models
{
    public class AnnouncementModel
    {
        public int? Id { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string Author { get; set; }
        public string RawHtml { get; set; }
        public string Title { get; set; }
        //Status of the announcement (1 : backlog , 2 : published , 3 : archived)
        public string Status { get; set; }
        public IEnumerable<string> StatusList = new []{ "Backlog", "Published", "Archived" };
    }
}