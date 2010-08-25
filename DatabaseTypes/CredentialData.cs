using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord.Attributes;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Config;
using System.Reflection;
using StateMagic.Common;

namespace StateMagic.DatabaseTypes
{
    [ActiveRecord]
    public class CredentialData : ActiveRecordBase<CredentialData>
    {
        [PrimaryKey]
        public int CredentialDataID { get; set; }

        [Property]
        public string Username { get; set; }
        
        [Property]
        public string Password { get; set; }

        [Property]
        public int Authentications { get; set; }

        [Property]
        public DateTime LastAuthentication { get; set; }

        [HasMany]
        public IList<ModelData> Models { get; set; }

        public ModelData GetModelDataById(int id)
        {
            return (from md in this.Models where md.ModelDataID == id select md).First();
        }

        [Property]
        public int TransactionBalance { get; set; }

        [Property]
        public Guid ApiKey { get; set; }

        public ModelData UpdateModel(StateModel model)
        {
            ModelData md = null;
            if (model.ModelID != 0)
            {
                md = GetModelDataById(model.ModelID);
            }

            if (null == md)
            {
                md = new ModelData();
            }
            md.AssociatedAccount = this;
            md.ModelName = model.ModelName;
            md.DeserializedStateModel = model;
            this.Models.Add(md);
            return md;
        }

    }
}
