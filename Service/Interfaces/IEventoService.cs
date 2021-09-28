using Domains;
using Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IEventoService
    {
        Task<Response> Cadastrar(Evento e);
        Task<DataResponse<Evento>> LerEventos();
        Task<SingleResponse<Evento>> GetByID(int id);
        Task<Response> Update(Evento e);
    }
}
