using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_TPMyAPI.Data;
using Project_TPMyAPI.Models;

namespace Project_TPMyAPI.Controllers
{
    [ApiController]
    [Route("tp/[controller]/[action]")]
    public class UsersEFController : Controller
    {

        AppDbContext _context;

        public UsersEFController(AppDbContext context) 
        {
            _context = context;
        }


        // GET: UsersEFController
        public async Task<ActionResult> Index()
        {
            var usersEF = await _context.UsersEF.ToListAsync();
            return Ok(usersEF);
        }

        // GET: UsersEFController/Details/5
        public ActionResult Details([FromBody]int id)
        {
            var user =  _context.UsersEF.FirstOrDefault(x => x.Id == id);
            return Ok(user);
        }

        // GET: UsersEFController/Create
        public ActionResult Create()
        {
            return Ok();
        }

        // POST: UsersEFController/Create
        [HttpPost]

        public ActionResult Create([FromBody] UsersEF user)
        {
            try
            {
                _context.UsersEF.Add(user);
                _context.SaveChanges();
                return Ok($"The user {user.Username} have been added with success!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: UsersEFController/Edit/5
        public ActionResult Edit([FromBody] int id)
        {
            return Ok(Details(id)!);
        }

        // POST: UsersEFController/Edit/5
        [HttpPost]

        public ActionResult Edit([FromBody] UsersEF user)
        {
            try
            {
                _context.UsersEF.Update(user);
                _context.SaveChanges();
                return Ok($"The user {user.Username} have been updates successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: UsersEFController/Delete/5
        public ActionResult Delete([FromBody] int id)
        {
            return Ok(Details(id)!);
        }

        // POST: UsersEFController/Delete/5
        [HttpPost]
        public ActionResult Delete([FromBody] UsersEF user)
        {
            try
            {
                _context.UsersEF.Remove(user);
                _context.SaveChanges();
                return Ok($"The user {user.Username} have been remove successfully");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
