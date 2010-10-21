using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StateMagic.Common;
using StateMagic.DatabaseTypes;

namespace StateMagic.Web
{
    public partial class UserDetails : System.Web.UI.Page
    {

        public int LastModelId
        {
            get
            {
                return this.Credentials.Models.Count == 0 ? 0 : this.Credentials.Models.Last().ModelDataID;
            }
        }

        public CredentialData Credentials { get; private set;}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (null == Session["Credentials"] as CredentialData)
            {
                Response.Redirect("SignIn.aspx");
            }


            // replenish the cd cache
            CredentialData cd = Session["Credentials"] as CredentialData;

            this.Credentials = CredentialData.Find(cd.CredentialDataID);

            this.AvailableModels.DataSource = this.Credentials.Models;
            this.AvailableModels.DataBind();

        }
    }
}
