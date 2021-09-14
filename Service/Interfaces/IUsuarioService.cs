using Domains;
using Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IUsuarioService
    {
        Task<Response> Cadastrar(Usuario u);
        Task<DataResponse<Usuario>> LerGeneros();
        Task<SingleResponse<Usuario>> GetByID(int id);
        Task<Response> Update(Usuario u);
    }
}
