using System.ComponentModel.DataAnnotations;

namespace WebApplication123.Models
{
    public class UpdateAccountModel
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "PinMust not be more than 4 digits")]
        public string Pin { get; set; }
        //[Required]
        //[Compare("Pin", ErrorMessage = "Pins do not match")]
        //public string ConfirmPin { get; set; }
        public DateTime DateLastUpdated { get; set; }
    }
}
