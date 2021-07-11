using Deviot.Hermes.ModbusTcp.Api.ViewModels.Bases;
using System.ComponentModel.DataAnnotations;

namespace Deviot.Hermes.ModbusTcp.Api.ViewModels
{
    public class UserInfoViewModel : EntityBaseModelView
    {
        [Required]
        [MinLength(5)]
        [MaxLength(150)]
        public string FullName { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string UserName { get; set; }

        public bool Enabled { get; set; } = false;

        public bool Administrator { get; set; } = false;
    }
}
