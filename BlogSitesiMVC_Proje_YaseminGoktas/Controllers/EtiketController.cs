using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BlogSitesiMVC_Proje_YaseminGoktas.Models;

namespace BlogSitesiMVC_Proje_YaseminGoktas.Controllers
{
    public class EtiketController : Controller
    {
        // GET: Etiket
        YaseminBlogSitesiMVCEntities db = new YaseminBlogSitesiMVCEntities();
        public ActionResult Index(int id )
        {
            return View(id);
        }
        public ActionResult MakaleListele(int id)
        {
            var data = db.Makale.Where(x => x.MakaleEtiket.Any(me => me.EtiketID == id));
            return View("MakaleListele", data);
        }
    }
}