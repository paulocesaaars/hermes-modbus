using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Deviot.Hermes.ModbusTcp.Api.ViewModels.Bases
{
    [ExcludeFromCodeCoverage]

    public class EntityBaseModelView
    {
        [Required]
        public Guid Id { get; set; }
    }
}
