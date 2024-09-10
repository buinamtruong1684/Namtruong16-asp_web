using BaiTapKiemTra01.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaiTapKiemTra01.Controllers
{
    public class TaiKhoanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DangKy(TaiKhoannViewModel taikhoan)
        {
            
            if (taikhoan != null && taikhoan.Password != null && (taikhoan.Password).Length > 0  && taikhoan.Fullname!=null  && taikhoan.Age > 0)
            {
                taikhoan.Password = taikhoan.Password + "_chuoi_ma_hoa";
            }

            return View(taikhoan);
        }
        public IActionResult BaiTap2()
        {
            var sanpham = new SanPhamViewModel()
            {

            };
            return View(sanpham);
        }
    }
   
}
