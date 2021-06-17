using FluentValidation;

namespace Deviot.Hermes.ModbusTcp.Business.Base
{
    public abstract class ValidatorBase<T> : AbstractValidator<T> where T : EntityBase
    {
        protected ValidatorBase()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("O id é obrigatório.");
        }
    }
}
