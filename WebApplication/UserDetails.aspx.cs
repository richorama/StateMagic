using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using DatabaseTypes;
using DataAccess;

namespace WebApplication
{
    public partial class UserDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (null == Session["Credentials"])
            {
                Response.Redirect("SignIn.aspx");
            }

            DataAccess.DatabaseWrapper.Init();
            CredentialData cd = Session["Credentials"] as CredentialData;
            this.AvailableModels.DataSource = cd.Models;

            AccountBalanceLabel.Text = cd.TransactionBalance.ToString();
        }
    }
}
