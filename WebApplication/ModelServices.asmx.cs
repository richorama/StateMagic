using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using DatabaseTypes;
using Common;
using DataAccess;
using Castle.ActiveRecord;
using NHibernate.Search;
using NHibernate.Criterion;


namespace WebApplication
{

    [WebService(Namespace = "http://v1.model.services.statemagic.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class ModelServices : System.Web.Services.WebService
    {
        internal static CredentialData LogIn(string username, Guid apiKey)
        {
            try
            {
                DataAccess.DatabaseWrapper.Init();
                CredentialData cd = CredentialData.FindOne(new ICriterion[] { Restrictions.Eq("Username", username), Restrictions.Eq("ApiKey", apiKey) });
                if (cd != null && cd.ApiKey == apiKey)
                {
                    cd.LastAuthentication = DateTime.Now;
                    cd.Authentications++;
                    cd.Save();
                    return cd;
                }
                else
                {
                    throw new System.Security.SecurityException();
                }
            }
            catch
            {
                throw new System.Security.SecurityException();
            }
            throw new System.Security.SecurityException();
        }

        [WebMethod]
        public List<ModelSummary> GetAllModels(string username, Guid apiKey)
        {
            CredentialData cd = LogIn(username, apiKey);
            return (from md in cd.Models select md.CreateModelSummary()).ToList();
        }

        [WebMethod]
        public StateModel GetModel(string username, Guid apiKey, int modelId)
        {
            CredentialData cd = LogIn(username, apiKey);
            StateModel sm = (from md in cd.Models where md.ModelDataID == modelId select md.DeserializedStateModel).First();
            if (null == sm)
            {
                return null;
            }
            sm.ModelID = modelId;
            return sm;
        }

        [WebMethod]
        public void SaveModel(string username, Guid apiKey, string password, StateModel model)
        {
            CredentialData cd = LogIn(username, apiKey);
            if (cd.Password != password)
            {
                throw new System.Security.SecurityException();
            }
            ModelData md = cd.UpdateModel(model);
            md.Save();
            cd.Save();
        }
   
    }
}
