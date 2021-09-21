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
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public double? Valor { get; set; }
        public int IdadeMinima { get; set; }
        public bool PrecisaApresentaDocumento { get; set; }
        public string Endereco { get; set; }
        public int QtdMaximaPessoas { get; set; }
        public ICollection<Tags> Tags { get; set; }
        public IFormFile Arquivo { get; set; } 
    }
}