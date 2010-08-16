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
using StateDesigner;

namespace WpfClient
{


    public delegate void StateDeletedHandler(object sender, MouseButtonEventArgs e);

    /// <summary>
    /// Interaction logic for StateControl.xaml
    /// </summary>
    public partial class StateControl : UserControl, IJoinable, INameable, IDeleteable
    {
        public Color LightBlue = new Color() { A = 255, R = 173, G = 216, B = 230 };

        //private GrabControl grabControl = null;
        
        public StateControl()
        {
            InitializeComponent();
            this.grabControl1.Grabbed += new ControlGrabbedHandler(this.OnGrabControlGrabbed);
            this.textBox1.Visibility = Visibility.Collapsed;
            this.label2.Visibility = Visibility.Collapsed;
            this.grabArrow1 = new Arrow(new Point(110, 110), new Point(125, 125));
            grabArrow1.Stroke = new SolidColorBrush(Colors.DarkGray);
            grabArrow1.Update();
            DestroyGrab();
            Page1.Instance.AddControl(grabArrow1);

        }

        public string DisplayName
        {
            get
            {
                return this.label1.Text == null ? "" : this.label1.Text as string;
            }
            set
            {
                this.label1.Text = value;
            }
        }

        public bool HasMouseHover
        {
            get;
            private set;
        }

        public event ControlGrabbedHandler GrabControlGrabbed;

        public event StateDeletedHandler StateDeleted;

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            this.HasMouseHover = true;
            this.StartEditingDisplayName();
            CreateGrab();
        }

        private void CreateGrab()
        {
            grabControl1.Visibility = Visibility.Visible;
            grabArrow1.Visibility = Visibility.Visible;
            Page1.CalcuateArrowPosition(this, grabControl1, grabArrow1);
        }



        private void DestroyGrab()
        {  
            this.grabControl1.Visibility = Visibility.Collapsed;
            this.grabArrow1.Visibility = Visibility.Collapsed;
        }


        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            this.HasMouseHover = false;
            this.DestroyGrab();
            this.EndEditingDisplayName();
        }

        private void OnGrabControlGrabbed(object sender, MouseButtonEventArgs e)
        {
            if (this.GrabControlGrabbed != null)
            {
                this.GrabControlGrabbed(this, e);
            }
        }


        public void StartedMoving()
        {
            this.DestroyGrab();
        }

        public void StoppedMoving()
        {
            if (this.HasMouseHover)
            {
                CreateGrab();
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
                return this.ellipse1.ActualWidth / 2;
            }
        }

        public bool LightUp(GrabControl grabControl)
        {

            if (this == grabControl.ParentControl)
            {
                this.ellipse1.Stroke = new SolidColorBrush(Colors.Red);
                return false;
            }


            foreach (Arrow arrow in this.arrowsIn)
            {
                if (arrow.TailControl == grabControl.ParentControl)
                {
                    this.ellipse1.Stroke = new SolidColorBrush(Colors.Red);
                    return false;
                }
            }
    
            this.ellipse1.Stroke = new SolidColorBrush(Colors.Green);
            return true;
        }



        public void LightOut(GrabControl grabControl)
        {
            this.ellipse1.Stroke = new SolidColorBrush(LightBlue);
        }

        private List<Arrow> arrowsIn = new List<Arrow>();
        private List<Arrow> arrowsOut = new List<Arrow>();

        public enum ArrowDirection
        {
            In,
            Out
        }

        public void AddArrow(Arrow arrow, ArrowDirection direction)
        { 
            switch (direction)
            {
                case ArrowDirection.In:
                    arrowsIn.Add(arrow);
                    break;
                case ArrowDirection.Out:
                    arrowsOut.Add(arrow);
                    break;
            }
        }

        public List<Arrow> ArrowsIn
        {
            get 
            {
                return this.arrowsIn;
            }   
        }

        public List<Arrow> ArrowsOut
        {
            get
            {
                return this.arrowsOut;
            }
        }

        public List<Arrow> AllArrows
        { 
            get
            {
                List<Arrow> arrows = new List<Arrow>();
                arrows.AddRange(this.ArrowsIn);
                arrows.AddRange(this.ArrowsOut);
                return arrows;
            }
        }

        public void StartEditingDisplayName()
        {
            this.label1.Visibility = Visibility.Collapsed;
            this.textBox1.Visibility = Visibility.Visible;
            this.textBox1.Text = this.DisplayName;
            this.textBox1.Focus();
        }

        public void EndEditingDisplayName()
        {
            this.DisplayName = textBox1.Text;
            this.label1.Visibility = Visibility.Visible;
            this.textBox1.Visibility = Visibility.Collapsed;
        }

        private void textBox1_LostFocus(object sender, RoutedEventArgs e)
        {
            EndEditingDisplayName();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                EndEditingDisplayName();
            }
        }

        private bool startState = false;
        public bool StartState
        {
            get
            {
                return this.startState;
            }
            set
            {
                if (value)
                {
                    this.ellipse1.Fill = new SolidColorBrush(Colors.White);
                    this.label2.Visibility = Visibility.Visible;
                }
                else
                {
                    this.ellipse1.Fill = new SolidColorBrush(LightBlue);
                    this.label2.Visibility = Visibility.Collapsed;
                }
                this.startState = value;
            }
        }

        public bool CanDelete
        {
            get
            {
                return !this.StartState;
            }
        }

        private void ellipse2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

    }
}
