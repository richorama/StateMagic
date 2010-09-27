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
    public partial class Designer : System.Web.UI.Page
    {
        List<string> paramsList = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request["mode"] == "start")
            { 
                var newcd = ModelServices.LogIn(Request["username"], new Guid(Request["apikey"]));
                if (newcd != null)
                {
                    Session["Credentials"] = newcd;
                    Response.Redirect(string.Format("Designer.aspx?ModelId={0}", Request["ModelId"]));
                }
                else
                {
                    Response.Redirect("default.htm");
                }
            }

            CredentialData cd = Session["Credentials"] as CredentialData;
            if (null != cd)
            {
                // we are not logged in
                AddParam("username", cd.Username);
                AddParam("apikey", cd.ApiKey.ToString());
                this.SignInLink.Visible = false;
            }
            else
            {
                this.UserDetailsLink.Visible = false;
                this.SignOutLink.Visible = false;
                if (null == Session["ApiKey"] as string)
                {
                    DatabaseWrapper.Init();
                    Guid key = Guid.NewGuid();
                    ApiKey apiKey = new ApiKey();
                    apiKey.APIKey = key;
                    apiKey.DateCreated = DateTime.Now;
                    apiKey.Save();
                    Session["ApiKey"] = key.ToString();
                    // TODO: save the apikey to the database
                }
                AddParam("apikey", Session["ApiKey"] as string);
            }

            if (null != Request["ModelId"])
            {
                AddParam("modelid", Request["ModelId"]);
            }

            this.initParams.Controls.Add(new LiteralControl(string.Format(@"<param name=""initParams"" value=""{0}"" />", string.Join(",", paramsList.ToArray()))));
        }

        private void AddParam(string name, string value)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(value)) throw new ArgumentNullException("value");

            paramsList.Add(string.Format("{0}={1}", name, value));
        }
    }
}
 