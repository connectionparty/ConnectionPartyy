using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains;
using Microsoft.AspNetCore.Http;

namespace MVCPresentationLayer.Models
{
    public class EventoInsertViewModel
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public DateTime DataFim { get; set; }
        public TimeSpan HoraFim { get; set; }
        public double? Valor { get; set; }
        public int IdadeMinima { get; set; }
        public bool PrecisaDocumento { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public int QtdMaximaPessoas { get; set; }
        public ICollection<Tags> Tags { get; set; }
        public ICollection<Domains.Usuario> Participantes { get; set; }
        public IFormFile Arquivo { get; set; } 
    }
}