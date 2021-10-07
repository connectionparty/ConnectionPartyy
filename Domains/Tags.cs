using System;
using System.Collections.Generic;

namespace Domains
{
    public class Tags
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
        public ICollection<Evento> Eventos { get; set; }
    }
}
