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

namespace Service
{
    public class EventoService : IEventoService
    {
        public async Task<Response> Cadastrar(Evento e)
        {
            EventoValidator validation = new EventoValidator();
            ValidationResult result = validation.Validate(e);

            Response r = result.ToResponse();
            if (!r.Success)
            {
                return r;
            }

            try
            {
                using (ConnectionPartyDBContext db = new ConnectionPartyDBContext())
                {
                    db.Eventos.Add(e);
                    await db.SaveChangesAsync();
                    return new Response()
                    {
                        Success = true,
                        Mensagem = "Evento cadastrado com sucesso."
                    };
                }
            }
            catch (Exception)
            {
                return ResponseFactory.ResponseDBError();
            }
        }

        public Task<SingleResponse<Usuario>> GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse<Evento>> LerEventos()
        {
            throw new NotImplementedException();
        }

        public Task<Response> Update(Evento e)
        {
            throw new NotImplementedException();
        }
    }
}
