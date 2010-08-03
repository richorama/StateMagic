using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfClient
{
    public interface IDeleteable
    {
        bool CanDelete
        {
            get;
        }

    }
}
