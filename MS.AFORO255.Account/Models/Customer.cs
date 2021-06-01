using System.ComponentModel.DataAnnotations;

namespace MS.AFORO255.Account.Models
{
    public class Customer
    {
        [Key]
        public int IdCustomer { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

    }
}
