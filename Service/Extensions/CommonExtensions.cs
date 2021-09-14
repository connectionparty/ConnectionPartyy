using FluentValidation.Results;
using Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Extensions
{
    static class CommonExtensions
    {
        public static Response ToResponse(this ValidationResult result)
        {
            if (result.IsValid)
            {
                return new Response()
                {
                    Success = true,
                    Mensagem = "Validação bem sucedida."
                };
            }

            StringBuilder sb = new StringBuilder();
            foreach (ValidationFailure item in result.Errors)
            {
                sb.AppendLine(item.ErrorMessage);
            }

            return new Response()
            {
                Success = false,
                Mensagem = sb.ToString()
            };


        }
    }
}
