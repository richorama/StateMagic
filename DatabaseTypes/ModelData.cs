using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord.Attributes;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Config;
using Common;

namespace DatabaseTypes
{
    [ActiveRecord]
    public class ModelData : ActiveRecordBase<ModelData>
    {
        [PrimaryKey]
        public int ModelDataID { get; set; }

        [Property(SqlType="Text")]
        public string SerializedStateModel { get; set; }

        // this field should not be persisted
        public StateModel DeserializedStateModel 
        {
            get
            {
                return SerializationWrapper.DeserializeString<StateModel>(this.SerializedStateModel) as StateModel;
            }
            set
            {
                this.SerializedStateModel = SerializationWrapper.Serialize<StateModel>(value);
            }
        }

        [Property]
        public string ModelName { get; set; }

        [BelongsTo("CredentialDataRef")]
        public CredentialData AssociatedAccount { get; set; }

        public ModelSummary CreateModelSummary()
        {
            return new ModelSummary() { ModelDataID = this.ModelDataID, ModelName = this.ModelName };
        }

    }
}
