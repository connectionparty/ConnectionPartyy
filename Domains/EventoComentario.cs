using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class EventoComentario
    {
        public Evento Evento { get; set; }
        public List<Comentario> Comentarios { get; set; }
    }
}
