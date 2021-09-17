using Domains.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPresentationLayer.Models
{
    public class UsuarioEditViewModel
    {
        public string Nome { get; set; }
        public string UserName { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public Genero Genero { get; set; }
    }
}
