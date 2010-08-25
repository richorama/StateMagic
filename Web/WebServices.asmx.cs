using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using StateMagic.DatabaseTypes;
using StateMagic.Common;
using StateMagic.DataAccess;
using Castle.ActiveRecord;
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
                DataAccess.DatabaseWrapper.Init();
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

        //[WebMethod]
        //public List<ModelSummary> GetAllModels(string username, string password)
        //{
        //    CredentialData cd = LogIn(username, password);
        //    return (from md in cd.Models select md.CreateModelSummary()).ToList();
        //}

        //[WebMethod]
        //public StateModel GetModel(string username, string password, int modelId)
        //{
        //    CredentialData cd = LogIn(username, password);
        //    StateModel sm = (from md in cd.Models where md.ModelDataID == modelId select md.DeserializedStateModel).First();
        //    if (null == sm)
        //    {
        //        return null;
        //    }
        //    sm.ModelID = modelId;
        //    return sm;
        //}

        //[WebMethod]
        //public void SaveModel(string username, string password, StateModel model)
        //{
        //    CredentialData cd = LogIn(username, password);
        //    ModelData md = cd.UpdateModel(model);
        //    md.Save();
        //    cd.Save();
        //}

        [WebMethod]
        public List<string> GetNextState(string username, string password, int modelID, string currentState)
        {
            CredentialData cd = LogIn(username, password);
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
