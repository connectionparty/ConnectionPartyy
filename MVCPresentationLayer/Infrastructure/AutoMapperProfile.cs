using AutoMapper;
using Domains;
using MVCPresentationLayer.Models;
using MVCPresentationLayer.Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPresentationLayer.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UsuarioInsertViewModel, Usuario>();
            CreateMap<UsuarioEditViewModel, Usuario>();
            CreateMap<Usuario, UsuarioEditViewModel>();
            CreateMap<Usuario, UsuarioQueryViewModel>();
            CreateMap<EventoInsertViewModel, Evento>();
            CreateMap<TagInstertViewModel, Tags>();
        }
    }
}
