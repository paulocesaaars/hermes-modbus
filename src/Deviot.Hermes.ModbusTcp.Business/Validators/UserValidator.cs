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
            RuleFor(x => x.FullName).MinimumLength(3).WithMessage("O nome completo precisa ter no mínimo 3 caracteres.")
                                .MaximumLength(150).WithMessage("O nome completo precisa ter no máximo 150 caracteres.");

            RuleFor(x => x.UserName).MinimumLength(3).WithMessage("O nome de usuário precisa ter no mínimo 3 caracteres.")
                                    .MaximumLength(20).WithMessage("O nome de usuário precisa ter no máximo 20 caracteres.")
                                    .Custom((userName, context) => {
                                            if(!Utils.ValidateAlphanumericWithUnderline(userName))
                                                context.AddFailure("O nome de usuário precisar ter somente valores alfanuméricos ou underline.");
                                    });

            RuleFor(x => x.Password).MinimumLength(3).WithMessage("A senha precisa ter no mínimo 3 caracteres.")
                                    .MaximumLength(10).WithMessage("A senha precisa ter no máximo 10 caracteres.");
        }
    }
}
