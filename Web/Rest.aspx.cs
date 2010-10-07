using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StateMagic.Web
{
    public partial class Rest : System.Web.UI.Page
    {
        public StateMagic.DatabaseTypes.CredentialData Credentials { get; private set; }
        public int ModelId { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {

            string username = Request["username"];
            Guid apiKey = new Guid(Request["apikey"]);
            int modelId = 0;
            int.TryParse(Request["modelid"], out modelId);
            string state = Request["state"];

            if (string.IsNullOrEmpty(username)) throw new ArgumentNullException("username");
            if (0 == modelId) throw new ArgumentNullException("modelid");
            this.ModelId = modelId;

            var cd = WebServices.LogIn(username, apiKey);
            this.Credentials = cd;
            var modelData = cd.GetModelDataById(modelId);
            if (null == modelData)
            {
                // no model found
                return;
            }
            var model = modelData.DeserializedStateModel;

            StateMagic.Common.State initialState = null;
            if (string.IsNullOrEmpty(state))
            {
                initialState = (from s in model.States where s.Default select s).First();
            }
            else
            {
                initialState = (from s in model.States where s.Name == state select s).First();
            }
            if (null == initialState)
            {
                return;
            }

            repeater.DataSource = (from stateitem in model.GetNextStates(initialState) select stateitem.Name).ToArray();
            repeater.DataBind();
            
        }
    }
}