using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MundoDisney.Commonn.DTOs;
using MundoDisney.Commonn.Responses;
using MundoDisney.Web.Data;
using MundoDisney.Web.Data.Entities;
using MundoDisney.Web.Helpers;
using MundoDisney.Web.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisney.Web.Controllers.Api
{

    [ApiController]
    [Route("/[controller]")]

    public class CharactersController : ControllerBase
    {
        private readonly DataContext _context;

        private readonly IMapper _mapper;

        private IConverterHelper _converterHelper;
        public CharactersController(DataContext context, IMapper mapper, IConverterHelper converterHelper)
        {
            _context = context;
            _mapper = mapper;
            _converterHelper = converterHelper;
        }


        //=========================================Listado de personaje=========================================
        // GET: /Characters
        [HttpGet]
        public async Task<IActionResult> GetPersonajes()
        {
            List<Personaje> personajes = await _context.Personajes.ToListAsync();
            var model = _mapper.Map<IEnumerable<PersonajeResponse>>(personajes);
            return Ok(model);
        }

        

        //======================================Detalle de un personaje =======================================
        // GET: characters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Personaje>> GetPersonaje(int id)
        {
            Personaje personaje = await _context.Personajes
                  .Include(p => p.PersonajePeliculas)
                  .ThenInclude(p => p.Pelicula)
                  .FirstOrDefaultAsync(m => m.PersonajeId == id);
            if (personaje == null)
            {
                return NotFound();
            }

           string jsonString = JsonConvert.SerializeObject(personaje, Formatting.None,
          new JsonSerializerSettings()
          {
              ReferenceLoopHandling = ReferenceLoopHandling.Ignore
          }); 
            return Ok(jsonString);
           
        }


        //====================================== Buscar personaje por su nombre ========================================
        // GET:    /characters/name?name=nombreQueHayQuePoner
        [HttpGet("name")]
        public ActionResult<PersonajeDto> GetPersonajeByName([FromQuery] string name)
        {
            Personaje personaje = _context.Personajes
                               .Where(p => p.Nombre == name).FirstOrDefault();

            if (personaje == null)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "El nombre ingresada no coresponde a ningun Personaje existente",
                    Result = Response.StatusCode
                });
            }
            PersonajeViewModel model = _converterHelper.ToPersonajeViewModel(personaje);

            var mapModel = _mapper.Map<PersonajeDto>(model);

            return mapModel;
        }



        //====================================== Buscar personaje por su edad ========================================
        //GET:          /characters/age?age=edadAPoner                      un int
        [HttpGet("age")]
        public ActionResult<PersonajeDto> GetPersonajeByAge([FromQuery] int age)
        {
            Personaje personaje = _context.Personajes
                               .Where(p => p.Edad == age).FirstOrDefault();

            if (personaje == null)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "La edad ingresada no coresponde a ningun Personaje existente",
                    Result = Response.StatusCode
                });
            }
            PersonajeViewModel model = _converterHelper.ToPersonajeViewModel(personaje);
            var mapModel = _mapper.Map<PersonajeDto>(model);
            return mapModel;
        }


        //====================================== Buscar personaje por su peso ========================================
        //GET:            /characters/weight?peso=pesoQueHayQuePoner        un int
        [HttpGet("weight")]
        public ActionResult<PersonajeDto> GetPersonajeByWeight([FromQuery] int peso)
        {
            Personaje personaje = _context.Personajes
                               .Where(p => p.Peso == peso).FirstOrDefault();

            if (personaje == null)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "El peso ingresado no coresponde a ningun Personaje existente",
                    Result = Response.StatusCode
                });
            }

            PersonajeViewModel model = _converterHelper.ToPersonajeViewModel(personaje);

            var mapModel = _mapper.Map<PersonajeDto>(model);

            return mapModel;
        }


        //====================================== Buscar personaje por IdMovie relacionado ========================================
       //GET:             /characters/idmovie?idmovie=iDQueHayQuePoner
        [HttpGet("idMovie")]
        public ActionResult<PersonajePelicula> GetPersonajeByIdMovie([FromQuery] int idMovie)
        {

            PersonajePelicula personajePelicula = _context.PersonajePeliculas
                                .Include(p => p.Personaje)
                                .Where(p => p.PeliculaId == idMovie)
                                .FirstOrDefault();

            if (personajePelicula == null)
            {
                return BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "El id de la pelicula ingresado no coincide con ningun Personaje existente",
                    Result = Response.StatusCode
                });
            }

            var map = _mapper.Map<PersonajePeliculaPersonaDto>(personajePelicula);

            string jsonString = JsonConvert.SerializeObject(map, Formatting.None,
            new JsonSerializerSettings()
            {
              ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Ok(jsonString);
        }



        // PUT: character/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
      //  [Route("/[Controller]")]
        public async Task<IActionResult> PutPersonaje(int id, Personaje personaje)
        {


            if (id != personaje.PersonajeId)
            {
                return BadRequest();
            }

            _context.Entry(personaje).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonajeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // POST: /character
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
  
        public async Task<ActionResult<Personaje>> PostPersonaje(Personaje personaje)
        {
            _context.Personajes.Add(personaje);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonaje", new { id = personaje.PersonajeId }, personaje);
        }

        // DELETE: character/5
      
        [HttpDelete("{id}")]
        public async Task<ActionResult<Personaje>> DeletePersonaje(int id)
        {
            var personaje = await _context.Personajes.FindAsync(id);
            if (personaje == null)
            {
                return NotFound();
            }

            _context.Personajes.Remove(personaje);
            await _context.SaveChangesAsync();

            return personaje;
        }

        private bool PersonajeExists(int id)
        {
            return _context.Personajes.Any(e => e.PersonajeId == id);
        }
    }
}
