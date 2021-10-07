using DataAcessObject;
using Domains;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Service.Extensions;
using Service.Interfaces;
using Service.Responses;
using Service.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class TagService : ITagService
    {
        public async Task<Response> Cadastrar(Tags t)
        {
            TagValidator validations = new TagValidator();
            ValidationResult result = validations.Validate(t);

            Response r = result.ToResponse();
            if (!r.Success)
            {
                return r;
            }

            try
            {
                using (ConnectionPartyDBContext db = new ConnectionPartyDBContext())
                {
                    Tags tagCadastrada = await db.Tags.FirstOrDefaultAsync(c => c.Nome == t.Nome);
                    if (tagCadastrada != null)
                    {
                        return new Response()
                        {
                            Success = false,
                            Mensagem = "Essa tag já foi cadastrada."
                        };
                    }
                    db.Tags.Add(t);
                    await db.SaveChangesAsync();
                    return new Response()
                    {
                        Success = true,
                        Mensagem = "Tag registrada com sucesso."
                    };
                }
            }
            catch (Exception ex)
            {
                return ResponseFactory.ResponseDBError();
            }
        }

        public async Task<DataResponse<Tags>> GetAll()
        {
            using (ConnectionPartyDBContext db = new ConnectionPartyDBContext())
            {
                List<Tags> tags = await db.Tags.ToListAsync();
                DataResponse<Tags> response = new DataResponse<Tags>()
                {
                    Data = tags,
                    Mensagem = "Dados retornados com sucesso.",
                    Success = true
                };
                return response;
            }
        }
    }
}
