using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Castle.ActiveRecord;
using StateMagic.DataAccess;

namespace StateMagic.InitData
{
    [TestFixture]
    public class CreateDatabaseTables
    {
        [Test]
        public void CreateSchema()
        {
            DataAccess.DatabaseWrapper.Init();
            ActiveRecordStarter.CreateSchema();
        }

    }
}
