using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Quotes.Models
{
    public class MailModel
    {
        [Key]
        public int MailId { get; set; }
        public int SenderId { get; set; }
        public List<int> RecipientListId { get; set; }
        public string Object { get; set; }
        public string Label { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ReceptionDate { get; set; }
        public bool IsDeleted { get; set; }
        public int ParentId { get; set; }
    }

    public class MailViewModel :MailModel
    {
        public string senderName { get; set; }

    }
}