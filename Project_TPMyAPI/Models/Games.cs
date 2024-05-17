using System.ComponentModel.DataAnnotations.Schema;

namespace Project_TPMyAPI.Models
{
    public class Games
    {
        /***Propreties***/
        public int GameId { get; set; }
        public string GameName { get; set; }
        public string Genre { get; set; }
        public string Platform { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double AverageRating { get; set; }
        public string Summary { get; set; }

        [ForeignKey("AddedByUserId")]
        public int AddedByUserId { get; set; }

        /***Empty***/
        public Games() { }
    }
}
