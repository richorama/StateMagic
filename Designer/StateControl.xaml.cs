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

namespace StateMagic.Designer
{


    public delegate void StateDeletedHandler(object sender, MouseButtonEventArgs e);

    /// <summary>
    /// Interaction logic for StateControl.xaml
    /// </summary>
    public partial class StateControl : UserControl, IJoinable, IDeleteable
    {
        public Color LightBlue = new Color() { A = 255, R = 173, G = 216, B = 230 };

        //private GrabControl grabControl = null;

        public int StateId { get; set; }

        public StateControl()
        {
            InitializeComponent();
            this.grabControl1.Grabbed += new ControlGrabbedHandler(this.OnGrabControlGrabbed);
            this.label2.Visibility = Visibility.Collapsed;
            DestroyGrab();
            this.deleteButton1.DeletePressed += new DeletePressed(this.Delete);
            

        }

        public string DisplayName
        {
            get
            {
                return this.label1.Text == null ? string.Empty : this.label1.Text as string;
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
            CreateGrab();
        }

        private void CreateGrab()
        {
            this.grabControl1.Visibility = Visibility.Visible;
            this.image1.Visibility = Visibility.Visible;
            if (this.CanDelete)
            {
                this.deleteButton1.Visibility = Visibility.Visible;
            }
            this.label1.TextDecorations = TextDecorations.Underline;
            this.label1.FontWeight = FontWeights.Bold;
        }



        private void DestroyGrab()
        {  
            this.grabControl1.Visibility = Visibility.Collapsed;
            this.image1.Visibility = Visibility.Collapsed;
            this.deleteButton1.Visibility = Visibility.Collapsed;
            this.label1.TextDecorations = null;
            this.label1.FontWeight = FontWeights.Normal;

        }


        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            this.HasMouseHover = false;
            this.DestroyGrab();
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
                    //this.ellipse1.Fill = new SolidColorBrush(LightBlue);
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

        private void Delete(object sender, MouseButtonEventArgs e)
        {
            if (StateDeleted != null)
            {
                StateDeleted(this, e);
            }

        }

        private void label1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RenameState rs = new RenameState();
            rs.Text = this.DisplayName;
            rs.Show();
            e.Handled = true;
            DestroyGrab();
            rs.Closed += delegate 
            {
                if (rs.DialogResult.HasValue && rs.DialogResult.Value)
                {
                    this.DisplayName = rs.Text;
                }
            };
        }

        public Point InitialPosition { get; set; }


    }
}
