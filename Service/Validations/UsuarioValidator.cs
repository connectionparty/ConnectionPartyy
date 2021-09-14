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
            RuleFor(c => c.Nome).NotEmpty().NotNull().MinimumLength(3).MaximumLength(70);
            RuleFor(c => c.Email).NotEmpty().EmailAddress();
            RuleFor(c => c.Senha).NotEmpty().MaximumLength(20);
        }
    }
}
