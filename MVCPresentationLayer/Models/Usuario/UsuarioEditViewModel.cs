using Domains;
using Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPresentationLayer.Models
{
    public class UsuarioEditViewModel
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        //public string UserName { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public Genero Genero { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string? Complemento { get; set; }
        public ICollection<Tags> Tags { get; set; }
    }
}
