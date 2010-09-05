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
using Castle.ActiveRecord.Linq;

namespace StateMagic.DatabaseTypes
{
    [ActiveRecord]
    public class ApiKey : ActiveRecordLinqBase<ApiKey>
    {
        [PrimaryKey]
        public int ApiKeyID { get; set; }

        [Property]
        public Guid APIKey { get; set; }

        [Property]
        public DateTime DateCreated { get; set; }

    }
}
