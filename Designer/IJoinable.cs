using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace StateMagic.Designer
{
    public interface IJoinable
    {
        Point Centre { get; }

        double Radius { get; }
    }
}
