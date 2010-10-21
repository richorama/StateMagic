using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using StateMagic.DatabaseTypes;
using StateMagic.Common;
using NHibernate.Search;
using NHibernate.Criterion;

namespace StateMagic.Web
{

    [WebService(Namespace = "http://v1.public.services.statemagic.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class WebServices : System.Web.Services.WebService
    {

        
        internal static CredentialData LogIn(string username, string password)
        {
            try
            {
                CredentialData cd = CredentialData.FindOne(new ICriterion[] { Restrictions.Eq("Username", username), Restrictions.Eq("Password", password) });
                if (cd != null && cd.Password == password)
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

        internal static CredentialData LogIn(string username, Guid apiKey)
        {
            try
            {
                CredentialData cd = CredentialData.FindOne(new ICriterion[] { Restrictions.Eq("Username", username), Restrictions.Eq("ApiKey", apiKey) });
                if (cd != null)
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
        public List<string> GetNextState(string username, Guid  apiKey, int modelID, string currentState)
        {
            CredentialData cd = LogIn(username, apiKey);
            if (cd.TransactionBalance <= 0)
            {
                throw new Exception("Account balance is zero. Please top up your account.");
            }

            ModelData md = cd.GetModelDataById(modelID);
            StateModel sm = md.DeserializedStateModel;
            State state = sm.GetStateByName(currentState);
            if (null == state)
            {
                return new List<string>();
            }
            cd.TransactionBalance--;
            cd.Save();// update balance.

            return (from connectedState in sm.GetNextStates(state) select connectedState.Name).ToList();
        }



    }
}
