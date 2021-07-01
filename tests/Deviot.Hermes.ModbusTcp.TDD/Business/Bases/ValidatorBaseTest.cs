using Deviot.Hermes.ModbusTcp.Business.Base;
using FluentAssertions;
using FluentValidation.TestHelper;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Business.Bases
{
    [ExcludeFromCodeCoverage]
    public class ValidatorBaseTest
    {
        [Fact(DisplayName = "Campo obrigatório propriedade Id")]
        public void Id_NotEmpty()
        {
            var entity = new EntityBase(Guid.Empty);
            var validator = new EntityBaseValidator();

            var resultado = validator.TestValidate(entity);

            resultado.ShouldHaveValidationErrorFor(c => c.Id).Should().NotBeEmpty();
        }
    }

    internal class EntityBaseValidator : ValidatorBase<EntityBase>
    {

    }
}
