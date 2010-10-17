using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Threading;

namespace StateMagic.Designer
{
    public partial class Toast : UserControl
    {
        public Toast()
        {
            InitializeComponent();
        }

        public string Message
        {
            get
            {
                return (string) this.label1.Content;
            }
            set
            {
                this.label1.Content = value;
            }
        }

        public void Show(string message)
        {

            this.Message = message;
            this.Visibility = System.Windows.Visibility.Visible;
            this.Opacity = 1.0;
          
            HideToast.Begin();
        }

    }
}
