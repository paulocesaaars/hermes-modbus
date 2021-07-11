using Deviot.Hermes.ModbusTcp.Api.ViewModels.Bases;
using System.ComponentModel.DataAnnotations;

namespace Deviot.Hermes.ModbusTcp.Api.ViewModels
{
    public class UserPasswordViewModel : EntityBaseModelView
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
