﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public string gv_NoIntMessage = "Please only insert positive Numbers!";
        public int gv_cities = 0;
        public int gv_workers = 0;
        public int gv_citiesPerPath;
        public int gv_pathLeft;
        public int midX;
        public int midY;
        public double gv_totalLength;
        public double gv_avgLength;
        public Point[] fullPath = new Point[0];    // initial definition, resize later and fill with the "paths"

        public Form1()
        {
            InitializeComponent();
            panel1.Paint += new PaintEventHandler(panel1_Paint);
            panel1.Controls.Clear();

            midX = panel1.Width / 2;
            midY = panel1.Height / 2;
        }

        public void panel1_Paint( object sender, PaintEventArgs e)
        {
            gv_totalLength = 0;
            gv_avgLength = 0;

            var p = sender as Panel;
            var g = e.Graphics;

            Pen blackPen = new Pen( Color.Black);
            Brush redBrush = new SolidBrush(Color.Red);
            Brush blackBrush = new SolidBrush(Color.Black);


            if (gv_cities == 0 & gv_workers == 0)
            {
                g.FillEllipse(redBrush, midX - 2, midY - 2, 5, 5);
            }

            else
            {
                g.FillEllipse(redBrush, midX - 2, midY - 2, 5, 5);

                g.DrawPolygon(blackPen, fullPath);
                
                for (int i = 0; i < fullPath.Length-1; i++)
                {
                    gv_totalLength += Math.Sqrt(Math.Pow(fullPath[i].X - fullPath[i + 1].X, 2) + Math.Pow(fullPath[i].Y - fullPath[i + 1].Y, 2));
                }
                gui_totalLength.Text = gv_totalLength.ToString();
                gv_avgLength = gv_totalLength / gv_workers;
                gui_avgLength.Text = gv_avgLength.ToString();
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                gv_cities = int.Parse(gui_cities.Text);
                gv_workers = int.Parse(gui_workers.Text);
            }
            catch (Exception)
            {
                MessageBox.Show(gv_NoIntMessage);
            }
            finally
            {
                if (gv_cities <= 0 | gv_workers <= 0)
                {
                    MessageBox.Show("Please only use positive numbers");
                }

                else if (gv_workers > gv_cities)
                {
                    MessageBox.Show("Numbers of cities should be higher than the number of emloyees");
                }
                else
                {

                    fullPath = initialization.path(gv_cities, gv_workers, panel1.Width, panel1.Height);

                    gv_totalLength = 0;
                    gv_avgLength = 0;
                    this.Refresh();
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            fullPath = local_optimization.singlePath(fullPath);
            this.Refresh();
        }

        public class initialization
        {
            public static Point[] path(int cities, int workers, int width, int height)
            {
                Point[] fullPath = new Point[0];
                Random pointRandom = new Random();

                int citiesPerPath = cities / workers;       
                int pathLeft = cities % workers;            //modulo leftover

                int midX = width / 2;
                int midY = height / 2;

                try
                {
                    for (int i = 0; i < workers; i++)
                    {

                        Point[] path = new Point[citiesPerPath + 1];

                        try
                        {
                            for (int j = 1; j <= citiesPerPath; j++)
                            {
                                Boolean rand = true;
                                int randomX = 0;
                                int randomY = 0;
                                while (rand)
                                {
                                    randomX = pointRandom.Next(0, width);
                                    randomY = pointRandom.Next(0, height);

                                    if (randomX == midX & randomY == midY)
                                    {
                                        rand = true;
                                    }
                                    else
                                    {
                                        rand = false;
                                    }
                                    for (int k = 0; k < fullPath.Length; k++)
                                    {
                                        if (randomX == fullPath[k].X & randomY == fullPath[k].Y)
                                        {
                                            rand = true;
                                        }
                                    }
                                    for (int k = 0; k < fullPath.Length; k++)
                                    {
                                        if (randomX == fullPath[k].X & randomY == fullPath[k].Y)
                                        {
                                            rand = true;
                                        }

                                    }
                                }
                                path[j] = new Point(randomX, randomY);
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("da passt wahrscheinlich was mit dem array nicht");
                        }
                        path[0] = new Point(midX, midY);
                        if (pathLeft > 0)
                        {
                            int randomX = pointRandom.Next(0, width);
                            int randomY = pointRandom.Next(0, height);
                            pathLeft--;
                            Array.Resize(ref path, path.Length + 1);
                            path[citiesPerPath + 1] = new Point(randomX, randomY);
                        }

                        int initLength = fullPath.Length;           //save Length before resize
                        Array.Resize(ref fullPath, fullPath.Length + path.Length);      //resize full Array

                        path.CopyTo(fullPath, initLength);          //add single path to complete path
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("da passt wahrscheinlich was mit dem array nicht");
                    }
              
                return fullPath;
            }

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Text|*.txt";
            saveFile.Title = "Save current Path";
            saveFile.ShowDialog();

            try
            {
                if (saveFile.FileName != "")
                {
                    System.IO.FileStream fs = null;
                    System.IO.StreamWriter fw = null;

                    path_format format = new path_format();


                    using ( fs = (System.IO.FileStream)saveFile.OpenFile())
                    using (fw = new System.IO.StreamWriter(fs))
                    {
                        fw.Write(format.toString(fullPath));
                    }
                    fw.Close();
                    fs.Close();
                }
            }
            catch (System.IO.IOException)
            {

            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


    }


    public class local_optimization
    {
        public static Point[] singlePath(Point[] points)
        {

            int splitX = points[0].X;       //X-coordinate of center
            int splitY = points[0].Y;       //Y-coordinate of center
            Boolean leftEnd = true;         //needed to split the complete Array into paths

            Point[] singlePath = new Point[0];      //will be resized and filled with a part of the complete array

            int leftOffset = 0;             //the index to copy from cpmplete Array; initialized
            int rightOffset = 0;            //the length of the copy from complete Array; initialized

            for (int i = 0; i < points.Length; i++) //loop for splitting the complete Array
            {

                if (points[i].X == splitX & points[i].Y == splitY)
                {
                    if (leftEnd)
                    {
                        leftOffset = i;
                        leftEnd = false;
                        
                    }
                    else
                    {
                        rightOffset = i;
                        leftEnd = true;

                        Array.Resize(ref singlePath, rightOffset+1);
                        Array.Copy(points, leftOffset, singlePath, 0, rightOffset+1);
                    }
                }
                //Boolean loopBreak = false;
                Boolean intersectFound = true;
                if (singlePath.Length > 3)
                {
                    while (intersectFound)
                    {
                        intersectFound = false;
                        Boolean loopBreak = false;
                        for (int j = 0; j < singlePath.Length; j++)     //loop for String-inversion inside a path
                        {
                            if (loopBreak)
                            {
                                Array.Copy(singlePath, 0, points, leftOffset, rightOffset - leftOffset);
                                break;
                            }
                            for (int k = 0; k < singlePath.Length; k++)
                            {
                                try
                                {
                                    if (singlePath[j] != singlePath[k] //there cant be intersections between the same 2 or 3 points
                                        & singlePath[j + 1] != singlePath[k]
                                        & singlePath[j] != singlePath[k + 1]
                                        & singlePath[j + 1] != singlePath[k + 1])
                                    {
                                        bool intersect = isIntersect(singlePath[j], singlePath[j + 1], singlePath[k], singlePath[k + 1]);
                                        if (intersect)
                                        {
                                            if (k > j)
                                            {
                                                Point[] tmp = new Point[Math.Abs(k - j)];
                                                Array.Copy(singlePath, j + 1, tmp, 0, Math.Abs(k - j));
                                                Array.Reverse(tmp);
                                                Array.Copy(tmp, 0, singlePath, j + 1, Math.Abs(k - j));
                                            }
                                            else if (j > k)
                                            {
                                                Point[] tmp = new Point[Math.Abs(j - (k+1))];
                                                Array.Copy(singlePath, k + 1, tmp, 0, Math.Abs(j - k));
                                                Array.Reverse(tmp);
                                                Array.Copy(tmp, 0, singlePath, k + 1, Math.Abs(j - k));
                                            }
                                            loopBreak = true;
                                            intersectFound = true;
                                            break;

                                        }
                                        else
                                        {
                                            intersectFound = false;
                                        } //close IF
                                    } //close IF
                                }
                                catch (IndexOutOfRangeException)
                                {
                                    intersectFound = false;
                                }
                            } //close inner for-loop (k)
                        } //close outer for-loop (j)
                    } //close while-loop
                } //close IF                
            } //close splitting for-loop (i)
                return points;
        } //close method singlePath

        private static Boolean isIntersect(Point pointA, Point pointB, Point pointC, Point pointD)
        {
            Point A = new Point();
            Point B = new Point();
            Point C = new Point();
            Point D = new Point();

            A = pointA;
            B = pointB;
            C = pointC;
            D = pointD;

            float mAB = 0;          //gradient AB
            float mCD = 0;          //gradient CD
            float cAB = 0;          //y-intersection AB
            float cCD = 0;          //y-intersection CD
            float intersectX;       //intersection coordinate X
            float intersectY;       //intersection coordinate Y

            try
            {
                try
                {
                    //line A->B:
                    mAB = Convert.ToSingle(B.Y - A.Y) / Convert.ToSingle(B.X - A.X);      //divide by zero! -> vertical line
                    cAB = (A.Y - mAB * A.X);

                    //line C->D
                    mCD = Convert.ToSingle(D.Y - C.Y) / Convert.ToSingle(D.X - C.X);      //divide by zero! -> vertical line
                    cCD = (C.Y - mCD * C.X);
                }
                catch (DivideByZeroException)
                {
                    MessageBox.Show("senkrechte Linie... Mist!");
                }

                try
                {
                    intersectX = ((cCD - cAB) / (mAB - mCD));   //divide by zero: same gradient -> no intersection
                                                                //what if 2 lines are aligning?
                    intersectY = mAB * intersectX + cAB;

                    if ( (intersectX < A.X && intersectX > B.X) && (intersectX < C.X && intersectX > D.X ) )
                    {
                        return true;
                    }
                    else if ( (intersectX > A.X && intersectX < B.X) && (intersectX > C.X && intersectX < D.X) )
                    {
                        return true;
                    }
                    else if ((intersectX > A.X && intersectX < B.X) && (intersectX < C.X && intersectX > D.X))
                    {
                        return true;
                    }
                    else if ((intersectX < A.X && intersectX > B.X) && (intersectX > C.X && intersectX < D.X))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (DivideByZeroException)
                {
                    return false;
                }
            }
            catch (DivideByZeroException)
            {
                return false;
            }
        }

        public static Point[] completePath(Point[] points)
        {

            return points;
        }

    }

    public class evo_optimization
    {

    }

    public class path_format
    {
        public string toString(Point[] points)
        {
            String pathString = null;

            try
            {
                for (int i = 0; i < points.Length; i++)
                {
                    pathString += points[i].X.ToString() + "," + points[i].Y.ToString() + "|";
                }

            }
            catch (System.IndexOutOfRangeException)
            {

            }

            return pathString;
        }
    }

}
