using System;
using System.Collections.Generic;

namespace Domains
{
    public class Evento
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public double? Valor { get; set; }
        public int IdadeMinima { get; set; }
        public bool PrecisaDocumento { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string? Complemento { get; set; }
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
