using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectB.Data;
using ProjectB.Models;
using System.Security.Claims;

namespace ProjectB.Controllers
{
    [Area("Customer")]
    public class GioHangController : Controller
    {
        private readonly ApplicationDbContext _db;

        public GioHangController(ApplicationDbContext db)
        {
            _db = db;
        }

        [Authorize]
        public IActionResult Index()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
            {

                return RedirectToAction("Login", "Account");
            }

            // Lấy danh sách sản phẩm trong giỏ hàng của người dùng
            //  IEnumerable<GioHang> dsGioHang = _db.GioHang
            //   .Include(gh => gh.SanPham)
            // .Where(gh => gh.ApplicationUserId == claim.Value)
            //  .ToList();

            // Tạo hóa đơn mới
            GioHangViewModel giohang = new GioHangViewModel()
            {
                DsGioHang = _db.GioHang.Include("SanPham").Where(gh => gh.ApplicationUserId == claim.Value).ToList(),
                HoaDon  = new HoaDon()

            };
            foreach (var item in giohang.DsGioHang)
            {

                {
                    Double ProductPrice = item.Quantity * item.SanPham.Price;
                    giohang.HoaDon.Total += ProductPrice;
                }
            }

            return View(giohang);
        }

        [Authorize]
        public IActionResult ThanhToan()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
            {

                return RedirectToAction("Login", "Account");
            }

            // Lấy danh sách sản phẩm trong giỏ hàng của người dùng
            //  IEnumerable<GioHang> dsGioHang = _db.GioHang
            //   .Include(gh => gh.SanPham)
            // .Where(gh => gh.ApplicationUserId == claim.Value)
            //  .ToList();

            // Tạo hóa đơn mới
            GioHangViewModel giohang = new GioHangViewModel()
            {
                DsGioHang = _db.GioHang.Include("SanPham").Where(gh => gh.ApplicationUserId == claim.Value).ToList(),
                HoaDon  = new HoaDon()


            };

            giohang.HoaDon.ApplicationUser = _db.ApplicationUser.FirstOrDefault(user => user.Id==claim.Value);

            giohang.HoaDon.Name = giohang.HoaDon.ApplicationUser.Name;
            giohang.HoaDon.Name = giohang.HoaDon.ApplicationUser.Address;
            giohang.HoaDon.Name = giohang.HoaDon.ApplicationUser.PhoneNumber;
            foreach (var item in giohang.DsGioHang)
            {

                {
                    Double ProductPrice = item.Quantity * item.SanPham.Price;
                    giohang.HoaDon.Total += ProductPrice;
                }
            }

            return View(giohang);
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]

        public IActionResult ThanhToan(GioHangViewModel giohang)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claim = identity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
            {

                return RedirectToAction("Login", "Account");
            }



            
            giohang.DsGioHang = _db.GioHang.Include("SanPham").Where(gh => gh.ApplicationUserId == claim.Value).ToList();
                
            giohang.HoaDon.ApplicationUserId = claim.Value;
            giohang.HoaDon.OrderDate = DateTime.Now;
            giohang.HoaDon.OrderStatus = "Đang xác nhận ";
            foreach (var item in giohang.DsGioHang)
            {


                double ProductPrice = item.Quantity * item.SanPham.Price;
                giohang.HoaDon.Total += ProductPrice;

            }
            _db.HoaDon.Add(giohang.HoaDon);
            _db.SaveChanges();
            foreach (var item in giohang.DsGioHang)
            {
                ChiTietHoaDon chitiethoadon = new ChiTietHoaDon()
                {
                    SanPhamId = item.SanPhamId,
                    HoaDonId = giohang.HoaDon.Id,
                    ProductPrice = item.SanPham.Price * item.Quantity,
                    Quantity = item.Quantity
                };
                _db.ChiTietHoaDon.Add(chitiethoadon);
                _db.SaveChanges();
            }
            _db.GioHang.RemoveRange(giohang.DsGioHang);
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }




            // Action Xóa sản phẩm khỏi giỏ hàng
            [HttpGet]
            [Authorize]
            public IActionResult Xoa(int giohangId)
            {
                var giohang = _db.GioHang.FirstOrDefault(gh => gh.Id == giohangId);
                if (giohang != null)
                {
                    _db.GioHang.Remove(giohang);
                    _db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            // Action Giảm số lượng sản phẩm trong giỏ hàng
            [HttpGet]
            [Authorize]
            public IActionResult Giam(int giohangId)
            {
                var giohang = _db.GioHang.FirstOrDefault(gh => gh.Id == giohangId);
                if (giohang != null)
                {
                    giohang.Quantity -= 1;
                    if (giohang.Quantity == 0)
                    {
                        _db.GioHang.Remove(giohang);
                    }
                    _db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            // Action Tăng số lượng sản phẩm trong giỏ hàng
            [HttpGet]
            [Authorize]
            public IActionResult Tang(int giohangId)
            {
                var giohang = _db.GioHang.FirstOrDefault(gh => gh.Id == giohangId);
                if (giohang != null)
                {
                    giohang.Quantity += 1;
                    _db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }
    }
