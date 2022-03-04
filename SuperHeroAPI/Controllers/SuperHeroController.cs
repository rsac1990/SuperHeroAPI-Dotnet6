using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    // se recomienda q la logica no vaya en el controlador sino en servicios aparte
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()// si quiero que swager me devuelva el formato del modelo lo cambio por Task<ActionResult<List<SuperHero>>
        {
            //return Ok(heroes); //obtiene de la lista estatica
            return Ok(await _context.SuperHeroes.ToListAsync()); //obtiene de la base de datos
        }

        [HttpGet("(id)")]
        public async Task<ActionResult<SuperHero>> Get( int id)// debo mandar el parametro en la linea anterior tb y deben llamarse igual
        {
            // esta es una manera var hero= heroes[id];
            //var hero = heroes.Find(h => h.Id==id); //esto es de la lista
            var hero = await _context.SuperHeroes.FindAsync(id); //esto de la bd

            if (hero == null)
            {
                return BadRequest("hero not found");
            }

            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero) //este nombre no importa realmente xq es en la linea anterior q defini el tipo de llamada
        {
            _context.SuperHeroes.Add(hero);
           await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync()); //obtiene de la base de datos
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero requestedHero) //este nombre no importa realmente xq es en la linea anterior q defini el tipo de llamada
        {
            //var hero = heroes.Find(h => h.Id == requestedHero.Id);
            var dbhero = await _context.SuperHeroes.FindAsync(requestedHero.Id); //esto de la bd
            if (dbhero == null)
            {
                return BadRequest("hero not found");
            }
            dbhero.Name = requestedHero.Name;
            dbhero.FirstName = requestedHero.FirstName;
            dbhero.LastName = requestedHero.LastName;
            dbhero.Place = requestedHero.Place;
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpDelete("(id)")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id) 
        {
            var dbhero = await _context.SuperHeroes.FindAsync(id); //esto de la bd

            if (dbhero == null)
            {
                return BadRequest("hero not found");
            }
            _context.SuperHeroes.Remove(dbhero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}
