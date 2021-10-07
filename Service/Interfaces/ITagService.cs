using Domains;
using Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ITagService
    {
        Task<Response> Cadastrar(Tags t);
        Task<DataResponse<Tags>> GetAll();

    }
}
