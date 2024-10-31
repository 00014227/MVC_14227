using System.ComponentModel.DataAnnotations;

namespace _14227_MVC.Models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required]
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishedDate { get; set; }

        public int AuthorId { get; set; }

        // Navigation Property
        public Author Author { get; set; }
    }
}
