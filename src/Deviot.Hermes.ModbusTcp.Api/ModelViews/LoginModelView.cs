using System.ComponentModel.DataAnnotations;

namespace Deviot.Hermes.ModbusTcp.Api.ModelViews
{
    public class LoginModelView
    {
        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string UserName { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(10)]
        public string Password { get; set; }
    }
}
