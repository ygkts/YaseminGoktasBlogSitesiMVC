using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BlogSitesiMVC_Proje_YaseminGoktas.Models;
namespace BlogSitesiMVC_Proje_YaseminGoktas.Controllers
{
    public class YazarController : Controller
    {
        // GET: Yazar
        YaseminBlogSitesiMVCEntities db = new YaseminBlogSitesiMVCEntities();
        public ActionResult Index(Guid id)
        {
            return View(id);
        }

        public ActionResult MakaleListele(Guid id) // Guid = uniqidentifier demek.
        {
            var data = db.Makale.Where(x => x.YazarID == id);
            return View("MakaleListele", data);
        }
    }
}