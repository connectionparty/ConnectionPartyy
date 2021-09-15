using Domains;
using Service.Interfaces;
using Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    class EventoService : IEventoService
    {
        public Task<Response> Cadastrar(Evento e)
        {
            throw new NotImplementedException();
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
