using BaiKiemTra02.Data;
using BaiKiemTra02.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaiKiemTra02.Controllers
{
  
    public class LopHocController : Controller
    {
        private readonly ApplicationDbContext _db;
        public LopHocController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var lophocs = _db.LopHocs.ToList();
            ViewBag.LopHocs = lophocs;
            return View();
        }
       

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        public IActionResult Create(LopHoc lophoc)
        {
            if (ModelState.IsValid)
            {
                _db.LopHocs.Add(lophoc);
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
            var lophoc = _db.LopHocs.Find(id);
            if (lophoc == null)
            {
                return NotFound();
            }
            return View(lophoc);
        }

       
        [HttpPost]
        public IActionResult Edit(LopHoc lophoc)
        {
            if (ModelState.IsValid)
            {
                _db.LopHocs.Update(lophoc);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lophoc);
        }

      
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var lophoc = _db.LopHocs.Find(id);
            if (lophoc == null)
            {
                return NotFound();
            }
            return View(lophoc);
        }

     
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var lophoc = _db.LopHocs.Find(id);
            if (lophoc == null)
            {
                return NotFound();
            }
            _db.LopHocs.Remove(lophoc);
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

            var lophoc = _db.LopHocs.Find(id);
            if (lophoc == null)
            {
                return NotFound();
            }
            return View(lophoc);
        }

       
      
        
    }
}
