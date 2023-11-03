using Microsoft.AspNetCore.Mvc;
using QLBaiGuiXe.Models;
using System.Diagnostics;

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
        public IActionResult GuiXe()
        {
            int _loai_xe = Convert.ToInt32(Request.Form["loai_xe"]);
            string _bai_xe = Request.Form["bai_xe"].ToString().Trim();
            ViewBag.loai_xe = _loai_xe;
        
            return View("QlGuiXe");
        }
        [HttpPost]
        public IActionResult GuiXePost()
        {
            string _loai_xe = Request.Form["loai_xe"].ToString().Trim();
            string _ten_xe = Request.Form["ten_xe"].ToString().Trim();
            string _dong_xe = Request.Form["dong_xe"].ToString().Trim();
            string _bien_so = Request.Form["bien_so"].ToString().Trim();
            string _mau_xe = Request.Form["mau_xe"].ToString().Trim();
            DateTime _ngay_vao = Convert.ToDateTime(Request.Form["ngay_vao"]);
           

            //tao mot ban ghi
            Xe record = new Xe();
            VeXe veXe = new VeXe();
            //---
            //---
            //record.NameProduct = _Name;
            //record.DescriptionProduct = _Description;
            //record.ContentProduct = _Content;
            //record.HotProduct = _Hot;
            //record.PriceProduct = _Price;
            //record.DiscountProduct = _Discount;
            ////lay thong tin cua file
            //string _Photo = "";
            //try
            //{
            //    _Photo = Request.Form.Files[0].FileName;
            //}
            //catch {; }
            //if (!string.IsNullOrEmpty(_Photo))
            //{
            //    //upload anh moi
            //    var timestamp = DateTime.Now.ToFileTime();//chuyen thoi gian ra thanh mot so nguyen
            //    _Photo = timestamp + "_" + _Photo;//noi chuoi thoi gian vao bien _Photo
            //                                      //lay duong dan cua file
            //                                      //Path.Combine(duongdan1,duongdan2...) noi duongdan1 va duongdan2... thanh mot duong dan
            //    string _Path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/GuiXe", _Photo);
            //    //upload file
            //    using (var stream = new FileStream(_Path, FileMode.Create))
            //    {
            //        Request.Form.Files[0].CopyTo(stream);
            //    }
            //    //cap nhat lai duong dan anh
            //    record.PhotoProduct = _Photo;
            //}
            ////them ban ghi vao table
            //db.Products.Add(record);
            ////cap nhat csdl
            //db.SaveChanges();
            ////lay id cua moi insert vao trong table Products
            //int InsertedId = record.IdProduct;
            ////---
            ////---
            ////insert du lieu moi vao table CategoriesProducts
            //foreach (var item in SelectedCategories)
            //{
            //    CategoryProduct new_record = new CategoryProduct();
            //    new_record.IdCategory = Convert.ToInt32(item);
            //    new_record.IdProduct = InsertedId;
            //    db.CategoryProducts.Add(new_record);
            //    db.SaveChanges();
            //}
            ////---
            ////insert du lieu moi vao table TagsProducts
            //foreach (var item in SelectedTags)
            //{
            //    TagProduct new_record = new TagProduct();
            //    new_record.IdTag = Convert.ToInt32(item);
            //    new_record.IdProduct = InsertedId;
            //    db.TagProducts.Add(new_record);
            //    db.SaveChanges();
            //}
            //---
            return RedirectToAction("Index");
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