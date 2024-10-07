using BaiKiemTra03_02.Data;
using BaiKiemTra03_02.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaiKiemTra03_02.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AuthorController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Author> authors = _db.Authors.ToList();
            return View(authors);

        }
        [HttpGet]
        public IActionResult Upsert(int id)
        {
            Author author = new Author();

            
            if (id == 0)
            {
                return View(author);
            }
            else
            {
               
                if (author == null)
                {
                    return NotFound();
                }
                return View(author);

            }
        }
        [HttpPost]
        public IActionResult Upsert(Author author)
        {
            if (ModelState.IsValid)
            {
                if (author.AuthorId == 0)
                {
                   
                    _db.Authors.Add(author);
                }
                else
                {
                 
                    _db.Authors.Update(author);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(author);
        }
    }
}
