using System.ComponentModel.DataAnnotations;

namespace BorrowedGames.Models
{
    public class Friend
    {
        public long Id { get; set; }
        [Required()]
        [MaxLength(100)]
        public string Name { get; set; }
        [RegularExpression(@"^\([1-9]{2}\) (?:[2-8]|9[1-9])[0-9]{3}\-[0-9]{4}$")]
        public string Phone { get; set; }
    }
}
