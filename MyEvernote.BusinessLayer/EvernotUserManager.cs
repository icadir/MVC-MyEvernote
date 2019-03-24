using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.DataAccessLayer.EntityFramework;
using MyEvernote.Entities;
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
            var layerResult = new BusinessLayerResult<EvernoteUser>();

            if (user != null)
            {
                if (user.Username == data.Username)
                {
                    layerResult.Erros.Add("Kullanıcı Adi Kayıtlı");
                }

                if (user.Email == data.Email)
                {
                    layerResult.Erros.Add("E-posta adresi kayıtlı");
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
                    layerResult.Result = repo_user.Find(x => x.Email == data.Email && x.Username == data.Username);

                    //TODO : aktivasyon maili atılacak
                    //layerresult.activaGuid

                }
            }

            return layerResult;
        }
    }
}
