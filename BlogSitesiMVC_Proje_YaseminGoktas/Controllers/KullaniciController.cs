using BlogSitesiMVC_Proje_YaseminGoktas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.SqlClient;

namespace BlogSitesiMVC_Proje_YaseminGoktas.Controllers
{
    public class KullaniciController : Controller
    {
        YaseminBlogSitesiMVCEntities db = new YaseminBlogSitesiMVCEntities();
        // GET: Kullanici
        public ActionResult GirisYap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GirisYap(string KullaniciAdi, string Parola)
        {

            if (Membership.ValidateUser(KullaniciAdi,Parola))
            {
                FormsAuthentication.RedirectFromLoginPage(KullaniciAdi, true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Mesaj = "Kullanıcı adı veya parola yanlış ! ";
                return View();
            }
        }

        public ActionResult KayitOl()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KayitOl(Kullanici kullanici, HttpPostedFileBase Resim , string Parola)
        {
            MembershipUser user = Membership.CreateUser(kullanici.Nick, Parola, kullanici.Mail);

            kullanici.Id = (Guid)user.ProviderUserKey;
            Session["Kullanici"] = kullanici;
            kullanici.ResimID = YonetimController.ResimKaydet(Resim, HttpContext);
            // HttpContext bütün web sitesini yöneten sınıftır.
            kullanici.KayitTarihi = DateTime.Now;
            db.Kullanici.Add(kullanici);
            db.SaveChanges();

            FormsAuthentication.RedirectFromLoginPage(kullanici.Nick, true);

            Session["Kullanici"] = kullanici;

            return RedirectToAction("Index","Home");
        }
    }
}