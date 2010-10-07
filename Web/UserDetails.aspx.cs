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
        public string Username 
        {
            get
            {
                return this.Credentials.Username;
            }
        }

        public CredentialData Credentials { get; private set;}

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

            this.Credentials = cd;
            this.AvailableModels.DataSource = cd.Models;
            this.AvailableModels.DataBind();
            if (cd.Models.Count > 0)
            {
                this.modelIdSnippet.Text = cd.Models[0].ModelDataID.ToString();
            }
            else
            {
                this.modelIdSnippet.Text = "1";
            }


            AccountBalanceLabel.Text = cd.TransactionBalance.ToString();
        }
    }
}
