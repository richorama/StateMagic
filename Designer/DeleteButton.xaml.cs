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

namespace StateMagic.Designer
{

    public delegate void DeletePressed(object sender, MouseButtonEventArgs e);

    public partial class DeleteButton : UserControl
    {
        public DeleteButton()
        {
            InitializeComponent();
        }

        public DeletePressed DeletePressed;

        private void ellipse2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.DeletePressed != null)
            {
                this.DeletePressed(this, e);
            }
        }
    }
}
