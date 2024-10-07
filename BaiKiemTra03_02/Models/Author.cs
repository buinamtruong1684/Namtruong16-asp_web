using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BaiKiemTra03_02.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public int BirthYear { get; set; }

       
        public List<Book> Books { get; set; }

    }
}
