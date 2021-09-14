using Domains;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validations
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(c => c.Nome).NotNull().MinimumLength(3).MaximumLength(70);
            RuleFor(c => c.Email).EmailAddress().NotNull().MaximumLength(100).WithMessage("Email inválido.");
            RuleFor(c => c.Senha).NotEmpty().NotNull().MaximumLength(20);
            RuleFor(c => c.Telefone).NotNull().MinimumLength(8).MaximumLength(15);
        }
    }
}
