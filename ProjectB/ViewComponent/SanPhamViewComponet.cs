using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectB.Data;
using ProjectB.Models;

namespace ProjectB.ViewComponents
{
    public class SanPhamViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public SanPhamViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sanpham = _db.SanPham.ToList(); // Lấy danh sách sản phẩm từ cơ sở dữ liệu
            return View(sanpham);
        }
    }
}
