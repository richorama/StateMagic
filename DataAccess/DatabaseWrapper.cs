using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Castle.ActiveRecord.Attributes;
using Castle.ActiveRecord.Framework;
using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework.Config;
using System.Reflection;
using StateMagic.DatabaseTypes;

namespace StateMagic.DataAccess
{
    public static class DatabaseWrapper
    {
        private static bool initialized = false;

        static object sync = new object();

        public static void Init()
        {
            lock (sync)
            {

                if (initialized)
                    return;

                // This is a complete hack, which will attempt to load the config file from the bin directory of the current application
                string dllLocation = System.Reflection.Assembly.GetExecutingAssembly().CodeBase.Replace(@"file:///", "").Replace(@"/", @"\");
                FileInfo file = new FileInfo(dllLocation);

                XmlConfigurationSource config = null;
                if (File.Exists(file.Directory + @"\ARConfig.xml"))
                {
                    config = new XmlConfigurationSource(file.Directory + @"\ARConfig.xml");
                }
                else if (File.Exists(file.Directory.Parent.FullName + @"\ARConfig.xml"))
                {
                    config = new XmlConfigurationSource(file.Directory.Parent.FullName + @"\ARConfig.xml");
                }



                if (!ActiveRecordStarter.IsInitialized)
                    ActiveRecordStarter.Initialize(config, typeof(CredentialData), typeof(ModelData), typeof(ApiKey));

                initialized = true;
            }
        }

    }
}
