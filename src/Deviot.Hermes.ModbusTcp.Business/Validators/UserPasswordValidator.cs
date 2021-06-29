using Deviot.Hermes.ModbusTcp.Business.Base;
using Deviot.Hermes.ModbusTcp.Business.Entities;
using FluentValidation;

namespace Deviot.Hermes.ModbusTcp.Business.Validators
{
    public class UserPasswordValidator : ValidatorBase<UserPassword>
    {
        public UserPasswordValidator()
        {
            RuleFor(x => x.Password).MinimumLength(3).WithMessage("A senha precisa ter no mínimo 3 caracteres.")
                                    .MaximumLength(10).WithMessage("A senha precisa ter no máximo 10 caracteres.");

            RuleFor(x => x.NewPassword).MinimumLength(3).WithMessage("A nova senha precisa ter no mínimo 3 caracteres.")
                                       .MaximumLength(10).WithMessage("A nova senha precisa ter no máximo 10 caracteres.");
        }
    }
}
