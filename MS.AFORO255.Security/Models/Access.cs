using System.ComponentModel.DataAnnotations;

namespace MS.AFORO255.Security.Models
{
    public class Access
    {
        [Key]
        public int UserId { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
