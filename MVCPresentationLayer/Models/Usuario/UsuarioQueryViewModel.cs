using Domains;
using Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPresentationLayer.Models.Usuario
{
    public class UsuarioQueryViewModel
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public Genero Genero { get; set; }
        public DateTime DataCadastro { get; set; }
        public ICollection<Comentario> Comentarios { get; set; }
        public ICollection<Evento> EventosParticipados { get; set; }
        public ICollection<Evento> EventosCriados { get; set; }
    }
}
