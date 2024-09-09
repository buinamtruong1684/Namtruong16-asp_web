using Microsoft.AspNetCore.Mvc;

namespace BTVN2.Controllers
{
    public class Tuan02Controller : Controller
    {
        public IActionResult Index()
        {
            ViewBag.HoTen="Bui Nam Truong";
            ViewBag.MSSV="1822041002";
            ViewBag.Nam="2024";
            return View();
        }
        public IActionResult MayTinh(double a,double b , string pheptinh)
        {
            double ketQua = 0;

            if(pheptinh == "cong")
            {
                ketQua = a + b;
            }
            else if(pheptinh =="tru")
            {
                ketQua = a - b;
            }
            else if(pheptinh=="nhan")
            {
                ketQua = a * b;
            }
            else if(pheptinh=="chia")
            {
               
                
					ketQua = a / b;
				
              

            }
            else
            {
                ViewBag.Error = "Loi:Phep tinh ko hop le";
            }

            ViewBag.KetQua=ketQua;
            return View();
           
        }
    }
}
