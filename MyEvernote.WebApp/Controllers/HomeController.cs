using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyEvernote.BusinessLayer;
using MyEvernote.Entities;
using MyEvernote.WebApp.ViewModels;

namespace MyEvernote.WebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            // farklı kontrollerdan gelen view talebi karşılama 
            //if (TempData["mm"] != null)
            //{
            //    return View(TempData["mm"] as List<Note>);
            //}
            NoteManager nm = new NoteManager();

            return View(nm.GetAllNote().OrderByDescending(x => x.ModifiedOn).ToList());
            //return View(nm.GetAllNoteQueryable().OrderByDescending(x=>x.ModifiedOn).ToList());
        }

        public ActionResult ByCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cm = new CategoryManager();
            var cat = cm.GetCategoryById(id.Value);
            if (cat == null)
            {
                return HttpNotFound();
            }

            return View("Index", cat.Notes.OrderByDescending(x => x.ModifiedOn).ToList());
        }

        public ActionResult MostLiked()
        {
            NoteManager nm = new NoteManager();
            return View("Index", nm.GetAllNote().OrderByDescending(x => x.LikeCount).ToList());
        }

        public ActionResult About()
        {
            return View();

        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            //Giriş kontrolü ve yönlendirme 
            //session akullanıcı bilgi saklama
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {

            // kuallnıcı username kontroşü
            // kllancı e-posta kontrolü
            // kayit işlemi 
            // aktivasyon e-postası gönderimi
            if (ModelState.IsValid)
            {

                // ileride mobil vs gelebilir bu kodları mobil vsde kullanamayız bu yüzden bunları BLL'e taşımamız gerekiyor.
                //if (model.Username == "aaa")
                //{
                //    ModelState.AddModelError("", "Kullanıcı Adı Kullanılıyor");

                //}
                //if (model.Email == "aaa@aa.com")
                //{
                //    ModelState.AddModelError("", "E-posta adresi kullanılıyor");

                //}

                //foreach (var item in ModelState)
                //{
                //    if (item.Value.Errors.Count > 0)
                //    {
                //        return View(model);
                //    }
                //}

                return RedirectToAction("RegisterOk");
            }
           
            return View(model);
        }

        public ActionResult RegisterOk()
        {
            return View();
        }

        public ActionResult UserActivate(Guid activate_id)
        {
            // Kullanıcı aktivasyonu sağlanacak.
            return View();
        }
        public ActionResult Logout()
        {
            return View();
        }

    }
}