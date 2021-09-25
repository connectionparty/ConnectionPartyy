using Domains;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validations
{
    public class EventoValidator : AbstractValidator<Evento>
    {
        public EventoValidator()
        {
            RuleFor(c => c.Nome).NotNull().MinimumLength(3).MaximumLength(70).WithMessage("Nome do evento é obrigatório e deve conter entre 3 e 70 caracteres.");
            RuleFor(c => c.Descricao).NotNull().MinimumLength(10).MaximumLength(300).WithMessage("Descrição do evento é obrigatório e deve conter entre 10 e 300 caracteres");
            RuleFor(c => c.IdadeMinima).NotNull().WithMessage("Idade mínima deve ser informada.");
            RuleFor(c => c.Bairro).NotNull().MinimumLength(3).MaximumLength(40).WithMessage("Nome do bairro deve ser informado e deve conter entre 3 e 40 caracteres.");
            RuleFor(c => c.Rua).NotNull().MinimumLength(3).MaximumLength(60).WithMessage("Nome da rua deve ser informado e deve conter entre 3 e 60 caracteres.");
            RuleFor(c => c.Numero).NotEmpty().MaximumLength(5).WithMessage("Número deve ser informado e deve conter no máximo 5 casas.");
            RuleFor(c => c.Complemento).MinimumLength(3).MaximumLength(60).WithMessage("Complemento deve conter entre 3 e 60 caracteres.");
            RuleFor(c => c.PrecisaDocumento).NotNull();
        }
    }
}
