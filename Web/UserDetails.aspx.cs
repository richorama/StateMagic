using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StateMagic.Common;
using StateMagic.DatabaseTypes;
using StateMagic.DataAccess;

namespace StateMagic.Web
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

            // replenish the cd cache
            CredentialData cd = Session["Credentials"] as CredentialData;

            cd = CredentialData.Find(cd.CredentialDataID);

            this.AvailableModels.DataSource = cd.Models;
            this.AvailableModels.DataBind();
            this.namelabel.Text = cd.Username;
            this.stateDiagramCountLabel.Text = cd.Models.Count.ToString();
            this.APIKey1.Text = cd.ApiKey.ToString();

            AccountBalanceLabel.Text = cd.TransactionBalance.ToString();
        }
    }
}
