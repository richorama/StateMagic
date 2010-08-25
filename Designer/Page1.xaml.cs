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
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : UserControl
    {
        private List<StateControl> stateControls = new List<StateControl>();

        public static Page1 Instance
        {
            get;
            private set;
        }

        public Page1()
        {
            InitializeComponent();
            Page1.Instance = this;
            this.MouseMove += new MouseEventHandler(sc_MouseMove);
            
            StateControl sc = CreateNewState();
            sc.StartState = true;
            SetPosition(sc, new Point(0, 150));
        }

        private int stateIndex = 1;
        private int initIndex = 1;

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            CreateNewState();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(App.Username))
            {
                // you are not a user, create an account
                CreateAccount accountForm = new CreateAccount();

                accountForm.Closed += delegate
                {
                    if (accountForm.DialogResult.HasValue && accountForm.DialogResult.Value)
                    {
                        ShowSaveDialog();
                    }
                };
                accountForm.Show();

            }
            else if (App.ModelId == 0)
            {
                // this is a new model
                ShowSaveDialog();

            }
            else
            { 
                // just save it, we're editing
            
            }
        }

        public void ShowSaveDialog()
        { 
            // TODO
            
        }

        public void Save()
        { 
            //TODO
        }

        private StateControl CreateNewState()
        {
            StateControl sc = new StateControl();
            if (textBox1.Text == string.Empty)
            {
                sc.DisplayName = string.Format("State {0}", stateIndex++);
            }
            else
            {
                sc.DisplayName = textBox1.Text;
            }

            sc.MouseLeftButtonDown += new MouseButtonEventHandler(sc_MouseDown);
            sc.MouseLeftButtonUp += new MouseButtonEventHandler(sc_MouseUp);
            sc.MouseMove += new MouseEventHandler(sc_MouseMove);
            sc.GrabControlGrabbed += new ControlGrabbedHandler(this.OnGrabControlGrabbed);
            sc.StateDeleted += new StateDeletedHandler(this.DeleteEvent);

            this.AddControl(sc);
            this.SetPosition(sc, new Point(10 * initIndex, 10 * initIndex));
            if (initIndex >= 50)
                initIndex = 1;
            initIndex++;

            textBox1.Text = "";
            textBox1.Focus();
            return sc;
        }

        public void AddControl(UIElement control)
        {
            this.canvas1.Children.Add(control);

            if (control is StateControl)
            {
                this.stateControls.Add(control as StateControl);
            }
        }

        public void RemoveControl(UIElement control)
        {
            this.canvas1.Children.Remove(control);
            if (control is StateControl)
            {
                this.stateControls.Remove(control as StateControl);
            }
        }

        public void SetPosition(Control control, Point position)
        {
            Canvas.SetLeft(control,position.X);
            Canvas.SetTop(control, position.Y);
        }

        public Point GetPosition(Control control)
        {
            return new Point((int)Canvas.GetLeft(control), (int)Canvas.GetTop(control));
        }

        bool m_mouseDown = false;
        bool m_grabMouseDown = false;
        Point originalMousePosition = new Point(0, 0);
        Point originalControlPosition = new Point(0, 0);
        StateControl activeControl = null;

        void sc_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is StateControl)
            {
                this.m_mouseDown = false;
                (sender as StateControl).StoppedMoving();

            }
            else
            {
                StateControl sc = CheckforHover(this.grabControl);
                if (null == sc)
                {
                    DeSelectAllStates();
                }
                else
                {
                    if (sc.LightUp(grabControl))
                    {

                        AddArrow(sc, this.grabControl.ParentControl);
                    }
                    sc.LightOut(this.grabControl);
                }
                this.m_grabMouseDown = false;
                this.RemoveControl(this.grabControl);
                this.grabControl = null;
                this.RemoveControl(this.grabArrow);
                this.grabArrow = null;
            }
            this.activeControl = null;
        }

        void sc_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is StateControl)
            {
                (sender as StateControl).StartedMoving();
                this.activeControl = sender as StateControl;
                this.m_mouseDown = true;
                this.originalMousePosition = e.GetPosition(this);
                this.originalControlPosition.X = Canvas.GetLeft(this.activeControl);
                this.originalControlPosition.Y = Canvas.GetTop(this.activeControl);
            }
        }

        private void UpdateArrows(StateControl sc)
        {
            if (sc == null) throw new ArgumentNullException("sc");
            foreach (Arrow arrow in sc.AllArrows)
            {
                CalcuateArrowPosition(arrow.TailControl, arrow.HeadControl, arrow);
            }
        }

        void sc_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (this.m_mouseDown)
            { 
                Point position = e.GetPosition(this);
                Canvas.SetLeft(this.activeControl, this.originalControlPosition.X + (position.X - this.originalMousePosition.X));
                Canvas.SetTop(this.activeControl, this.originalControlPosition.Y + (position.Y - this.originalMousePosition.Y));
                UpdateArrows(this.activeControl);
            }
            else if (this.m_grabMouseDown)
            {
                Point position = e.GetPosition(canvas1);
                Canvas.SetLeft(this.grabControl, this.originalControlPosition.X + (position.X - this.originalMousePosition.X) - 7);
                Canvas.SetTop(this.grabControl, this.originalControlPosition.Y + (position.Y - this.originalMousePosition.Y) - 7);
                StateControl sc = CheckforHover(this.grabControl);
                if (null == sc)
                {
                   CalcuateArrowPosition(grabControl.ParentControl, grabControl, this.grabArrow);
                   DeSelectAllStates();
                   this.grabArrow.Stroke = new SolidColorBrush(Colors.DarkGray);
                }
                else
                {
                    CalcuateArrowPosition(grabControl.ParentControl, sc, this.grabArrow);
                    
                    if (sc.LightUp(grabControl))
                    {
                        this.grabArrow.Stroke = new SolidColorBrush(Colors.Green);
                    }
                    else
                    {
                        this.grabArrow.Stroke = new SolidColorBrush(Colors.Red);
                    }
                
                }

            }
        }

        private void DeSelectAllStates()
        {
            foreach (StateControl control in this.stateControls)
            {
                control.LightOut(grabControl);
            }
        }

        private void Page_MouseLeave(object sender, MouseEventArgs e)
        {

            this.m_mouseDown = false;
        }


        private GrabControl grabControl;
        private Arrow grabArrow;

        private void OnGrabControlGrabbed(object sender, MouseButtonEventArgs e)
        {
            grabControl = new GrabControl();
            this.AddControl(grabControl);
            this.m_grabMouseDown = true;
            this.originalMousePosition = e.GetPosition(canvas1);
            this.originalControlPosition = this.originalMousePosition;
            Canvas.SetLeft(this.grabControl, this.originalMousePosition.X - 7);
            Canvas.SetTop(this.grabControl, this.originalMousePosition.Y - 7);
            grabControl.MouseMove += new MouseEventHandler(this.sc_MouseMove);
            grabControl.MouseLeftButtonUp += new MouseButtonEventHandler(this.sc_MouseUp);
            grabControl.ParentControl = sender as StateControl;

            grabArrow = new Arrow();
            CalcuateArrowPosition(sender as StateControl, grabControl, grabArrow);
            
            grabArrow.Stroke = new SolidColorBrush(Colors.DarkGray);
            this.AddControl(grabArrow);
        }

        public static void CalcuateArrowPosition(IJoinable source, IJoinable target, Arrow arrow)
        {
            Point sourceMidpoint = source.Centre;
            Point targetMidpoint = target.Centre;
            double angle = Math.Atan2(targetMidpoint.Y - sourceMidpoint.Y, targetMidpoint.X - sourceMidpoint.X);
            Point start = new Point(sourceMidpoint.X + Math.Cos(angle -0.2) * source.Radius, sourceMidpoint.Y + Math.Sin(angle -0.2) * source.Radius);
            Point end = new Point(targetMidpoint.X - Math.Cos(angle +0.2) * target.Radius, targetMidpoint.Y - Math.Sin(angle +0.2) * target.Radius);
            arrow.StartPoint = start;
            arrow.EndPoint = end;
            arrow.Update();
        }

        public static double GetDistance(Point point1, Point point2)
        {
            return Math.Sqrt(Math.Pow(point2.X - point1.X, 2) + Math.Pow(point2.Y - point1.Y, 2));
        }

        public StateControl CheckforHover(GrabControl grabControl)
        {
            Point grabControlPosition = grabControl.Centre;
            foreach (StateControl sc in this.stateControls)
            {
             
                if (GetDistance(grabControlPosition, sc.Centre) <= sc.Radius)
                {
                    return sc;
                }
         
            }
            return null;
        }

        public Arrow AddArrow(StateControl head, StateControl tail)
        {
            Arrow arrow = new Arrow();
            arrow.HeadControl = head;
            arrow.TailControl = tail;
            head.AddArrow(arrow, StateControl.ArrowDirection.In);
            tail.AddArrow(arrow, StateControl.ArrowDirection.Out);
            CalcuateArrowPosition(tail, head, arrow);
            arrow.Stroke = new SolidColorBrush(Colors.DarkGray);
            this.AddControl(arrow);
            arrow.Update();
            return arrow;
        }

        private void DeleteEvent(object sender, MouseButtonEventArgs e)
        {
            if (!(sender as IDeleteable).CanDelete)
            {
                return;
            }

            if (sender is StateControl)
            {
                StateControl sc = sender as StateControl;
                foreach (Arrow arrow in sc.ArrowsIn)
                {
                    arrow.TailControl.ArrowsOut.Remove(arrow);
                    RemoveControl(arrow);
                }
                foreach (Arrow arrow in sc.ArrowsOut)
                {
                    arrow.HeadControl.ArrowsIn.Remove(arrow);
                    RemoveControl(arrow);
                }
                RemoveControl(sc);
                sc = null;
            }


            if (sender is Arrow)
            {
                Arrow arrow = sender as Arrow;
                arrow.HeadControl.ArrowsIn.Remove(arrow);
                arrow.TailControl.ArrowsOut.Remove(arrow);
                RemoveControl(arrow);
            }
        }

       
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CreateNewState();
            }
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            sc_MouseMove(sender, e);
        }


        public void SaveModel()
        { 
            
        
        }
        
    }
}
