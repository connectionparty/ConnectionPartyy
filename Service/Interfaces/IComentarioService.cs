using Domains;
using Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IComentarioService
    {
        Task<Response> Comentar(Comentario comentario);
        Task<Response> Editar(Comentario comentario);
        Task<Response> Deletar(Comentario comentario);
        DataResponse<Comentario> ListarComentarios(Comentario comentario);
    }
}
