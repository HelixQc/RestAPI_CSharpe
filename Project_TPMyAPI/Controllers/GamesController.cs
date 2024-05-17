using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Project_TPMyAPI.Models;
using System.Data;

namespace Project_TPMyAPI.Controllers
{
    [ApiController]
    [Route("Tp/[controller]/[action]")]
    public class GamesController : Controller
    {

        private readonly IConfiguration _config;
        private readonly SqlConnection _conn;
        private readonly SqlCommand _cmd;
        private readonly string _connectionStrings;
        private  SqlDataReader _reader;

        public GamesController(IConfiguration config)
        {
            _config = config;
            _connectionStrings = _config.GetConnectionString("SQLConnection")!;
            _conn = new SqlConnection(_connectionStrings);
            _cmd = new SqlCommand();
            _cmd.CommandType = CommandType.StoredProcedure;
        }




        // GET: GamesController
        public ActionResult Index()
        {

            _cmd.CommandText = "getGames";
            _cmd.Connection = _conn;
            _conn.Open();
            _reader = _cmd.ExecuteReader();

            List<Games> games = new List<Games>();
            while (_reader.Read()) 
            {
                Games game = new Games();
                game.GameId = _reader.GetInt32("GameID");
                game.GameName = _reader.GetString("GameName");
                game.Genre = _reader.GetString("Genre");
                game.Platform = _reader.GetString("Platforms");
                game.ReleaseDate = _reader.GetDateTime("ReleaseDate");
                game.AverageRating = _reader.GetDouble("AverageRating");
                game.Summary = _reader.GetString("Summary");
                game.AddedByUserId = _reader.GetInt32("AddedByUserId");
                games.Add(game);
    
            }
            _conn.Close();
            return Ok(games);
        }

        // GET: GamesController/Details/5
        public ActionResult Details([FromBody]int id)
        {
     
            if (_conn != null)
            {

                _cmd.CommandText = "GetGameDetails";
                _cmd.Connection = _conn;
                _cmd.Parameters.Add(new SqlParameter("@GameId" , id!));
                _conn.Open();
                _reader = _cmd.ExecuteReader();

                Games game = new Games();
                while (_reader.Read())
                {
                    game.GameId = _reader.GetInt32("GameID");
                    game.GameName = _reader.GetString("GameName");
                    game.Genre = _reader.GetString("Genre");
                    game.Platform = _reader.GetString("Platforms");
                    game.ReleaseDate = _reader.GetDateTime("ReleaseDate");
                    game.AverageRating = _reader.GetDouble("AverageRating");
                    game.Summary = _reader.GetString("Summary");
                    game.AddedByUserId = _reader.GetInt32("AddedByUserId");
                }
                _conn.Close();
                return Ok(game);
            }
            return BadRequest("We need an 'int id' in the body of postman");
        }

