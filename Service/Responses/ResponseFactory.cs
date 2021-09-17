using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Responses
{
    public static class ResponseFactory
    {
        public static Response ResponseDBError()
        {
            Response response = new Response();
            response.Success = false;
            response.Mensagem = "Erro no banco de dados, contate o administrador.";
            return response;
        }

        
    }
}
