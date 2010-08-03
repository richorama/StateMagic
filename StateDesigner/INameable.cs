using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfClient
{
    public interface INameable
    {
        string DisplayName
        {
            get;
            set;
        }

        void StartEditingDisplayName();

        void EndEditingDisplayName();

    }
}
