using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord.Attributes;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Config;
using System.Reflection;

namespace DatabaseTypes
{
    [ActiveRecord]
    public class SessionData : ActiveRecordBase<SessionData>
    {
        [PrimaryKey]
        public int SessionDataID { get; set; }

        [Property]
        public Guid SessionGuid { get; set; }

        [Property]
        public DateTime SessionStart { get; set; }

        [Property]
        public DateTime SessionLastActivity { get; set; }

        [Property]
        public int ActivityCounter { get; set; }

        [BelongsTo("CredentialDataRef")]
        public CredentialData AssociatedAccount { get; set; }

    }
}
