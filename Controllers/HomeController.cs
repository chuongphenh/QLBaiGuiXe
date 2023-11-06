using Microsoft.AspNetCore.Mvc;
using QLBaiGuiXe.Models;
using System.Diagnostics;
using System.IO; //thao tac voi file, thu muc
using System.Data;//su dung DataTalbe, DataRow
using System.Data.SqlClient;//su dung SqlConnection, DataAdapter...
using System.Security.Cryptography;
namespace QLBaiGuiXe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public QlbaiGuiXeContext db = new QlbaiGuiXeContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<BaiXe> list_BaiXe = db.BaiXes.ToList();

            return View(list_BaiXe);
        }

        [HttpPost]
        public IActionResult Index1()
        {
            ViewBag.loai_xe = Convert.ToInt32(Request.Form["loai_xe"]);
            List<BaiXe> list_BaiXe = db.BaiXes.ToList();
            return View(list_BaiXe);
        }
        [HttpPost]
        public IActionResult GuiXe()
        {
            int _loai_xe = Convert.ToInt32(Request.Form["loai_xe"]);
            string _bai_xe = Request.Form["bai_xe"].ToString().Trim();
            int _loai_ve = Convert.ToInt32(Request.Form["loai_ve"]);
            ViewBag.loai_xe = _loai_xe;
            ViewBag.bai_xe = _bai_xe;

            if (_loai_ve == 0)
            {
                return View("QlGuiXe");
            }
            else
            {
                return View("QlGuiXe_VeThang");
            }

        }
        [HttpPost]
        public IActionResult GuiXePost()
        {
            string _ma_ve_luot = Request.Form["ma_ve_luot"].ToString().Trim();
            int _loai_xe = Convert.ToInt32(Request.Form["loai_xe"]);
            string _bai_xe = Request.Form["bai_xe"].ToString().Trim();
            string _ten_xe = Request.Form["ten_xe"].ToString().Trim();
            string _dong_xe = Request.Form["dong_xe"].ToString().Trim();
            string _bien_so = Request.Form["bien_so"].ToString().Trim();
            string _mau_xe = Request.Form["mau_xe"].ToString().Trim();

            Xe xe = new Xe();
            VeXe veXe = new VeXe();
            List<BaiXe> list_BaiXe = db.BaiXes.ToList();
            var record_bai_xe = db.BaiXes.Where(item => item.TenBaiXe == _bai_xe).FirstOrDefault();

            xe.IdLoaiXe = _loai_xe + 1;
            xe.TenXe = _ten_xe;
            xe.BienSo = _bien_so;
            xe.MauSac = _mau_xe;
            xe.DongXe = _dong_xe;
            db.Xes.Add(xe);
            db.SaveChanges();

            veXe.MaVe = _ma_ve_luot;
            switch (_loai_xe)
            {
                case 0:
                    veXe.IdLoaiVe = 1;
                    break;
                case 1:
                    veXe.IdLoaiVe = 2;
                    break;
                case 2:
                    veXe.IdLoaiVe = 3;
                    break;
                default:
                    break;
            }
            veXe.IdXe = xe.Id;
            veXe.NgayVao = DateTime.Now;
            string _Photo = "";
            try
            {
                _Photo = Request.Form.Files[0].FileName;
            }
            catch {; }
            if (!string.IsNullOrEmpty(_Photo))
            {
                //upload anh moi
                var timestamp = DateTime.Now.ToFileTime();//chuyen thoi gian ra thanh mot so nguyen
                _Photo = timestamp + "_" + _Photo;//noi chuoi thoi gian vao bien _Photo
                                                  //lay duong dan cua file
                                                  //Path.Combine(duongdan1,duongdan2...) noi duongdan1 va duongdan2... thanh mot duong dan
                string _Path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/Xe", _Photo);
                //upload file
                using (var stream = new FileStream(_Path, FileMode.Create))
                {
                    Request.Form.Files[0].CopyTo(stream);
                }
                //cap nhat lai duong dan anh
                veXe.HinhAnh = _Photo;
            }
            veXe.TrangThai = "DangGuiXe";
            veXe.IdBaiXe = record_bai_xe.Id;
            db.VeXes.Add(veXe);
           


            if (_loai_xe == 0 || _loai_xe == 1)
            {
                record_bai_xe.SoChoTrongXeDapMay--;
            }
            else
            {
                record_bai_xe.SoChoTrongOTo--;
            }
            db.SaveChanges();

            ViewBag.NotiGuiXesuccess = "Gửi xe thành công";

            return View("Index", list_BaiXe);
        }
        [HttpPost]
        public IActionResult GuiXePostVeThang()
        {
            int _loai_xe = Convert.ToInt32(Request.Form["loai_xe"]);
            string _ma_ve_thang = Request.Form["ma_ve_thang"].ToString().Trim();
            string _btn_gui_xe = Request.Form["btn_gui_xe"].ToString().Trim();
            string _bai_xe = Request.Form["bai_xe"].ToString().Trim();

            var record_bai_xe = db.BaiXes.Where(item => item.TenBaiXe == _bai_xe).FirstOrDefault();

            ViewBag.ma_ve_thang = _ma_ve_thang;
            ViewBag.bai_xe = _bai_xe;
            var list_ve_thang = db.VeXes.Where(v => v.NgayDangKy != null).ToList();
            var record = (from item_ve_xe in db.VeXes
                          join item_loai_ve in db.LoaiVes on item_ve_xe.IdLoaiVe equals item_loai_ve.Id
                          join item_order in db.Orders on item_ve_xe.Id equals item_order.IdVeXe
                          join item_user in db.Users on item_order.IdUser equals item_user.Id
                          join item_xe in db.Xes on item_ve_xe.IdXe equals item_xe.Id
                          join item_loai_xe in db.LoaiXes on item_xe.IdLoaiXe equals item_loai_xe.Id
                          where item_ve_xe.MaVe == _ma_ve_thang
                          select new
                          {
                              Order = item_order,
                              User = item_user,
                              Xe = item_xe,
                              VeXe = item_ve_xe,
                              LoaiVe = item_loai_ve,
                              LoaiXe = item_loai_xe
                          }).FirstOrDefault();
            ViewBag.Record = record;

            if (_btn_gui_xe == "Gửi xe")
            {
                List<BaiXe> list_BaiXe = db.BaiXes.ToList();
                var RecordVeXe = db.VeXes.Where(item => item.MaVe == _ma_ve_thang).FirstOrDefault();
                if (RecordVeXe != null)
                {
                    RecordVeXe.TrangThai = "DangGuiXe";
                    RecordVeXe.IdBaiXe = record_bai_xe.Id;
                    RecordVeXe.NgayVao = DateTime.Now;
                    if (_loai_xe == 0 || _loai_xe == 1)
                    {
                        record_bai_xe.SoChoTrongXeDapMay--;
                    }
                    else
                    {
                        record_bai_xe.SoChoTrongOTo--;
                    }
                    db.SaveChanges();
                }
                ViewBag.NotiGuiXesuccess = "Gửi xe thành công";
                return View("Index", list_BaiXe);
            }

            bool check = false;
            foreach (var v in list_ve_thang)
            {
                if (_ma_ve_thang == v.MaVe)
                {
                    check = true;
                }
            }
            if (check)
            {
                ViewBag.loai_xe = _loai_xe;
                ViewBag.noti = "Tìm thấy vé tháng!";
                ViewBag.btnGuiXe = "Gửi xe";
                ViewBag.btnGuiXeTC = "Từ chối";
                return View("QlGuiXe_VeThang");
            }
            else
            {
                ViewBag.loai_xe = _loai_xe;
                ViewBag.noti = "Mã vé tháng không hợp lệ hoặc đã hết hạn!";
                return View("QlGuiXe_VeThang");
            }


        }
        public IActionResult DanhSachXeDangGui()
        {
            var records = (from item_ve_xe in db.VeXes
                           join item_loai_ve in db.LoaiVes on item_ve_xe.IdLoaiVe equals item_loai_ve.Id
                           join item_xe in db.Xes on item_ve_xe.IdXe equals item_xe.Id
                           join item_loai_xe in db.LoaiXes on item_xe.IdLoaiXe equals item_loai_xe.Id
                           join item_bai_xe in db.BaiXes on item_ve_xe.IdBaiXe equals item_bai_xe.Id
                           where item_ve_xe.TrangThai == "DangGuiXe"
                           select new
                           {
                               Xe = item_xe,
                               VeXe = item_ve_xe,
                               LoaiVe = item_loai_ve,
                               LoaiXe = item_loai_xe,
                               BaiXe = item_bai_xe,
                           }).OrderBy(record => record.BaiXe.TenBaiXe).ToList();
            // Truy cập các thông tin trong record
            ViewBag.listRecords = records;
            return View();
        }

        public IActionResult TaoVeThang()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TaoVeThangPost()
        {
            string _ma_ve = Request.Form["ma_ve"].ToString().Trim();
            int _loai_xe = Convert.ToInt32(Request.Form["loai_xe"]);
            string _ho_ten = Request.Form["ho_ten"].ToString().Trim();
            string _so_dien_thoai = Request.Form["so_dien_thoai"].ToString().Trim();
            string _gioi_tinh = Request.Form["gioi_tinh"].ToString().Trim();
            string _dia_chi = Request.Form["dia_chi"].ToString().Trim();
            string _ten_xe = Request.Form["ten_xe"].ToString().Trim();
            string _bien_so = Request.Form["bien_so"].ToString().Trim();
            string _dong_xe = Request.Form["dong_xe"].ToString().Trim();
            string _mau_xe = Request.Form["mau_xe"].ToString().Trim();
            int _so_thang = Convert.ToInt32(Request.Form["so_thang"]);
            int _loai_ve = 0;


            VeXe veXe = new VeXe();
            User user = new User();
            Xe xe = new Xe();
            Order order = new Order();

            user.TenDayDu = _ho_ten;
            user.SoDienThoai = _so_dien_thoai;
            user.DiaChi = _dia_chi;
            user.GioiTinh = _gioi_tinh;
            db.Users.Add(user);
            db.SaveChanges();

            xe.IdLoaiXe = _loai_xe;
            xe.TenXe = _ten_xe;
            xe.BienSo = _bien_so;
            xe.MauSac = _mau_xe;
            xe.DongXe = _dong_xe;
            xe.IdUser = user.Id;
            db.Xes.Add(xe);
            db.SaveChanges();

            veXe.MaVe = _ma_ve;
            switch (_loai_xe)
            {
                case 1:
                    veXe.IdLoaiVe = 4;
                    _loai_ve = 4;
                    break;
                case 2:
                    veXe.IdLoaiVe = 5;
                    _loai_ve = 5;
                    break;
                case 3:
                    veXe.IdLoaiVe = 6;
                    _loai_ve = 6;
                    break;
                default:
                    break;
            }
            veXe.IdXe = xe.Id;
            veXe.NgayDangKy = DateTime.Now;
            veXe.NgayHetHan = DateTime.Now.AddDays(_so_thang * 30);

            string _Photo = "";
            try
            {
                _Photo = Request.Form.Files[0].FileName;
            }
            catch {; }
            if (!string.IsNullOrEmpty(_Photo))
            {
                //upload anh moi
                var timestamp = DateTime.Now.ToFileTime();//chuyen thoi gian ra thanh mot so nguyen
                _Photo = timestamp + "_" + _Photo;//noi chuoi thoi gian vao bien _Photo
                                                  //lay duong dan cua file
                                                  //Path.Combine(duongdan1,duongdan2...) noi duongdan1 va duongdan2... thanh mot duong dan
                string _Path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/Xe", _Photo);
                //upload file
                using (var stream = new FileStream(_Path, FileMode.Create))
                {
                    Request.Form.Files[0].CopyTo(stream);
                }
                //cap nhat lai duong dan anh
                veXe.HinhAnh = _Photo;
            }
            db.VeXes.Add(veXe);
            db.SaveChanges();

            var giaTien = (from item_loaiVe in db.LoaiVes where item_loaiVe.Id == _loai_ve select item_loaiVe.GiaVe).SingleOrDefault();
            double tongTien = 0;
            if (giaTien != null)
            {
                tongTien = (double)(giaTien * (double)_so_thang);
                // Sử dụng giá trị của tongTien ở đây
            }
            else
            {
                tongTien = 0;
            }
            //var record = from item_veXe in db.VeXes join item_loaiVe in db.LoaiVes on item_veXe.IdLoaiVe equals item_loaiVe.Id where item_veXe.IdLoaiVe == _loai_ve select item_loaiVe.GiaVe;

            order.IdVeXe = veXe.Id;
            order.IdUser = user.Id;
            order.TongTien = Convert.ToInt32(tongTien);

            var loaive = db.LoaiVes.Where(v => v.Id == _loai_ve).FirstOrDefault();
            var loaixe = db.LoaiXes.Where(v => v.Id == _loai_xe).FirstOrDefault();

            order.BienSo = _bien_so;
            order.LoaiVe = loaive.TenLoaiVe;
            order.LoaiXe = loaixe.TenLoaiXe;
            order.TenXe = _ten_xe;
            order.MauSac = _mau_xe;

            db.Orders.Add(order);
            db.SaveChanges();



            return Redirect("XacNhanDangKy/" + order.Id);
        }

        public IActionResult XacNhanDangKy(int? id)
        {
            int _id = id ?? 0;
            var records = (from item_ve_xe in db.VeXes
                           join item_loai_ve in db.LoaiVes on item_ve_xe.IdLoaiVe equals item_loai_ve.Id
                           join item_order in db.Orders on item_ve_xe.Id equals item_order.IdVeXe
                           join item_user in db.Users on item_order.IdUser equals item_user.Id
                           join item_xe in db.Xes on item_ve_xe.IdXe equals item_xe.Id
                           join item_loai_xe in db.LoaiXes on item_xe.IdLoaiXe equals item_loai_xe.Id
                           where item_order.Id == _id
                           select new
                           {
                               Order = item_order,
                               User = item_user,
                               Xe = item_xe,
                               VeXe = item_ve_xe,
                               LoaiVe = item_loai_ve,
                               LoaiXe = item_loai_xe
                           }
                          ).FirstOrDefault();

            // Truy cập các thông tin trong record
            ViewBag.order = records.Order;
            ViewBag.user = records.User;
            ViewBag.xe = records.Xe;
            ViewBag.vexe = records.VeXe;
            ViewBag.loaive = records.LoaiVe;
            ViewBag.loaixe = records.LoaiXe;

            return View();
        }
        public IActionResult DanhSachVeThang()
        {
            var records = (from item_ve_xe in db.VeXes
                           join item_loai_ve in db.LoaiVes on item_ve_xe.IdLoaiVe equals item_loai_ve.Id
                           join item_order in db.Orders on item_ve_xe.Id equals item_order.IdVeXe
                           join item_user in db.Users on item_order.IdUser equals item_user.Id
                           join item_xe in db.Xes on item_ve_xe.IdXe equals item_xe.Id
                           join item_loai_xe in db.LoaiXes on item_xe.IdLoaiXe equals item_loai_xe.Id
                           select new
                           {
                               Order = item_order,
                               User = item_user,
                               Xe = item_xe,
                               VeXe = item_ve_xe,
                               LoaiVe = item_loai_ve,
                               LoaiXe = item_loai_xe
                           }).ToList();
            // Truy cập các thông tin trong record
            ViewBag.listRecords = records;
            return View();
        }

        public IActionResult QlTraXe()
        {
            ViewBag.LoaiVe = 0;
            return View();
        }
        [HttpPost]
        public IActionResult QlTraXePost()
        {
            //int _loai_xe = Convert.ToInt32(Request.Form["loai_xe"]);
            string _ma_ve = Request.Form["ma_ve"].ToString().Trim();
            //string _btn_tra_xe = Request.Form["btn_tra_xe"].ToString().Trim();
            //string _bai_xe = Request.Form["bai_xe"].ToString().Trim();

            //var record_bai_xe = db.BaiXes.Where(item => item.TenBaiXe == _bai_xe).FirstOrDefault();

            ViewBag.ma_ve = _ma_ve;
            var record = (from item_ve_xe in db.VeXes
                          join item_loai_ve in db.LoaiVes on item_ve_xe.IdLoaiVe equals item_loai_ve.Id
                          join item_xe in db.Xes on item_ve_xe.IdXe equals item_xe.Id
                          join item_loai_xe in db.LoaiXes on item_xe.IdLoaiXe equals item_loai_xe.Id
                          join item_bai_xe in db.BaiXes on item_ve_xe.IdBaiXe equals item_bai_xe.Id
                          where item_ve_xe.MaVe == _ma_ve && item_ve_xe.TrangThai == "DangGuiXe"
                          select new
                          {
                              Xe = item_xe,
                              VeXe = item_ve_xe,
                              LoaiVe = item_loai_ve,
                              LoaiXe = item_loai_xe,
                              BaiXe = item_bai_xe
                          }).FirstOrDefault();

            ViewBag.Record = record;
           
            if (record != null)
            {
                ViewBag.LoaiVe = record.LoaiVe.Id;
                ViewBag.noti = "Tìm thấy vé!";
                ViewBag.thanh_toan = "Thanh toán";
                record.VeXe.NgayRa = DateTime.Now;
                db.SaveChanges();
                ViewBag.ngay_ra = record.VeXe.NgayRa;
                float tong_tien = 0;

                TimeSpan thoiGian = (DateTime)record.VeXe.NgayRa - (DateTime)record.VeXe.NgayVao;
                int soNgay = thoiGian.Days;
                if (soNgay <= 0)
                {
                    tong_tien = (float)record.LoaiVe.GiaVe;
                }
                else
                {
                    tong_tien = (float)record.LoaiVe.GiaVe * (float)soNgay;
                }
                ViewBag.tong_tien = tong_tien;

                return View("QlTraXe");
            }

            else
            {
                ViewBag.noti = "Mã vé không hợp lệ hoặc chưa được sử dụng!";
                return View("QlTraXe");
            }

        }
        public IActionResult TraXeVeThang(string? ma_ve)
        {
            string _ma_ve = ma_ve ?? "";
            var record = (from item_ve_xe in db.VeXes
                          join item_loai_ve in db.LoaiVes on item_ve_xe.IdLoaiVe equals item_loai_ve.Id
                          join item_xe in db.Xes on item_ve_xe.IdXe equals item_xe.Id
                          join item_loai_xe in db.LoaiXes on item_xe.IdLoaiXe equals item_loai_xe.Id
                          join item_bai_xe in db.BaiXes on item_ve_xe.IdBaiXe equals item_bai_xe.Id
                          join item_order in db.Orders on item_ve_xe.Id equals item_order.IdVeXe
                          where item_ve_xe.MaVe == _ma_ve
                          select new
                          {
                              Xe = item_xe,
                              VeXe = item_ve_xe,
                              LoaiVe = item_loai_ve,
                              LoaiXe = item_loai_xe,
                              Order = item_order,
                              BaiXe = item_bai_xe
                          }).FirstOrDefault();
            if (record != null)
            {
                record.VeXe.TrangThai = "DaTraXe";
                record.Order.NgayVao = record.VeXe.NgayVao;
                record.Order.NgayRa = record.VeXe.NgayRa;
                record.VeXe.NgayRa = null;
                record.VeXe.NgayVao = null;
                record.VeXe.IdBaiXe = null;
                if (record.Xe.IdLoaiXe == 1 || record.Xe.IdLoaiXe == 2)
                {
                    record.BaiXe.SoChoTrongXeDapMay++;
                }
                else if(record.Xe.IdLoaiXe == 3)
                {
                    record.BaiXe.SoChoTrongOTo++;
                }
                db.SaveChanges();
                ViewBag.NotiSuccess = "Trả xe thành công";
            }
            return View("QlTraXe");
        }
        public IActionResult ThanhToanTraXe(string? ma_ve, float? tong_tien)
        {
            string _ma_ve = ma_ve ?? "";
            float _tong_tien = tong_tien ?? 0;
            var record = (from item_ve_xe in db.VeXes
                          join item_loai_ve in db.LoaiVes on item_ve_xe.IdLoaiVe equals item_loai_ve.Id
                          join item_xe in db.Xes on item_ve_xe.IdXe equals item_xe.Id
                          join item_loai_xe in db.LoaiXes on item_xe.IdLoaiXe equals item_loai_xe.Id
                          join item_bai_xe in db.BaiXes on item_ve_xe.IdBaiXe equals item_bai_xe.Id

                          where item_ve_xe.MaVe == _ma_ve
                          select new
                          {
                              Xe = item_xe,
                              VeXe = item_ve_xe,
                              LoaiVe = item_loai_ve,
                              LoaiXe = item_loai_xe,
                              BaiXe = item_bai_xe
                          }).FirstOrDefault();
            //record.VeXe.TrangThai = "DaTraXe";
            if (record != null)
            {
                db.VeXes.Remove(record.VeXe);
                db.Xes.Remove(record.Xe);
                Order order = new Order();
                order.BienSo = record.Xe.BienSo;
                order.LoaiVe = record.LoaiVe.TenLoaiVe;
                order.LoaiXe = record.LoaiXe.TenLoaiXe;
                order.TenXe = record.Xe.TenXe;
                order.MauSac = record.Xe.MauSac;
                order.NgayVao = record.VeXe.NgayVao;
                order.NgayRa = record.VeXe.NgayRa;
                order.IdVeXe = record.VeXe.Id;
                order.TongTien = _tong_tien;
                db.Orders.Add(order);

                if (record.Xe.IdLoaiXe == 1 || record.Xe.IdLoaiXe == 2)
                {
                    record.BaiXe.SoChoTrongXeDapMay++;
                }
                else if(record.Xe.IdLoaiXe == 3)
                {
                    record.BaiXe.SoChoTrongOTo++;
                }
                db.SaveChanges();
                ViewBag.NotiSuccess = "Trả xe thành công";
            }
            return View("QlTraXe");
        }
        public IActionResult ListXeOrder()
        {
            var list_order = db.Orders.ToList();
            ViewBag.list_order = list_order;
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}