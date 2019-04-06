using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BlogSitesiMVC_Proje_YaseminGoktas.Models;
namespace BlogSitesiMVC_Proje_YaseminGoktas.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        YaseminBlogSitesiMVCEntities db = new YaseminBlogSitesiMVCEntities();
        public ActionResult Index()
        {
            return View();
        }

        // partial view oluşturdum:
        public ActionResult CategoryWidgetGetir()
        {
            return View(db.Kategori.ToList());
        }

        // gönderileri getirmesi için partial view:
        public ActionResult PostWidgetGetir()
        {
            // En son eklenen 5 makaleyi getiriyorum :
            ViewBag.Yeniler = db.Makale.OrderByDescending(x => x.YayinTarihi).Take(5);
            // En Çok görüntülenen ( popüler ) 5 makaleyi getiriyorum :
            ViewBag.Populer = db.Makale.OrderByDescending(x => x.Goruntulenme).Take(5);
            return View();
        }

        // etiketleri getirmek için partial view oluşturdum :
        public ActionResult EtiketlerWidgetGetir()
        {
            var tags = db.Etiket.ToList();
            return View(tags);
        } 

        // makalelerin sayfanın ortasında listelenmesi için :
        public ActionResult TumMakalelerGetir()
        {
            var makaleler = db.Makale.ToList();
            return View("MakaleListele", makaleler);
        }
    }
}