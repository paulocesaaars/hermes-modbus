using System;
using System.ComponentModel.DataAnnotations;

namespace Deviot.Hermes.ModbusTcp.Api.ModelViews
{
    public class UserInfoModelView
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(150)]
        public string FullName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string UserName { get; set; }

        public bool Enabled { get; set; } = false;

        public bool Administrator { get; set; } = false;
    }
}
