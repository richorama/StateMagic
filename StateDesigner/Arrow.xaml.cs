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
using WpfClient;

namespace StateDesigner
{
    public partial class Arrow : UserControl, IDeleteable
    {
        private Point startPoint;
        public Point StartPoint
        {
            get { return startPoint; }
            set
            {
                startPoint = value;
            }
        }

        private Point endPoint;
        public Point EndPoint
        {
            get { return endPoint; }
            set
            {
                endPoint = value;
            }
        }

        public Arrow()
        {
            InitializeComponent();
            this.StrokeThickness = 3;
            Update();
        }

        public Arrow(Point StartPoint, Point EndPoint)
        {
            InitializeComponent();
            startPoint = StartPoint;
            endPoint = EndPoint;
            this.StrokeThickness = 3;
            Update();
        }


        public StateControl HeadControl
        {
            get;
            set;
        }

        public StateControl TailControl
        {
            get;
            set;
        }

        public bool CanDelete
        {
            get
            {
                return true;
            }
        }

        public Brush Stroke { get; set; }
        public Double StrokeThickness { get; set; }

        const double HeadWidth = 15;
        const double HeadHeight = 10;

        public void Update()
        {
            double X1 = startPoint.X;
            double Y1 = startPoint.Y;
            double X2 = endPoint.X;
            double Y2 = endPoint.Y;
            double theta = Math.Atan2(Y1 - Y2, X1 - X2);
            double sint = Math.Sin(theta + 0.2);
            double cost = Math.Cos(theta + 0.2);

            Point pt1 = new Point(X1, Y1);
            Point pt2 = new Point(X2, Y2);

            Point pt3 = new Point(
                X2 + (HeadWidth * cost - HeadHeight * sint),
                Y2 + (HeadWidth * sint + HeadHeight * cost));

            Point pt4 = new Point(
                X2 + (HeadWidth * cost + HeadHeight * sint),
                Y2 - (HeadHeight * cost - HeadWidth * sint));


            double midLength = Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2)) / 2;

            Point ptx = new Point(pt1.X - midLength * Math.Cos(theta - 0.2), pt1.Y - midLength * Math.Sin(theta - 0.2));

            this.Connector.X1 = pt1.X;
            this.Connector.Y1 = pt1.Y;
            this.Connector.X2 = pt2.X;
            this.Connector.Y2 = pt2.Y;
            this.Cap1.X1 = pt2.X;
            this.Cap1.Y1 = pt2.Y;
            this.Cap1.X2 = pt3.X;
            this.Cap1.Y2 = pt3.Y;
            this.Cap2.X1 = pt2.X;
            this.Cap2.Y1 = pt2.Y;
            this.Cap2.X2 = pt4.X;
            this.Cap2.Y2 = pt4.Y;

            BezierSegment bs = new BezierSegment();
            bs.Point1 = pt1;
            bs.Point2 = ptx;
            bs.Point3 = pt2;
            PathFigure pf = new PathFigure();
            pf.StartPoint = pt1;
            pf.Segments.Add(bs);
            PathGeometry pg = new PathGeometry();
            pg.Figures.Add(pf);
            this.bezier.Data = pg;
            this.bezier.StrokeThickness = this.StrokeThickness;
            this.bezier.Stroke = this.Stroke;

            Cap1.StrokeThickness = this.StrokeThickness;
            Cap1.Stroke = this.Stroke;
            Cap2.StrokeThickness = this.StrokeThickness;
            Cap2.Stroke = this.Stroke;
        }
    }
}