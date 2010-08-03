using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class State
    {
        public int StateID { get; set; }

        public string Name { get; set; }

        public bool Default { get; set; }

    }
}
