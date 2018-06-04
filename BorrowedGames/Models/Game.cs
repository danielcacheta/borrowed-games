using System.ComponentModel.DataAnnotations;

namespace BorrowedGames.Models
{
    public class Game
    {
        public long Id { get; set; }
        [Required()]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
