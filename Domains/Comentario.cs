using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class Comentario
    {
        public int ID { get; set; }
        public int UsuarioID { get; set; }
        public Usuario Usuario { get; set; }
        public string Texto { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public int? ComentarioID { get; set; }
        public ICollection<Comentario> Resposta { get; set; }
        public int EventoID { get; set; }
        public Evento Evento { get; set; }
    }
}
