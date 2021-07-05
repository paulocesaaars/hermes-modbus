using System.ComponentModel.DataAnnotations;

namespace Deviot.Hermes.ModbusTcp.Api.ViewModels
{
    public class UserPasswordViewModel
    {
        [Required]
        [MinLength(5)]
        [MaxLength(10)]
        public string Password { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(10)]
        public string NewPassword { get; set; }
    }
}
