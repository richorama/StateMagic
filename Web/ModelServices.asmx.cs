using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using StateMagic.DatabaseTypes;
using StateMagic.Common;
using Castle.ActiveRecord;
using NHibernate.Search;
using NHibernate.Criterion;


namespace StateMagic.Web
{

    [WebService(Namespace = "http://v1.model.services.statemagic.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class ModelServices : System.Web.Services.WebService
    {

        public static readonly Guid SystemKey = new Guid("3FB3447D-5707-4525-91DF-8FE7B2396088");

        internal static CredentialData LogIn(string username, Guid apiKey)
        {
            //try
            //{
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
            //}
            //catch
            //{
            //    throw new System.Security.SecurityException();
            //}
            throw new System.Security.SecurityException();
        }

        [WebMethod]
        public List<ModelSummary> GetAllModels(string username, Guid apiKey, Guid systemKey)
        {
            CheckSystemKey(systemKey);
            CredentialData cd = LogIn(username, apiKey);
            return (from md in cd.Models select md.CreateModelSummary()).ToList();
        }

        [WebMethod]
        public StateModel GetModel(string username, Guid apiKey, int modelId, Guid systemKey)
        {
            CheckSystemKey(systemKey);
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
        public int SaveModel(string username, Guid apiKey, StateModel model, Guid systemKey)
        {
            CheckSystemKey(systemKey);
            CredentialData cd = LogIn(username, apiKey);
            ModelData md = cd.UpdateModel(model);
            md.Save();
            cd.Save();
            return md.ModelDataID;
        }

        [WebMethod]
        public bool CreateAccount(string username, Guid apiKey, string password, Guid systemKey)
        {
            CheckSystemKey(systemKey);
            CheckApiKey(apiKey);

            return CreateAccount(username, apiKey, password) != null;
        }

        public static CredentialData CreateAccount(string username, Guid newApiKey, string password)
        {
            var errorMessages = new List<string>();

            CredentialData[] matches = CredentialData.FindAllByProperty("Username", username);
            if (matches.Length > 0)
            {
                return null;
            }

            // success - create an account
            CredentialData cd = new CredentialData();
            cd.Username = username;
            cd.Password = password;
            cd.Authentications = 0;
            cd.LastAuthentication = DateTime.Now;
            cd.ApiKey = newApiKey;
            cd.TransactionBalance = 1000; // this is the starting balance.
            cd.Save();
            return cd;
        }

        [WebMethod]
        public Guid LogIn(string username, string password, Guid apiKey, Guid systemKey)
        {
            CheckSystemKey(systemKey);
            CheckApiKey(apiKey);

            var credentialData = WebServices.LogIn(username, password);
            return credentialData.ApiKey;
        }

        internal static void CheckApiKey(Guid apiKey)
        {
            
            var key = ApiKey.FindFirst(Restrictions.Eq("APIKey", apiKey));
            if (null == key || key.DateCreated < DateTime.Now.AddDays(-1))
            {
                throw new System.Security.SecurityException("Invalid API Key");
            }
        }

        internal static void CheckSystemKey(Guid value)
        {
            if (value != SystemKey)
            {
                throw new System.Security.SecurityException("Invalid system key");
            }
        }

   
    }
}
