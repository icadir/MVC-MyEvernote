using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.DataAccessLayer.EntityFramework;
using MyEvernote.Entities;
using MyEvernote.Entities.Messages;
using MyEvernote.Entities.ValueObjects;

namespace MyEvernote.BusinessLayer
{
    public class EvernotUserManager
    {
        private Repository<EvernoteUser> repo_user = new Repository<EvernoteUser>();

        public BusinessLayerResult<EvernoteUser> RegisterUser(RegisterViewModel data)
        {
            // kuallnıcı username kontroşü
            // kllancı e-posta kontrolü
            // kayit işlemi 
            // aktivasyon e-postası gönderimi
            var user = repo_user.Find(x => x.Username == data.Username || x.Email == data.Email);
            var res = new BusinessLayerResult<EvernoteUser>();

            if (user != null)
            {
                if (user.Username == data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı Adı Kayıtlı");
                }

                if (user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kullanılıyor.");
                }
            }
            else
            {
                var dbResult = repo_user.Insert(new EvernoteUser()
                {
                    Username = data.Username,
                    Email = data.Email,
                    Password = data.Password,
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = false,
                    IsAdmin = false,

                });

                if (dbResult > 0)
                {
                    res.Result = repo_user.Find(x => x.Email == data.Email && x.Username == data.Username);

                    //TODO : aktivasyon maili atılacak
                    //layerresult.activaGuid

                }
            }

            return res;
        }

        public BusinessLayerResult<EvernoteUser> LoginUSer(LoginViewModel data)
        {
            // Giriş kontrolü 
            // Hesap aktive edilmiş mi ?
            var res = new BusinessLayerResult<EvernoteUser>();
            res.Result = repo_user.Find(x => x.Username == data.Username && x.Email == data.Password);


            if (res.Result != null)
            {
                if (!res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserIsNotActive, "Kullanıcı Aktifleştirilmemiştir.");
                    res.AddError(ErrorMessageCode.CheckYourEmail, "Lüsfen eposta adresinizi kontrol ediliz.");

                }

            }
            else
            {
                res.AddError(ErrorMessageCode.UsernameOrPassWrong, "Kullanıcı adı yada sifre uyuşmuyor.");
            }

            return res;
        }
    }
}
