﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Config;
using StateMagic.DatabaseTypes;

namespace StateMagic.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            ActiveRecordStarter.Initialize(typeof(CredentialData).Assembly, ActiveRecordSectionHandler.Instance);

            // If you want to let ActiveRecord create the schema for you:
            ActiveRecordStarter.CreateSchema();

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}