using System;
using System.Collections.Generic;

namespace Domains
{
    public class Tags
    {
        public int ID { get; set; }
        public string Nome{ get; set; }
        public ICollection<Domains.Evento> Eventos { get; set; }
    }
}
