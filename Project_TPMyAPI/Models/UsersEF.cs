namespace Project_TPMyAPI.Models
{
    public class UsersEF
    {
        /***Propreties***/
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        /***Empty***/
        public UsersEF() { }
    }
}
