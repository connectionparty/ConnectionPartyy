using Domains.Enums;
using System;
using System.Collections.Generic;

namespace Domains
{
    //Facebook não possui campos diferentes para e-mail e telefone no cadastro. É como se tivesse um campo só no banco, ou os campos podem ser nulos (se o e-mail foi preenchido, 
    //o campo de telefone é nulo, e vice versa), mas provavelmente é somente um campo mesmo.

    //Já o google se quer pede número de telefone no cadastro.

    //Endereço pode ser null.
    public class Usuario
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Telefone{ get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }
        public Genero Genero { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string? Complemento { get; set; }
        public DateTime DataCadastro { get; set; }
        public ICollection<Comentario> Comentarios { get; set; }
        public ICollection<Evento> EventosParticipados{ get; set; }
        public ICollection<Evento> EventosCriados { get; set; }
    }
}
