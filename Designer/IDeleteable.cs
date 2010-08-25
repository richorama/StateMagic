using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StateMagic.Designer
{
    public interface IDeleteable
    {
        bool CanDelete
        {
            get;
        }

    }
}
