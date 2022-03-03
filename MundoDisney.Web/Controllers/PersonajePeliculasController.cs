using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MundoDisney.Web.Data;
using MundoDisney.Web.Data.Entities;

namespace MundoDisney.Web.Controllers
{
    public class PersonajePeliculasController : Controller
    {
        private readonly DataContext _context;

        public PersonajePeliculasController(DataContext context)
        {
            _context = context;
        }

        // GET: PersonajePeliculas
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.PersonajePeliculas.Include(p => p.Pelicula).Include(p => p.Personaje);
            return View(await dataContext.ToListAsync());
        }

        // GET: PersonajePeliculas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personajePelicula = await _context.PersonajePeliculas
                .Include(p => p.Pelicula)
                .Include(p => p.Personaje)
                .FirstOrDefaultAsync(m => m.PersonajePeliculaId == id);
            if (personajePelicula == null)
            {
                return NotFound();
            }

            return View(personajePelicula);
        }

        // GET: PersonajePeliculas/Create
        public IActionResult Create()
        {
            ViewData["PeliculaId"] = new SelectList(_context.Peliculas, "PeliculaId", "Titulo");
            ViewData["PersonajeId"] = new SelectList(_context.Personajes, "PersonajeId", "Nombre");
            return View();
        }

        // POST: PersonajePeliculas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonajePeliculaId,PersonajeId,PeliculaId")] PersonajePelicula personajePelicula)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personajePelicula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PeliculaId"] = new SelectList(_context.Peliculas, "PeliculaId", "Titulo", personajePelicula.PeliculaId);
            ViewData["PersonajeId"] = new SelectList(_context.Personajes, "PersonajeId", "Nombre", personajePelicula.PersonajeId);
            return View(personajePelicula);
        }

        // GET: PersonajePeliculas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personajePelicula = await _context.PersonajePeliculas.FindAsync(id);
            if (personajePelicula == null)
            {
                return NotFound();
            }
            ViewData["PeliculaId"] = new SelectList(_context.Peliculas, "PeliculaId", "Titulo", personajePelicula.PeliculaId);
            ViewData["PersonajeId"] = new SelectList(_context.Personajes, "PersonajeId", "Nombre", personajePelicula.PersonajeId);
            return View(personajePelicula);
        }

        // POST: PersonajePeliculas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonajePeliculaId,PersonajeId,PeliculaId")] PersonajePelicula personajePelicula)
        {
            if (id != personajePelicula.PersonajePeliculaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personajePelicula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonajePeliculaExists(personajePelicula.PersonajePeliculaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PeliculaId"] = new SelectList(_context.Peliculas, "PeliculaId", "Titulo", personajePelicula.PeliculaId);
            ViewData["PersonajeId"] = new SelectList(_context.Personajes, "PersonajeId", "Nombre", personajePelicula.PersonajeId);
            return View(personajePelicula);
        }

        // GET: PersonajePeliculas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personajePelicula = await _context.PersonajePeliculas
                .Include(p => p.Pelicula)
                .Include(p => p.Personaje)
                .FirstOrDefaultAsync(m => m.PersonajePeliculaId == id);
            if (personajePelicula == null)
            {
                return NotFound();
            }

            return View(personajePelicula);
        }

        // POST: PersonajePeliculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personajePelicula = await _context.PersonajePeliculas.FindAsync(id);
            _context.PersonajePeliculas.Remove(personajePelicula);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonajePeliculaExists(int id)
        {
            return _context.PersonajePeliculas.Any(e => e.PersonajePeliculaId == id);
        }
    }
}
