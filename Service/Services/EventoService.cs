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
    public class EventoService : IEventoService
    {
        public async Task<Response> Cadastrar(Evento e)
        {
            EventoValidator validation = new EventoValidator();
            ValidationResult result = validation.Validate(e);

            Response r = result.ToResponse();
            if (!r.Success)
            {
                return r;
            }

            try
            {
                using (ConnectionPartyDBContext db = new ConnectionPartyDBContext())
                {
                    db.Set<Evento>().Add(e);
                    foreach (Tags item in e.Tags)
                    {
                        db.Entry(item).State = EntityState.Unchanged;
                    }

                    db.Set<Evento>().Add(e);
                    foreach (Usuario item in e.Participantes)
                    {
                        db.Entry(item).State = EntityState.Unchanged;
                    }
                    await db.SaveChangesAsync();
                    return new Response()
                    {
                        Success = true,
                        Mensagem = "Evento cadastrado com sucesso."
                    };
                }
            }
            catch (Exception ex)
            {
                return ResponseFactory.ResponseDBError();
            }
        }

        public async Task<Response> CheckInUsuario(int idEvento, int idUsuario)
        {
            Response response = new Response();

            try
            {
                using (ConnectionPartyDBContext db = new ConnectionPartyDBContext())
                {
                    Evento eventoBanco = await db.Eventos.Include(c => c.Participantes).FirstOrDefaultAsync(c => c.ID == idEvento);
                    Usuario usuario = new Usuario();
                    usuario.ID = idUsuario;
                    eventoBanco.Participantes.Add(usuario);
                    db.Entry(usuario).State = EntityState.Unchanged;
                    await db.SaveChangesAsync();
                    response.Success = true;
                    response.Mensagem = "Ae porra deu certo";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Mensagem = ex.ToString();
            }
            return response;

        }

        public async Task<Response> Comentar(Comentario c)
        {
            ComentarioValidator validation = new ComentarioValidator();
            ValidationResult result = validation.Validate(c);

            Response r = result.ToResponse();
            if (!r.Success)
            {
                return r;
            }

            try
            {
                using (ConnectionPartyDBContext db = new ConnectionPartyDBContext())
                {
                    db.Add(c);
                    await db.SaveChangesAsync();
                    return new Response()
                    {
                        Success = true,
                        Mensagem = "Comentário publicado com sucesso."
                    };
                }
            }
            catch (Exception)
            {
                return ResponseFactory.ResponseDBError();
            }
        }

        public async Task<Response> Curtir(int idEvento)
        {
            Response response = new SingleResponse<Evento>();

            try
            {
                using (ConnectionPartyDBContext db = new ConnectionPartyDBContext())
                {
                    Evento evento = await db.Eventos.FindAsync(idEvento);
                    if (evento == null)
                    {
                        response.Success = false;
                        response.Mensagem = "Evento não encontrado";
                        return response;
                    }
                    evento.Likes++;
                    response.Success = true;
                    response.Mensagem = "Obrigado por curtir (y).";
                    await db.SaveChangesAsync();
                    return response;
                }
            }
            catch (Exception)
            {
                return ResponseFactory.ResponseDBError();
            }
        }

        public async Task<Response> Descurtir(int idEvento)
        {
            Response response = new SingleResponse<Evento>();

            try
            {
                using (ConnectionPartyDBContext db = new ConnectionPartyDBContext())
                {
                    Evento evento = await db.Eventos.FindAsync(idEvento);
                    if (evento == null)
                    {
                        response.Success = false;
                        response.Mensagem = "Evento não encontrado";
                        return response;
                    }
                    evento.Dislikes++;
                    response.Success = true;
                    response.Mensagem = "Obrigado por descurtir (y).";
                    await db.SaveChangesAsync();
                    return response;
                }
            }
            catch (Exception)
            {
                return ResponseFactory.ResponseDBError();
            }
        }

        public async Task<SingleResponse<Evento>> GetByID(int id)
        {
            SingleResponse<Evento> eventoResponse = new SingleResponse<Evento>();

            try
            {
                using (ConnectionPartyDBContext db = new ConnectionPartyDBContext())
                {
                    Evento evento = await db.Eventos.Include(c => c.Tags).Include(c => c.Organizador).Include(c => c.Participantes).Include(c => c.Comentarios).ThenInclude(c=> c.Usuario).FirstOrDefaultAsync(c => c.ID == id);
                    if (evento == null)
                    {
                        eventoResponse.Success = false;
                        eventoResponse.Mensagem = "Evento não encontrado";
                        return eventoResponse;
                    }
                    eventoResponse.Success = true;
                    eventoResponse.Mensagem = "Evento encontrado com sucesso.";
                    eventoResponse.Item = evento;
                }
            }

            catch (Exception)
            {
                eventoResponse.Success = false;
                eventoResponse.Mensagem = "Erro no banco de dados. Contate o administrador.";
            }
            return eventoResponse;
        }

        public async Task<DataResponse<Evento>> LerEventos()
        {
            DataResponse<Evento> eventoResponse = new DataResponse<Evento>();

            try
            {
                using (ConnectionPartyDBContext db = new ConnectionPartyDBContext())
                {
                    List<Evento> eventos = await db.Eventos.OrderBy(c => c.ID).ToListAsync();
                    eventoResponse.Success = true;
                    eventoResponse.Data = eventos;
                    eventoResponse.Mensagem = "Eventos encontrados com sucesso.";
                }
            }
            catch (Exception ex)
            {
                eventoResponse.Success = false;
                eventoResponse.Mensagem = "Erro no banco de dados, contate o administrador.";
            }
            return eventoResponse;
        }

        public async Task<DataResponse<Evento>> LerEventosPreferencia(int idUser)
        {
            DataResponse<Evento> eventoResponse = new DataResponse<Evento>();
            try
            {
                using (ConnectionPartyDBContext db = new ConnectionPartyDBContext())
                {
                    //Seleciona no banco de dados o usuário logado e trás as tags dele!
                    Usuario usuario = await db.Usuarios.Include(c => c.Tags).ThenInclude(c=> c.Eventos).ThenInclude(c=>c.Comentarios).FirstOrDefaultAsync(c => c.ID == idUser);
                    List<Tags> tags = usuario.Tags.ToList();
                    HashSet<Evento> eventosLinhaTempo = new HashSet<Evento>();
                    foreach (var tag in tags)
                    {
                        List<Evento> eventos = tag.Eventos.Where(c => c.DataHoraFim > DateTime.Now).OrderBy(c=> c.Likes).ThenBy(c=> c.Comentarios.Count).ToList();
                        foreach (var item in eventos)
                        {
                            eventosLinhaTempo.Add(item);
                        }
                    }
                    eventoResponse.Data = eventosLinhaTempo.ToList();
                    return eventoResponse;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Response> Update(Evento e)
        {
            EventoValidator validation = new EventoValidator();
            ValidationResult result = validation.Validate(e);

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
                    //db.Entry<Genero>(g).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    //db.SaveChanges();

                    //Tecnica 2
                    Evento eventoExistente = await db.Eventos.FirstOrDefaultAsync(gen => gen.ID == e.ID);

                    Evento eventoBanco = await db.Eventos.FindAsync(e.ID);
                    eventoBanco.ID = e.ID;
                    await db.SaveChangesAsync();
                    return new Response()
                    {
                        Success = true,
                        Mensagem = "Evento editado com sucesso."
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
