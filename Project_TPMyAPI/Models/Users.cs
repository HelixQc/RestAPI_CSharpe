namespace Project_TPMyAPI.Models
{
    public class Users
    {
        /***Propreties***/
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        /***Empty***/
        public Users() { }
    }
}
