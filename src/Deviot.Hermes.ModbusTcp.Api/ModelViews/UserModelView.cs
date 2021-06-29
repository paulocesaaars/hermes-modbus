﻿using System.ComponentModel.DataAnnotations;

namespace Deviot.Hermes.ModbusTcp.Api.ModelViews
{
    public class UserModelView
    {
        [Required]
        [MinLength(3)]
        [MaxLength(150)]
        public string FullName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string UserName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(10)]
        public string Password { get; set; }

        public bool Enabled { get; set; } = false;

        public bool Administrator { get; set; } = false;
        
    }
}
