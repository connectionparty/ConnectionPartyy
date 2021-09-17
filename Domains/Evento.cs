using System;
using System.Collections.Generic;

namespace Domains
{
    public class Evento
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim{ get; set; }
        public double? Valor { get; set; }
        public int IdadeMinima { get; set; }
        public bool PrecisaApresentaDocumento { get; set; }
        public string Endereco { get; set; }
        public Usuario Organizador { get; set; }
        public int UsuarioID { get; set; }
        public bool EhPublico { get; set; }
        public ICollection<Usuario> Participantes { get; set; }
        public int Likes { get; set; }
        public int Dislikes{ get; set; }
        public ICollection<Comentario> Comentarios { get; set; }
        public int QtdMaximaPessoas { get; set; }
        public ICollection<Tags> Tags { get; set; }
    }
}
