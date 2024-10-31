using System.ComponentModel.DataAnnotations;

namespace _14227_MVC.Models
{
    public class Author
    {
        public int AuthorId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Biography { get; set; }

        // Navigation Property
        public List<Book> Books { get; set; }
    }
}
