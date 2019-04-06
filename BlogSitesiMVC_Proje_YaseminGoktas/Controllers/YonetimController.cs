using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Configuration;
using System.IO;
using System.Drawing;

namespace BlogSitesiMVC_Proje_YaseminGoktas.Controllers
{
    using Models;
    public class YonetimController : Controller
    {
        YaseminBlogSitesiMVCEntities db = new YaseminBlogSitesiMVCEntities();
        // GET: Yonetim
        public ActionResult Index()
        {
            ViewBag.Tip = 1;
            return View();
        }

        public  ActionResult MakaleYaz()
        {
            ViewBag.Tip = 1;
            ViewBag.MakaleTipler = db.MakaleTip.ToList();
            ViewBag.Kullanicilar = db.Kullanici.ToList();
            ViewBag.KapakResimleri = db.MultiMedia.ToList();
            var kateList = (from k in db.Kategori select k).ToList();
            ViewBag.Kategoriler = new SelectList(kateList, "Id", "Adi");
            return View();
        }
        [HttpPost]
        public ActionResult MakaleYaz( Makale makale , HttpPostedFileBase Resim, String etiketler)
        {
            if (makale != null)
            {
                // membership kullandım : veri tabanına asp.net membership ini ekledim
                // developer command prompt a aspnet_regsql yazdım ve adımları takip etttim
                // asp.net tarafında kaydolmamış birini kullanıcı olarak kaydedemek için : aspnet users tablsoun pk'ini kulllancici tablosunun pk'sine bağlıyorum
                // :\Windows\Microsoft.NET\Framework64\v4.0.30319\Config\machine.config dosyasından membership, profie, rolmanager taglarini alıp projemin webconfig dosyasında system web içerisine yapıştırıyorum ve isteğime göre özellikleri değiştiriyorum.
                // provider ın başına <clear/> ekliyorum
                Kullanici aktif = Session["Kullanici"] as Kullanici;
                makale.YayinTarihi = DateTime.Now;
                makale.MakaleTipID = 1;
                makale.YazarID = aktif.Id;
                makale.KapakResimID = ResimKaydet(Resim,HttpContext );

                db.Makale.Add(makale);
                db.SaveChanges();

                string[] etikets = etiketler.Split(',');
                foreach (string etiket in etikets)
                {
                    MakaleEtiket etk = db.MakaleEtiket.FirstOrDefault(x => x.Etiket.Adi.ToLower() == etiket.ToLower().Trim()); 
                    if (etk != null)
                    {
                        // Etiket var
                        makale.MakaleEtiket.Add(etk);
                        db.SaveChanges();
                    }
                    else
                    {
                        // Etiket yok
                        etk = new MakaleEtiket();
                        etk.Etiket.Adi = etiket;
                        db.Etiket.Add(etk.Etiket);
                        db.SaveChanges();

                        makale.MakaleEtiket.Add(etk);
                        db.SaveChanges();

                    }
                }

            }
            return View();
        }

        // ResimKaydet metodunu KullaniciController'da da kullanacağım için static tanımladım
        public static int ResimKaydet(HttpPostedFileBase Resim, HttpContextBase ctx)
        {
            YaseminBlogSitesiMVCEntities db = new YaseminBlogSitesiMVCEntities();
           

            int kucukWidth =  Convert.ToInt32(ConfigurationManager.AppSettings["kw"]);
            int kucukHeight = Convert.ToInt32(ConfigurationManager.AppSettings["kh"]);
            int ortaWidth = Convert.ToInt32(ConfigurationManager.AppSettings["ow"]);
            int ortaHeight = Convert.ToInt32(ConfigurationManager.AppSettings["oh"]);
            int buyukWidth = Convert.ToInt32(ConfigurationManager.AppSettings["bw"]);
            int buyukHeight = Convert.ToInt32(ConfigurationManager.AppSettings["bh"]);

            // resimlerin otomatik isimlendirmesi :
            string newName = Path.GetFileNameWithoutExtension(Resim.FileName) + "-" + Guid.NewGuid() + Path.GetExtension(Resim.FileName);

            // FromStream : resmi, boyutunu sabit olmadan çekmek için kullanılır. Bir resmi istediğimiz boyutta çekerizs.
            Image orjRes = Image.FromStream(Resim.InputStream);

            Bitmap kucukRes = new Bitmap(orjRes, kucukWidth, kucukHeight);
            Bitmap ortaRes = new Bitmap(orjRes, ortaWidth, ortaHeight);
            Bitmap buyukRes = new Bitmap(orjRes, buyukWidth, buyukHeight);
            // Bitmap buyukRes = new Bitmap(orjRes); --> orijinal hali kalsın istersek

            kucukRes.Save(ctx.Server.MapPath("~/Content/Resimler/Kucuk/" + newName));
            ortaRes.Save(ctx.Server.MapPath("~/Content/Resimler/Orta/" + newName));
            buyukRes.Save(ctx.Server.MapPath("~/Content/Resimler/Buyuk" + newName));

            Kullanici k = (Kullanici)ctx.Session["Kullanici"];

            MultiMedia dbRes = new MultiMedia();

            dbRes.Adi = Resim.FileName;
            dbRes.BuyukResimYol = "/Content/Resimler/Buyuk" + newName;
            dbRes.OrtaResimYol = "/Content/Resimler/Orta" + newName;
            dbRes.KucukResimYol = "/Content/Resimler/Kucuk" + newName;
            dbRes.EklenmeTarihi = DateTime.Now;
            dbRes.EkleyenID = k.Id;

            db.MultiMedia.Add(dbRes);
            db.SaveChanges();

            return dbRes.Id;

        }
    }
}