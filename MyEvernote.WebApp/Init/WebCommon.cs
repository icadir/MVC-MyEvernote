using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyEvernote.Entities;
using MyEvernoteCommon;

namespace MyEvernote.WebApp.Init
{
    public class WebCommon : ICommon
    {
        public string GetCurrentUsername()
        {
            if (HttpContext.Current.Session["login"] != null)
            {
                EvernoteUser user = HttpContext.Current.Session["login"] as EvernoteUser;
                return user.Username;
            }

            return "system";
        }
    }
}