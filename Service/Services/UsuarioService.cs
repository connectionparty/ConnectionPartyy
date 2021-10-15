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
                    db.Set<Usuario>().Add(usuario);
                    foreach (Tags item in usuario.Tags)
                    {
                        db.Entry(item).State = EntityState.Unchanged;
                    }
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

        public async Task<SingleResponse<Usuario>> GetByID(int id)
        {
            SingleResponse<Usuario> response = new SingleResponse<Usuario>();

            try
            {
                using (ConnectionPartyDBContext db = new ConnectionPartyDBContext())
                {
                    Usuario usuario = await db.Usuarios.FindAsync(id);
                    if (usuario == null)
                    {
                        response.Success = false;
                        response.Mensagem = "Usuário não encontrado.";
                        return response;
                    }
                    response.Success = true;
                    response.Mensagem = "Usuário selecionado com sucesso.";
                    response.Item = usuario;
                }
            }
            catch (Exception)
            {
                response.Success = false;
                response.Mensagem = "Erro no banco de dados, contate o adm.";
            }
            return response;
        }
        public async Task<SingleResponse<Usuario>> GetByEmail(string email)
        {
            SingleResponse<Usuario> response = new SingleResponse<Usuario>();

            try
            {
                using (ConnectionPartyDBContext db = new ConnectionPartyDBContext())
                {
                    Usuario usuario = await db.Usuarios.FindAsync(email);
                    if (usuario == null)
                    {
                        response.Success = false;
                        response.Mensagem = "Usuário não encontrado";
                        response.Item = usuario;
                    }
                    response.Success = true;
                    response.Mensagem = "Usuário encontrado com sucesso";
                    response.Item = usuario;
                }
            }
            catch (Exception)
            {
                response.Success = false;
                response.Mensagem = "Erro no banco de dados. Contate o administrador.";
            }
            return response;
        }

        public async Task<DataResponse<Usuario>> LerUsuarios()
        {
            DataResponse<Usuario> response = new DataResponse<Usuario>();

            try
            {
                using (ConnectionPartyDBContext db = new ConnectionPartyDBContext())
                {
                    List<Usuario> Usuarios = await db.Usuarios.OrderBy(c => c.ID).ToListAsync();
                    response.Data = Usuarios;
                    response.Success = true;
                    response.Mensagem = "Usuários selecionados com sucesso.";
                }
            }
            catch (Exception)
            {
                response.Success = false;
                response.Mensagem = "Erro no banco de dados, contate o adm.";
            }
            return response;
        }

        public async Task<Response> Update(Usuario u)
        {
            UsuarioValidator validation = new UsuarioValidator();
            ValidationResult result = validation.Validate(u);

            Response r = result.ToResponse();
            if (!r.Success)
            {
                return r;
            }

            try
            {
                using (ConnectionPartyDBContext db = new ConnectionPartyDBContext())
                {
                    //Tecnica 1
                    //db.Entry<Tags>(t).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    //db.SaveChanges();

                    //Tecnica 2
                   
                    Usuario usuarioExistente = await db.Usuarios.FirstOrDefaultAsync(gen => gen.ID == u.ID);
                    if (usuarioExistente == null)
                    {
                        return new Response()
                        {
                            Success = false,
                            Mensagem = "Usuário não encontrado"
                        };
                    }
                    
                    foreach (Tags item in u.Tags)
                    {
                        db.Entry(item).State = EntityState.Unchanged;
                    }

                    Usuario usuarioBanco = await db.Usuarios.FindAsync(u.ID);
                    usuarioBanco.Nome = u.Nome;
                    usuarioBanco.Telefone = u.Telefone;
                    usuarioBanco.DataNascimento = u.DataNascimento;
                    usuarioBanco.Genero = u.Genero;
                    usuarioBanco.Bairro = u.Bairro;
                    usuarioBanco.Rua = u.Rua;
                    usuarioBanco.Numero = u.Numero;
                    usuarioBanco.Complemento = u.Complemento;
                    usuarioBanco.Tags = u.Tags;

                    await db.SaveChangesAsync();
                    return new Response()
                    {
                        Success = true,
                        Mensagem = "Usuário editado com sucesso."
                    };
                }
            }
            catch (Exception ex)
            {
                return ResponseFactory.ResponseDBError(); 
            }
        }
    }
}
