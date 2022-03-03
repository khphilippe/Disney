using AutoMapper;
using MundoDisney.Commonn.DTOs;
using MundoDisney.Commonn.Responses;
using MundoDisney.Web.Data;
using MundoDisney.Web.Data.Entities;

namespace MundoDisney.Web
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Personaje, PersonajeResponse>();
            CreateMap<Personaje, PersonajeDto>();

            CreateMap<Pelicula, PeliculaResponse>();
            CreateMap<Pelicula, PeliculaDto>();

            CreateMap<PersonajePelicula, PersonajePeliculaPersonaDto>();
            CreateMap<PersonajePelicula, PersonajePeliculaPeliculaDto>();



        }

    }
}