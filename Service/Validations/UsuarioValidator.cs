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
            RuleFor(c => c.Nome).NotNull().MinimumLength(3).MaximumLength(70).WithMessage("Nome deve conter entre 3 e 70 caracteres.");
            RuleFor(c => c.Email).EmailAddress().NotNull().MaximumLength(100).WithMessage("Email inválido.");
            RuleFor(c => c.Senha).NotEmpty().MaximumLength(20).WithMessage("Senha é um campo obrigatótio e deve conter no máximo 20 caracteres.");
            RuleFor(c => c.Telefone).NotNull().MinimumLength(8).MaximumLength(15).WithMessage("Telefone deve conter entre 8 e 15 números.");
            RuleFor(c => c.Bairro).NotNull().MinimumLength(3).MaximumLength(40).WithMessage("Nome do bairro deve ser informado e deve conter entre 3 e 40 caracteres.");
            RuleFor(c => c.Rua).NotNull().MinimumLength(3).MaximumLength(60).WithMessage("Nome da rua deve ser informado e deve conter entre 3 e 60 caracteres.");
            RuleFor(c => c.Numero).NotEmpty().MaximumLength(5).WithMessage("Número deve ser informado e deve conter no máximo 5 casas.");
            RuleFor(c => c.Complemento).MinimumLength(3).MaximumLength(60).WithMessage("Complemento deve conter entre 3 e 60 caracteres.");
        }
    }
}
