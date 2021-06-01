using System.ComponentModel.DataAnnotations;

namespace MS.AFORO255.Notification.Models
{
    public class SendMail
    {
        [Key]
        public int SendMailId { get; set; }
        public string SendDate { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string Address { get; set; }
        public int AccountId { get; set; }
    }
}
