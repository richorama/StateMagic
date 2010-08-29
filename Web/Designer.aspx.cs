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

            CredentialData cd = Session["Credentials"] as CredentialData;
            if (null != cd)
            {
                AddParam("username", cd.Username);
                AddParam("apikey", cd.ApiKey.ToString());
            }
            else
            {
                if (null == Session["ApiKey"] as string)
                {
                    Session["ApiKey"] = Guid.NewGuid().ToString();
                    // TODO: save the apikey to the database
                }
                AddParam("apikey", Session["ApiKey"] as string);

            }

            this.initParams.Controls.Add(new LiteralControl(string.Format(@"<param name=""windowless"" value=""{0}"" />", string.Join(",", paramsList.ToArray()))));
        }

        private void AddParam(string name, string value)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("value");

            paramsList.Add(string.Format("{0}={1}", name, value));
        }
    }
}
 