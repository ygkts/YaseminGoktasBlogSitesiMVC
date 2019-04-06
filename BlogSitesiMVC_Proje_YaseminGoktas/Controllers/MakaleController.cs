using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BlogSitesiMVC_Proje_YaseminGoktas.Models;
namespace BlogSitesiMVC_Proje_YaseminGoktas.Controllers
{
    public class MakaleController : Controller
    {
        // GET: Makale
        YaseminBlogSitesiMVCEntities db = new YaseminBlogSitesiMVCEntities();
        public ActionResult Index(int id)
        {
            return View(id);
        }

        public ActionResult TariheGoreListe(int yil, int ay)
        {
            ViewBag.yil = yil;
            ViewBag.ay = ay;
            return View(new { yil = yil, ay = ay });
        }
        public ActionResult MakaleListele(int yil = 0, int ay = 0)
        {
            var data = db.Makale.Where(x => x.YayinTarihi.Year == yil && x.YayinTarihi.Month == ay);
            return View("MakaleListele", data);
        }

        public ActionResult Detay(int id)
        {
            ViewBag.Kullanici = Session["Kullanici"];  // şuanki kullaniciyi almak
            
            Makale mak = db.Makale.FirstOrDefault(x => x.Id == id);

            return View(mak);
        }

        [HttpPost]
        public ActionResult YorumYaz(Yorum yorum)
        {
            yorum.EklenmeTarihi = DateTime.Now;
            yorum.Baslik = "";
            yorum.Aktif = false;
            db.Yorum.Add(yorum);
            db.SaveChanges();
            return RedirectToAction("Detay", new {id=yorum.MakaleID });
        }
    }
}