using DataAcessObject;
using Domains;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Service.Extensions;
using Service.Interfaces;
using Service.Responses;
using Service.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UsuarioService : IUsuarioService
    {
        public async Task<SingleResponse<Usuario>> Authenticate(string email, string senha)
        {
            try
            {
                using (ConnectionPartyDBContext db = new ConnectionPartyDBContext())
                {
                    Usuario usuario = await db.Usuarios.FirstOrDefaultAsync(c => c.Email == email && c.Senha == senha);
                    if (usuario == null)
                    {
                        return new SingleResponse<Usuario>()
                        {
                            Mensagem = "Usuário e/ou senha inválidos.",
                            Success = false
                        };
                    }
                    return new SingleResponse<Usuario>()
                    {
                        Mensagem = "Usuário encontrado!",
                        Success = true,
                        Item = usuario
                    };
                }
            }
            catch (Exception ex)
            {
                return new SingleResponse<Usuario>()
                {
                    Mensagem = "Erro no banco de dados, contate o administrador",
                    Success = false
                };
            }
        }

        public async Task<Response> Cadastrar(Usuario usuario)
        {
            UsuarioValidator validation = new UsuarioValidator();
            ValidationResult result = validation.Validate(usuario);

            Response r = result.ToResponse();
            if (!r.Success)
            {
                return r;
            }

            try
            {
                using (ConnectionPartyDBContext db = new ConnectionPartyDBContext())
                {
                    Usuario usuarioCadastrado = await db.Usuarios.FirstOrDefaultAsync(c => c.Email == usuario.Email);
                    if (usuarioCadastrado != null)
                    {
                        //Retorna pois a chave única estouraria este registro
                        return new Response()
                        {
                            Success = false,
                            Mensagem = "Esse email já foi cadastrado."
                        };
                    }
                    usuario.DataCadastro = DateTime.Now;
                    db.Usuarios.Add(usuario);
                    await db.SaveChangesAsync();
                    return new Response()
                    {
                        Success = true,
                        Mensagem = "Conta criada com sucesso."

                    };
                }
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    Success = false,
                    Mensagem = "Erro no banco de dados, contate o administrador."
                };
            }
        }


        public Task<SingleResponse<Usuario>> GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse<Usuario>> LerUsuarios()
        {
            throw new NotImplementedException();
        }

        public Task<Response> Update(Usuario u)
        {
            throw new NotImplementedException();
        }
    }
}
