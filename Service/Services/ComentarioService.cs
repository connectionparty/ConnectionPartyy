using DataAcessObject;
using Domains;
using FluentValidation.Results;
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
    public class ComentarioService : IComentarioService
    {
        public async Task<Response> Comentar(Comentario comentario)
        {
            ComentarioValidator validation = new ComentarioValidator();
            ValidationResult result = validation.Validate(c);

            Response r = result.ToResponse();
            if (!r.Success)
            {
                return r;
            }

            try
            {
                using (ConnectionPartyDBContext db = new ConnectionPartyDBContext())
                {

                    db.Comentarios.Add(comentario);
                    await db.SaveChangesAsync();
                    return new Response()
                    {
                        Success = true,
                        Mensagem = "Comentário registrado com sucesso."
                    };
                }
            }

            catch (Exception ex)
            {
                return ResponseFactory.ResponseDBError();
            }
        }

        public async Task<Response> Deletar(Comentario comentario)
        {
            Response response = new Response();

            using (ConnectionPartyDBContext db = new ConnectionPartyDBContext())
            {
                db.Comentarios.Remove(comentario);
                await db.SaveChangesAsync();
                response.Success = true;
                response.Mensagem = "Comentário excluído com sucesso.";
            }

            return response;
        }

        public Task<Response> Editar(Comentario comentario)
        {
            throw new NotImplementedException();
        }

        public DataResponse<Comentario> ListarComentarios(Comentario comentario)
        {
            throw new NotImplementedException();
        }
    }
}
