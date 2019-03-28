using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyEvernote.BusinessLayer;
using MyEvernote.Entities;
using MyEvernote.Entities.Messages;
using MyEvernote.Entities.ValueObjects;
using MyEvernote.WebApp.ViewModels.BilgilendirmeSayfalari;

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

        public ActionResult ShowProfile()
        {
            EvernoteUser currentUser = Session["login"] as EvernoteUser;

            EvernotUserManager eum = new EvernotUserManager();

            BusinessLayerResult<EvernoteUser> res = eum.GetUserById(currentUser.Id);

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
            EvernoteUser currentUser = Session["login"] as EvernoteUser;

            EvernotUserManager eum = new EvernotUserManager();

            BusinessLayerResult<EvernoteUser> res = eum.GetUserById(currentUser.Id);

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

                Session["login"] = res.Result; // profil güncellendigi içinsession güncellendi.
                return RedirectToAction("ShowProfile");
            }

            return View(model);
        }

        public ActionResult DeleteProfile()
        {
            EvernoteUser currentUser = Session["loing"] as EvernoteUser;

            EvernotUserManager eum = new EvernotUserManager();
            BusinessLayerResult<EvernoteUser> res = eum.RemoveUserById(currentUser.Id);

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
                var eum = new EvernotUserManager();
                var res = eum.LoginUSer(model);

                if (res.Erros.Count > 0)
                {

                    if (res.Erros.Find(x => x.Code == ErrorMessageCode.UserIsNotActive) != null)
                    {

                        ViewBag.SetLink = "E-posta Gönder";
                    }
                    res.Erros.ForEach(x => ModelState.AddModelError("", x.Message));


                    return View(model);
                }

                Session["login"] = res.Result;  // session akullanıcı bilgi saklama
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
                EvernotUserManager eum = new EvernotUserManager();
                var res = eum.RegisterUser(model);

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
            var eum = new EvernotUserManager();
            var res = eum.ActivateUser(id);

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

    }
}