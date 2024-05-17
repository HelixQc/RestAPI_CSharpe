using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_TPMyAPI.Data;
using Project_TPMyAPI.Models;

namespace Project_TPMyAPI.Controllers
{
    [ApiController]
    [Route("Tp/[controller]/[action]")]
    public class GamesEFController : Controller
    {
        AppDbContext _context;

        public GamesEFController( AppDbContext context)
        {
            _context = context;
        }

        // GET: GamesEFController
        public ActionResult Index()
        {
            var games = _context.GamesEF.ToList();
            return Ok(games);
        }

        // GET: GamesEFController/Details/5
        public async Task<ActionResult> Details([FromBody]int id)
        {
            var game = await _context.GamesEF.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(game);
        }

        // GET: GamesEFController/Create
        public ActionResult Create()
        {
            return Ok();
        }

        // POST: GamesEFController/Create
        [HttpPost]
        public ActionResult Create([FromBody] GamesEF game)
        {
            try
            {
                _context.GamesEF.Add(game);
                _context.SaveChanges();
                return Ok($"The game {game.GameName} have been added with success!!");
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: GamesEFController/Edit/5
        public ActionResult Edit([FromBody]int id)
        {
            return Ok(Details(id)!);
        }

        // POST: GamesEFController/Edit/5
        [HttpPost]
        public ActionResult Edit([FromBody] GamesEF game)
        {
            try
            {
                _context.GamesEF.Update(game);
                _context.SaveChanges();
                return Ok($"The game {game.GameName} have been updated with success");
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: GamesEFController/Delete/5
        public async Task<ActionResult> Delete([FromBody]int id)
        {
            return Ok(await Details(id)!);
        }

        // POST: GamesEFController/Delete/5
        [HttpPost]
        public ActionResult Delete([FromBody] GamesEF game)
        {
            try
            {
                _context.GamesEF.Remove(game);
                _context.SaveChanges();
                return Ok($"The game {game.GameName} have been removed with success!");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public ActionResult GetGamesByUser([FromBody] int UserId) 
        {
            var games = _context.GamesEF.Where(options => options.UserId == UserId);
            return Ok(games);
        }
        [HttpGet]
        public ActionResult GetGamesByGenre([FromBody] string genre) 
        {
            var games = _context.GamesEF.Where(options => options.Genre == genre);
            return Ok(games);
        }
    }
}
