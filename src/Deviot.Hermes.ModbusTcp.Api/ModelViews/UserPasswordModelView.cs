using System.ComponentModel.DataAnnotations;

namespace Deviot.Hermes.ModbusTcp.Api.ModelViews
{
    public class UserPasswordModelView
    {
        [Required]
        [MinLength(3)]
        [MaxLength(10)]
        public string Password { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(10)]
        public string NewPassword { get; set; }
    }
}
