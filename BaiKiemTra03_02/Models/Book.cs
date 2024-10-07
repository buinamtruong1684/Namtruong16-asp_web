namespace BaiKiemTra03_02.Models
{
    public class Book
    {
       
            public int BookId { get; set; }
            public string Title { get; set; }
            public int PublicationYear { get; set; }
            public int AuthorId { get; set; }
            public string Genre { get; set; }

           
            public Author Author { get; set; }
   

    }
}