        // GET: GamesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GamesController/Create
        [HttpPost]
        public ActionResult Create([FromBody]Games game)
        {
            try
            {
                if (_conn != null) 
                {
                    _cmd.CommandText = "CreateGames";
                    _cmd.Connection = _conn;
                    _cmd.Parameters.Add(new SqlParameter("@GameName", game.GameName!));
                    _cmd.Parameters.Add(new SqlParameter("@Genre", game.Genre!));
                    _cmd.Parameters.Add(new SqlParameter("@Platforms", game.Platform!));
                    _cmd.Parameters.Add(new SqlParameter("@ReleaseDate", game.ReleaseDate!));
                    _cmd.Parameters.Add(new SqlParameter("@AverageRating", game.AverageRating!));
                    _cmd.Parameters.Add(new SqlParameter("@Summary", game.Summary!));
                    _cmd.Parameters.Add(new SqlParameter("@AddedByUserID", game.AddedByUserId!));
                    _conn.Open();
                    int rowCounts = _cmd.ExecuteNonQuery();
                    _conn.Close();
                    return Ok($"The games {game.GameName} have been added with succes!");
                }
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: GamesController/Edit/5
        public ActionResult Edit([FromBody]int id)
        {
            
            return Ok(Details(id));
        }


        // POST: GamesController/Edit/5
 /***Do a method get to retrieve the game you want to update. Copy it in the body
 * Switch your method to post with the change made in the json body you just retrieve 
 * then send it to the server***/
        [HttpPost]
        public ActionResult Edit([FromBody]Games game)
        {

            try
            {
                if (_conn != null)
                {
                    _cmd.CommandText = "UpdateGames";
                    _cmd.Connection = _conn;
                    _cmd.Parameters.Add(new SqlParameter("@GameID", game.GameId));
                    _cmd.Parameters.Add(new SqlParameter("@GameName", game.GameName!));
                    _cmd.Parameters.Add(new SqlParameter("@Genre", game.Genre!));
                    _cmd.Parameters.Add(new SqlParameter("@Platforms", game.Platform!));
                    _cmd.Parameters.Add(new SqlParameter("@ReleaseDate", game.ReleaseDate!));
                    _cmd.Parameters.Add(new SqlParameter("@AverageRating", game.AverageRating!));
                    _cmd.Parameters.Add(new SqlParameter("@Summary", game.Summary!));
                    _cmd.Parameters.Add(new SqlParameter("@AddedByUserID", game.AddedByUserId!));
                    _conn.Open();
                    int rowCounts = _cmd.ExecuteNonQuery();
                    _conn.Close();
                    return Ok($"The games {game.GameName} have been updated with success!");
                }
                
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /***Do a method get to retrieve the game you want to delete. Copy it in the body
        * Switch your method to post with the change made in the json body you just retrieve 
        * then send it to the server***/

        // GET: GamesController/Delete/5
        public ActionResult Delete([FromBody] int id)
        {
            return Ok(Details(id));
        }

        // POST: GamesController/Delete/5
        [HttpPost]
        public ActionResult Delete([FromBody] Games game)
        {
            try
            {
                if (_conn != null)
                {
                    _cmd.CommandText = "DeleteGames";
                    _cmd.Connection = _conn;
                    _cmd.Parameters.Add(new SqlParameter("@GameID", game.GameId));
                    _conn.Open();
                    int rowCounts = _cmd.ExecuteNonQuery();
                    _conn.Close();
                    Console.WriteLine(rowCounts);
                    return Ok($"The games  have been delete with success!");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult GetGamesByUser([FromBody]int UserId) 
        {           
            _cmd.CommandText = "GetGamesByUser";
            _cmd.Connection = _conn;
            _cmd.Parameters.Add(new SqlParameter("@AddedByUserID", UserId));
            _conn.Open();
            _reader = _cmd.ExecuteReader();

            List<Games> games = new List<Games>();
            if (_reader.HasRows)
            { 
                while (_reader.Read()) 
                {
                    Games game = new Games();
                    game.GameId = _reader.GetInt32("GameID");
                    game.GameName = _reader.GetString("GameName");
                    game.Genre = _reader.GetString("Genre");
                    game.Platform = _reader.GetString("Platforms");
                    game.ReleaseDate = _reader.GetDateTime("ReleaseDate");
                    game.AverageRating = _reader.GetDouble("AverageRating");
                    game.Summary = _reader.GetString("Summary");
                    game.AddedByUserId = _reader.GetInt32("AddedByUserID");
                    games.Add(game);
                }
            }
            _conn.Close();
            return Ok(games);
        }

        [HttpGet]
        public ActionResult GetGamesByGenre([FromBody] string genre)
        {
            _cmd.CommandText = "GetGamesByGenre";
            _cmd.Connection = _conn;
            _cmd.Parameters.Add(new SqlParameter("@Genre", genre));
            _conn.Open();
            _reader = _cmd.ExecuteReader();

            List<Games> games = new List<Games>();
            if (_reader.HasRows)
            {
                while (_reader.Read())
                {
                    Games game = new Games();
                    game.GameId = _reader.GetInt32("GameID");
                    game.GameName = _reader.GetString("GameName");
                    game.Genre = _reader.GetString("Genre");
                    game.Platform = _reader.GetString("Platforms");
                    game.ReleaseDate = _reader.GetDateTime("ReleaseDate");
                    game.AverageRating = _reader.GetDouble("AverageRating");
                    game.Summary = _reader.GetString("Summary");
                    game.AddedByUserId = _reader.GetInt32("AddedByUserID");
                    games.Add(game);
                }
            }
            _conn.Close();
            return Ok(games);
        }

    }
}
