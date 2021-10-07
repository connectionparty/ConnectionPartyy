using Domains;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validations
{
    public class TagValidator : AbstractValidator<Tags>
    {
        public TagValidator()
        {
            RuleFor(c => c.Nome).NotEmpty().MaximumLength(20).WithMessage("Tag deve conter no máximo 20 caracteres.");
        }
    }
}
