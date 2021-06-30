using Deviot.Common;
using Deviot.Hermes.ModbusTcp.Business.Base;
using Deviot.Hermes.ModbusTcp.Business.Entities;
using FluentValidation;

namespace Deviot.Hermes.ModbusTcp.Business.Validators
{
    public class UserValidator : ValidatorBase<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.FullName).MinimumLength(5).WithMessage("O nome completo precisa ter no mínimo 5 caracteres.")
                                .MaximumLength(150).WithMessage("O nome completo precisa ter no máximo 150 caracteres.");

            RuleFor(x => x.UserName).MinimumLength(5).WithMessage("O nome de usuário precisa ter no mínimo 5 caracteres.")
                                    .MaximumLength(20).WithMessage("O nome de usuário precisa ter no máximo 20 caracteres.")
                                    .Custom((userName, context) => {
                                            if(!Utils.ValidateAlphanumericWithUnderline(userName))
                                                context.AddFailure("O nome de usuário precisar ter somente valores alfanuméricos ou underline.");
                                    });

            RuleFor(x => x.Password).MinimumLength(5).WithMessage("A senha precisa ter no mínimo 5 caracteres.")
                                    .MaximumLength(10).WithMessage("A senha precisa ter no máximo 10 caracteres.")
                                    .Custom((password, context) => {
                                        if (!Utils.ValidateAlphanumeric(password))
                                            context.AddFailure("A senha precisar ter somente valores alfanuméricos.");
                                    });
        }
    }
}
