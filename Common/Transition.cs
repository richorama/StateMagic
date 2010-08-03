using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    [Serializable]
    public class Transition
    {
        public int SourceStateRef { get; set; }

        public int DestinationStateRef { get; set; }

    }
}
