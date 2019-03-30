﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyEvernote.Entities;
using MyEvernote.WebApp.Models;
using MyEvernoteCommon;

namespace MyEvernote.WebApp.Init
{
    public class WebCommon : ICommon
    {
        public string GetCurrentUsername()
        {
            EvernoteUser user = CurrentSession.User;
            if (user != null)
                return user.Username;
            else
                return "system";
        }
    }
}