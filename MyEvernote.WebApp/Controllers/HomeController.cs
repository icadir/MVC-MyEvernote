using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyEvernote.BusinessLayer;
using MyEvernote.BusinessLayer.Result;
using MyEvernote.Entities;
using MyEvernote.Entities.Messages;
using MyEvernote.Entities.ValueObjects;
using MyEvernote.WebApp.Filters;
using MyEvernote.WebApp.Models;
using MyEvernote.WebApp.ViewModels.BilgilendirmeSayfalari;

namespace MyEvernote.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private NoteManager noteManager = new NoteManager();
        private CategoryManager categoryManager = new CategoryManager();
        private EvernotUserManager evernoteUserManager = new EvernotUserManager();
        // GET: Home
        public ActionResult Index()
        {
            // farklı kontrollerdan gelen view talebi karşılama 
            //if (TempData["mm"] != null)
            //{
            //    return View(TempData["mm"] as List<Note>);
            //}

            return View(noteManager.ListQueryable().Where(x => x.IsDraft == false).OrderByDescending(x => x.ModifiedOn).ToList());
            //return View(nm.GetAllNoteQueryable().OrderByDescending(x=>x.ModifiedOn).ToList());
        }

        public ActionResult ByCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //var cat = categoryManager.Find(x => x.Id == id.Value);
            //List<Note> Notes = cat.Notes.Where(x => x.IsDraft == false).OrderByDescending(x => x.ModifiedOn).ToList()
            //if (cat == null)
            //{
            //    return HttpNotFound();
            //}
            List<Note> notes = noteManager.ListQueryable()
                .Where(x => x.IsDraft == false && x.CategoryId == id)
                .OrderByDescending(X => X.ModifiedOn).ToList();
            return View("Index", notes);
        }

        public ActionResult MostLiked()
        {

            return View("Index", noteManager.ListQueryable().OrderByDescending(x => x.LikeCount).ToList());
        }

        public ActionResult About()
        {
            return View();

        }
        [Auth]
        public ActionResult ShowProfile()
        {


            BusinessLayerResult<EvernoteUser> res = evernoteUserManager.GetUserById(CurrentSession.User.Id);

            if (res.Erros.Count > 0)
            {
                ErrorViewModel errornotifyObj = new ErrorViewModel()
                {
                    Tittle = "Hata Oluştu",
                    Items = res.Erros,
                };

                return View("Error", errornotifyObj);
            }

            return View(res.Result);
        }

        public ActionResult EditProfile()
        {


            BusinessLayerResult<EvernoteUser> res = evernoteUserManager.GetUserById(CurrentSession.User.Id);

            if (res.Erros.Count > 0)
            {
                ErrorViewModel errornotifyObj = new ErrorViewModel()
                {
                    Tittle = "Hata Oluştu",
                    Items = res.Erros,
                };

                return View("Error", errornotifyObj);
            }

            return View(res.Result);
        }
        [Auth]
        [HttpPost]
        public ActionResult EditProfile(EvernoteUser model, HttpPostedFileBase ProfileImage)
        {
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                if (ProfileImage != null && (ProfileImage.ContentType == "image/jpeg" ||
                                             ProfileImage.ContentType == "image/jpg" ||
                                             ProfileImage.ContentType == "image/png"))
                {
                    string filename = $"user_{model.Id}.{ProfileImage.ContentType.Split('/')[1]}";
                    ProfileImage.SaveAs(Server.MapPath($"~/images/{filename}"));
                    model.ProfileImageFilename = filename;
                }

                EvernotUserManager eum = new EvernotUserManager();
                BusinessLayerResult<EvernoteUser> res = eum.UpdateProfile(model);
                if (res.Erros.Count > 0)
                {
                    ErrorViewModel errorNotifyObj = new ErrorViewModel()
                    {
                        Items = res.Erros,
                        Tittle = "Profil Güncellenemedi",
                        RedirectingUrl = "/Home/EditProfile"
                    };
                    return View("Error", errorNotifyObj);
                }
                // profil güncellendigi içinsession güncellendi.
                CurrentSession.Set<EvernoteUser>("login", res.Result);
                return RedirectToAction("ShowProfile");
            }

            return View(model);
        }
        [Auth]
        public ActionResult DeleteProfile()
        {


            BusinessLayerResult<EvernoteUser> res = evernoteUserManager.RemoveUserById(CurrentSession.User.Id);

            if (res.Erros.Count > 0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Items = res.Erros,
                    Tittle = "Profile Silinemedi",
                    RedirectingUrl = "/Home/ShowProfile"
                };
                return View("Error", errorNotifyObj);
            }
            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                var res = evernoteUserManager.LoginUSer(model);

                if (res.Erros.Count > 0)
                {

                    if (res.Erros.Find(x => x.Code == ErrorMessageCode.UserIsNotActive) != null)
                    {

                        ViewBag.SetLink = "E-posta Gönder";
                    }
                    res.Erros.ForEach(x => ModelState.AddModelError("", x.Message));


                    return View(model);
                }

                CurrentSession.Set<EvernoteUser>("login", res.Result); // session akullanıcı bilgi saklama
                return RedirectToAction("Index");  //yönlendirme

            }

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

                var res = evernoteUserManager.RegisterUser(model);

                if (res.Erros.Count > 0)
                {
                    res.Erros.ForEach(x => ModelState.AddModelError("", x.Message));


                    return View(model);
                }

                //EvernoteUser user = null;
                //try
                //{
                //    user = eum.RegisterUser(model);
                //}
                //catch (Exception ex)
                //{
                //    ModelState.AddModelError("", ex.Message);

                //}


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

                //if (user == null)
                //{
                //    return View(model);
                //}
                OkViewModel notifyObj = new OkViewModel()
                {
                    Tittle = "Kayıt Başarılı",
                    RedirectingUrl = "/Home/Login",

                };
                notifyObj.Items.Add("Lütfen e-posta adresinize gönderdiğimiz aktivasyon link'ine tıklayarak hesabınızı aktive ediniz. Hesabınız aktive etmeden not ekleyemez ve beğenme yapamazsınız..");

                return View("Ok", notifyObj);
            }

            return View(model);
        }

        public ActionResult UserActivate(Guid id)
        {

            var res = evernoteUserManager.ActivateUser(id);

            if (res.Erros.Count > 0)
            {
                ErrorViewModel errornotifyObj = new ErrorViewModel()
                {
                    Tittle = "Geçersiz İşlem",
                    Items = res.Erros,
                };

                return View("Error", errornotifyObj);
            }

            OkViewModel okNotifyObj = new OkViewModel()
            {
                Tittle = "Hesap Aktifleştirildi.",
                RedirectingUrl = "/Home/Login"
            };

            okNotifyObj.Items.Add("Hesabınız Aktifleştirildi. Artık not Paylaşabilir ve beğenme yapabilirsiniz.");

            return View("Ok", okNotifyObj);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult AccessDenied()
        {
            return View();
        }


    }
}