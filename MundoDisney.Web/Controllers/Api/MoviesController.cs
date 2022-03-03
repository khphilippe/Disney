using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MundoDisney.Commonn.DTOs;
using MundoDisney.Commonn.Responses;
using MundoDisney.Web.Data;
using MundoDisney.Web.Helpers;
using MundoDisney.Web.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisney.Web.Controllers.Api
{

    [ApiController]
    [Route("[controller]")]

    public class MoviesController : ControllerBase
    {
        private readonly DataContext _context;

        private readonly IMapper _mapper;

        private readonly IConverterHelper _converterHelper;

        public MoviesController(DataContext context, IMapper mapper, IConverterHelper converterHelper)
        {
            _context = context;
            _mapper = mapper;
            _converterHelper = converterHelper;
        }

      public  const string asc = "ASC";
      public  const string desc = "DESC";

        //================================ Lista de peliculas ==========================================
        // GET: /Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pelicula>>> GetPeliculas()
        {
            List<Pelicula> peliculas = await _context.Peliculas
                 .ToListAsync();
            var model = _mapper.Map<IEnumerable<PeliculaResponse>>(peliculas);
            return Ok(model);
        }


        //================================ Buscar Peliculas por titulos ==========================================
        //GET : /movies/name?name=nombre
        [HttpGet("name")]
        public ActionResult<Pelicula> GetByTitulo([FromQuery] string name)
        {
            Pelicula pelicula = _context.Peliculas.Where(c => c.Titulo == name).FirstOrDefault();

            if (pelicula != null)
            {
                return pelicula;
            }
            else
            {
                return BadRequest(new Response
                {
                    Message = "La Pelicula no fue encontrada! ",
                    IsSuccess = true,
                    Result = Response.StatusCode
                });
            }
        }


        //================================ Buscar peliculas por id de genero ==========================================
        //GET : /movies/genre?idgenre=5
        [HttpGet]
        [Route("genre")]
        public ActionResult<PeliculaViewModel> GetPeliculaByGenero([FromQuery] int idGenre)
        {
            Pelicula pelicula = _context.Peliculas
                                 .Where(p => p.Genero.GeneroId == idGenre)
                                 .FirstOrDefault();

            if (pelicula == null)
            {
                return BadRequest(new Response
                {
                    Message = " No existe ninguna Pelicula relacionada con el genero que escribiste! ",
                    IsSuccess = true,
                    Result = Response.StatusCode
                });
            }
            PeliculaViewModel peliculaViewModel = _converterHelper.ToPeliculaViewModel(pelicula);

            var map = _mapper.Map<PeliculaDto>(peliculaViewModel);
            return Ok(map);
        }


        //================================ Ordenar las peliculas por criterio ASC O DESC ==========================================
        // GET:         /movies/ord?order=ASC
      //  GET:	       /movies/ord? order = DESC
      [HttpGet("ord")]
        public ActionResult<IEnumerable<Pelicula>> OrderList([FromQuery] string order)
        {
            if(order !=asc && order != desc )
            {
                return NotFound(new Response
                {
                    Message = " No se puede ordenar la lista con el criterio que entraste! Los criterios admitidos son ASC o DESC ",
                    IsSuccess = true,
                    Result = Response.StatusCode
                });
            }
          return  Ok(OrdenarSegun(order));
        }

        
        public IEnumerable<Pelicula> OrdenarSegun(string criterio)
        {
            List<Pelicula> order = new List<Pelicula>();
          
            switch (criterio)
            {
                case asc:
                    order = _context.Peliculas.OrderBy(p => p.Titulo).ToList();
                    return order;
                case desc:
                    order = _context.Peliculas.OrderByDescending(p => p.Titulo).ToList();
                    return order;
                default:
                    return order;           
            }
                    
        } 



        //================================ Detalle peliculas ==========================================
        // GET:/movies/5                    
        [HttpGet("{id}")]
        public async Task<ActionResult<Pelicula>> GetPelicula(int id)
        {
            Pelicula pelicula = await _context.Peliculas
                .Include(p => p.Genero)
               .Include(p => p.PersonajePeliculas)
                .ThenInclude(p => p.Personaje)
                .FirstOrDefaultAsync(p => p.PeliculaId == id);

            if (pelicula == null)
            {
                return NotFound();
            }

            string jsonString = JsonConvert.SerializeObject(pelicula, Formatting.None,
         new JsonSerializerSettings()
         {
             ReferenceLoopHandling = ReferenceLoopHandling.Ignore
         });
            return Ok(jsonString);
        }





        // PUT: movies/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPelicula(int id, Pelicula pelicula)
        {
            if (id != pelicula.PeliculaId)
            {
                return BadRequest();
            }

            _context.Entry(pelicula).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeliculaExists(id))
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

        // POST: /movies
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Pelicula>> PostPelicula(Pelicula pelicula)
        {
            _context.Peliculas.Add(pelicula);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPelicula", new { id = pelicula.PeliculaId }, pelicula);
        }

        // DELETE: movies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pelicula>> DeletePelicula(int id)
        {
            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula == null)
            {
                return NotFound();
            }

            _context.Peliculas.Remove(pelicula);
            await _context.SaveChangesAsync();

            return pelicula;
        }

        private bool PeliculaExists(int id)
        {
            return _context.Peliculas.Any(e => e.PeliculaId == id);
        }




    }
}
