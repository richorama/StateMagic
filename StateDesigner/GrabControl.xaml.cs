using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfClient
{

    public delegate void ControlGrabbedHandler(object sender, MouseButtonEventArgs e);

    /// <summary>
    /// Interaction logic for GrabControl.xaml
    /// </summary>
    public partial class GrabControl : UserControl, IJoinable
    {
        public GrabControl()
        {
            InitializeComponent();
        }

        public event ControlGrabbedHandler Grabbed;

        public bool HasMouseHover
        {
            get;
            private set;
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            this.HasMouseHover = true;
            //e.Handled = true;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            this.HasMouseHover = false;
            //e.Handled = true;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            e.Handled = true;
            if (this.Grabbed != null)
            {
                this.Grabbed(this, e);
            }
        }

        public Point Centre
        {
            get
            {
                return new Point(Canvas.GetLeft(this) + this.ActualWidth / 2, Canvas.GetTop(this) + this.ActualHeight / 2);
            }
        }

        public double Radius
        {
            get 
            {
                return this.ActualWidth / 2; 
            }
        }

        public StateControl ParentControl
        {
            get;
            set;
        }

    }
}
