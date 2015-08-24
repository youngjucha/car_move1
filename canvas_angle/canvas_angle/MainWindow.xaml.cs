using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace canvas_angle
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        Point currentPoint = new Point();
        Point D_currentPoint = new Point();
        Point M_currentPoint = new Point();
        List<Point> angle_list1 = new List<Point>();
        int delayCount = 0;
        List<double> angle_list2 = new List<double>();
        int wheelA, wheelB;




        public MainWindow()
        {
            InitializeComponent();
        }

        private void canv_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                currentPoint = e.GetPosition(this);
                //Console.WriteLine("click"+D_currentPoint);
            }
            D_currentPoint = currentPoint;
            angle_list1.Add(D_currentPoint);
        }

        private void canv_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (delayCount != 5)
            {
                delayCount++;
            }
            else
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Line line = new Line();

                    line.Stroke = SystemColors.WindowFrameBrush;
                    line.X1 = currentPoint.X;
                    line.Y1 = currentPoint.Y;
                    line.X2 = e.GetPosition(this).X;
                    line.Y2 = e.GetPosition(this).Y;

                    currentPoint = e.GetPosition(this);
                    canv.Children.Add(line);
                    //LogText.Text += "("+M_currentPoint.X +", " + M_currentPoint.Y +")\n";
                    //Log.ScrollToEnd();
                    angle_list1.Add(currentPoint);

                }

                M_currentPoint = currentPoint;
                //Console.WriteLine("Move"+M_currentPoint);
                delayCount = 0;
            }

        }

        private void canv_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Released)
            {
                currentPoint = e.GetPosition(this);
                ////Console.WriteLine("U");
                angle_list1.Add(currentPoint);
                angle();
            }
        }

        public void angle()
        {
            for (int i = 0; i < (angle_list1.Count) - 1; i++)
            {
                //Console.WriteLine("list Count : " + angle_list1.Count);
                //Console.WriteLine("i : " + i + " / i+1 : " + (i + 1));

                Vector a =
                    new Vector(0, Math.Abs((angle_list1[i].Y != angle_list1[i + 1].Y) ? angle_list1[i].Y - angle_list1[i + 1].Y : angle_list1[i + 1].Y));
                Vector b =
                    new Vector(angle_list1[i + 1].X - angle_list1[i].X, angle_list1[i + 1].Y - angle_list1[i].Y);
                double angle = Vector.AngleBetween(b, a);
                angle_list2.Add(angle);

                LogText.Text += "======\n";
                LogText.Text += "[i] : " + angle_list1[i] + "\n";
                LogText.Text += "[i+1] : " + angle_list1[i + 1] + "\n";
                LogText.Text += "angle : " + angle + "\n";

                //LogText.Text += angle_list1[i] + "Betwwen" + angle_list1[i + 1] + " angle : " + angle;
                Log.ScrollToEnd();
            }

            //Vector a =
            //    new Vector(0, Math.Abs((D_currentPoint.Y != M_currentPoint.Y) ? D_currentPoint.Y - M_currentPoint.Y : M_currentPoint.Y));
            //Vector b =
            //   new Vector(M_currentPoint.X - D_currentPoint.X, M_currentPoint.Y - D_currentPoint.Y);
            //double angle = Vector.AngleBetween(b, a);

            for (int i = 0; i < (angle_list2.Count) - 1; i++)
            {
                if (angle_list2[i] == angle_list2[i + 1])
                {
                    wheelA = 1;
                    wheelB = 1;
                }
                else if ((angle_list2[i]) > 90 && (angle_list2[i] < 180))
                {
                    wheelA = 1;
                    wheelB = 0;
                }
                else if ((angle_list2[i]) > 0 && (angle_list2[i] < 90))
                {
                    wheelA = -1;
                    wheelB = 0;
                }
                else if ((angle_list2[i]) > -90 && (angle_list2[i] < 0))
                {
                    wheelA = 0;
                    wheelB = -1;
                }
                else if ((angle_list2[i]) > -180 && (angle_list2[i] < -90))
                {
                    wheelA = 0;
                    wheelB = 1;
                }
                else { }

            }
            LogText.Text += "A : " + wheelA + "\n";
            LogText.Text += "B : " + wheelB + "\n";
        }
    }
}

