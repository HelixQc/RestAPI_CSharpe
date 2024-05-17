using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Project_TPMyAPI.Models;
using System.Data;

namespace Project_TPMyAPI.Controllers
{

    [ApiController]
    [Route("Tp/[controller]/[action]")]
    public class UsersController : Controller
    {

        private readonly IConfiguration _config;
        private readonly SqlConnection _conn;
        private readonly SqlCommand _cmd;
        private readonly string _connectionStrings;
        private SqlDataReader _reader;

        public UsersController(IConfiguration config) 
        {
            _config = config;
            _connectionStrings = _config.GetConnectionString("SQLConnection")!;
            _conn = new SqlConnection(_connectionStrings);
            _cmd = new SqlCommand();
            _cmd.CommandType = CommandType.StoredProcedure;
        }

        // GET: UsersController
        public ActionResult Index()
        {
            if (_conn != null) 
            {
                _cmd.CommandText = "getUsers";
                _cmd.Connection = _conn;
                _conn.Open();

                _reader = _cmd.ExecuteReader();

                List<Users> users = new List<Users>();
                while (_reader.Read()) 
                {
                    var user = new Users();
                    user.Id = _reader.GetInt32("UserID");
                    user.Username = _reader.GetString("Username");
                    user.Email = _reader.GetString("Email");
                    users.Add(user);

                }
                _conn.Close();
                return Ok(users);
            }
            return BadRequest("Error");
        }

        // GET: UsersController/Details/5
        public ActionResult Details([FromBody]int id)
        {
            if (_conn != null)
            {
                _cmd.CommandText = "getUsersDetails";
                _cmd.Connection = _conn;
                _cmd.Parameters.Add(new SqlParameter("@UserID", id));
                _conn.Open();

                _reader = _cmd.ExecuteReader();
                Users user = new Users();
                if (_reader.HasRows) 
                {
                    while (_reader.Read()) 
                    {
                        user.Id = _reader.GetInt32("UserID");
                        user.Username = _reader.GetString("Username");
                        user.Email = _reader.GetString("Email");
                    }
                }
                return Ok(user);
            }

            return BadRequest();
        }

        // GET: UsersController/Create
        public ActionResult Create()
        {
            return Ok();
        }

        // POST: UsersController/Create
        [HttpPost]

        public ActionResult Create([FromBody] Users user)
        {
            try
            {
                if (_conn != null)
                {
                    _cmd.CommandText = "createUser";
                    _cmd.Connection = _conn;
                    _cmd.Parameters.Add(new SqlParameter("@Username", user.Username));
                    _cmd.Parameters.Add(new SqlParameter("@Email", user.Email));
                    _cmd.Parameters.Add(new SqlParameter("@Password", user.Password));
                    _conn.Open();

                    int rowCounts = _cmd.ExecuteNonQuery();

                    return Ok($"The user {user.Username} have been added with success");
                }
                return Ok("No connection");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: UsersController/Edit/5
        public ActionResult Edit([FromBody]int id)
        {
            return Ok(Details(id));
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        public ActionResult Edit(Users user)
        {
            try
            {
                if (_conn != null)
                {
                    _cmd.CommandText = "UpdateUser";
                    _cmd.Connection = _conn;
                    _cmd.Parameters.Add(new SqlParameter("@UserID", user.Id));
                    _cmd.Parameters.Add(new SqlParameter("@Username", user.Username));
                    _cmd.Parameters.Add(new SqlParameter("@Email", user.Email));
                    _cmd.Parameters.Add(new SqlParameter("@Password", user.Password));

                    _conn.Open();
                    int countRows = _cmd.ExecuteNonQuery();

                    return Ok($"The user {user.Username} have been updated successfully!");
                }
                return Ok("Connection is lost");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        

        // GET: UsersController/Delete/5
        public async Task<ActionResult> Delete([FromBody]int id)
        {
            return Ok(Details(id));
        }

        // POST: UsersController/Delete/5
        [HttpPost]

        public ActionResult Delete([FromBody]Users user)
        {
            try
            {
                if (_conn != null)
                {
                    _cmd.CommandText = "DeleteUser";
                    _cmd.Connection = _conn;
                    _cmd.Parameters.Add(new SqlParameter("@UserID", user.Id));

                    _conn.Open();
                    int countRows = _cmd.ExecuteNonQuery();
                    return Ok($"The user {user.Username} have been deleted successfully!");
                }
                return Ok("Something went wrong!!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
