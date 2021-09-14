using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Responses
{
    public class SingleResponse<T> : Response
    {
        public T Item { get; set; }
    }
}
