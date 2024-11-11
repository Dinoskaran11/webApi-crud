using Crud_webApi.Data;
using Crud_webApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Crud_webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebApiController : ControllerBase
    {

        private readonly DataContext _dataContext;

        public WebApiController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<WebApi>>> GetAllHeroes()
        {
            var heroes = await _dataContext.SuperHeroes.ToListAsync();

            return Ok(heroes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WebApi>> GetHero(int id)
        {
            var hero = await _dataContext.SuperHeroes.FindAsync(id);
            if (hero == null) 
                return BadRequest("Hero Not Found.");
                    
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<WebApi>> AddHero(WebApi hero)
        {
            _dataContext.SuperHeroes.Add(hero);
            await _dataContext.SaveChangesAsync();

            return Ok(hero);
           // return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<WebApi>> UpdateHero(WebApi updatedHero)
        {
            var dbHero = await _dataContext.SuperHeroes.FindAsync(updatedHero.Id);
            if (dbHero == null)
                return BadRequest("Hero Not Found.");

            dbHero.Name = updatedHero.Name;
            dbHero.FirstName = updatedHero.FirstName;
            dbHero.LastName = updatedHero.LastName;
            dbHero.Place = updatedHero.Place;

            await _dataContext.SaveChangesAsync();

            return Ok(dbHero);
        }

        [HttpDelete]
        public async Task<ActionResult<WebApi>> DeleteHero(int id)
        {
            var dbHero = await _dataContext.SuperHeroes.FindAsync(id);
            if (dbHero == null)
                return BadRequest("Hero Not Found.");

            _dataContext.SuperHeroes.Remove(dbHero);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }
    }
}
