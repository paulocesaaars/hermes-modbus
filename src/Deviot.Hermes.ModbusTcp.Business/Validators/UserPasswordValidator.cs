using Deviot.Common;
using Deviot.Hermes.ModbusTcp.Business.Base;
using Deviot.Hermes.ModbusTcp.Business.Entities;
using FluentValidation;

namespace Deviot.Hermes.ModbusTcp.Business.Validators
{
    public class UserPasswordValidator : ValidatorBase<UserPassword>
    {
        public UserPasswordValidator()
        {
            RuleFor(x => x.Password).MinimumLength(5).WithMessage("A senha precisa ter no mínimo 5 caracteres")
                                    .MaximumLength(10).WithMessage("A senha precisa ter no máximo 10 caracteres")
                                    .Custom((password, context) => {
                                        if (!Utils.ValidateAlphanumeric(password))
                                            context.AddFailure("A senha precisar ter somente valores alfanuméricos");
                                    });

            RuleFor(x => x.NewPassword).MinimumLength(5).WithMessage("A nova senha precisa ter no mínimo 5 caracteres")
                                       .MaximumLength(10).WithMessage("A nova senha precisa ter no máximo 10 caracteres")
                                       .Custom((password, context) => {
                                           if (!Utils.ValidateAlphanumeric(password))
                                               context.AddFailure("A senha precisar ter somente valores alfanuméricos");
                                       });
        }
    }
}
