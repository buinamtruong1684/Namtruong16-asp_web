using BaiKiemTra03_02.Data;
using BaiKiemTra03_02.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BaiKiemTra03_02.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var books = _db.Books.Include(b => b.Author).ToList();
            ViewBag.Books = books;
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _db.Books.Add(book);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

                return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var book = _db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            ViewBag.Authors = _db.Authors.ToList(); 
            return View(book);
        }

       
        [HttpPost]
        public IActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                _db.Books.Update(book);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Authors = _db.Authors.ToList(); 
            return View(book);
        }

        
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var book = _db.Books.Include(b => b.Author).FirstOrDefault(b => b.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
        [HttpPost]
        public IActionResult DeleteConfirm(int id)
        {
            var book = _db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            _db.Books.Remove(book);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var book = _db.Books.Include(b => b.Author).FirstOrDefault(b => b.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

    }
}
