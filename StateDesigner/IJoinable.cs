using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace WpfClient
{
    public interface IJoinable
    {
        Point Centre { get; }

        double Radius { get; }
    }
}
