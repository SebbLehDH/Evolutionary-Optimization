using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

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
            localOptBox.SelectedIndex = 0;
            gui_evo_strategy.SelectedIndex = 0;
            gui_cb_mutation.Checked = true;
            gui_cb_recomb.Checked = true;
            chart.Series.Clear();
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

            //chart.Series.Clear();
            //Series path_series = new Series("Optimization");
            //path_series.Points.AddXY(1, gv_totalLength);
            //chart.Series.Add(path_series);

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
                //gv_cities = 0;
                //gv_workers = 0;
                MessageBox.Show(gv_NoIntMessage);
            }
            finally
            {
                if (gv_cities <= 0 | gv_workers <= 0)
                {
                    MessageBox.Show(gv_NoIntMessage);
                }

                else if (gv_workers > gv_cities)
                {
                    MessageBox.Show("Numbers of Points should be higher or equal to the number of paths");
                }
                else
                {

                    fullPath = initialization.path(gv_cities, gv_workers, panel1.Width, panel1.Height);

                    gv_totalLength = 0;
                    gv_avgLength = 0;
                    localGo.Enabled = true;
                    evoGo.Enabled = true;
                    this.Refresh();
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //fullPath = local_optimization.singlePath(fullPath);
            //this.Refresh();
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

                Array.Resize(ref fullPath, fullPath.Length + 1);
                fullPath[fullPath.Length - 1].X = midX;
                fullPath[fullPath.Length - 1].Y = midY;
                return fullPath;
            }

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Evo-Path|*.evo";
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
                MessageBox.Show("Fehler beim Speichern!");
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.IO.Stream fs = null;
            System.IO.StreamReader fr = null;

            path_format format = new path_format();

            String pathString = null;

            OpenFileDialog openFile = new OpenFileDialog();
            //openFile.InitialDirectory = "C:\\";
            openFile.Filter = "Evo-Path|*.evo";
            openFile.Title = "Open Path";
            openFile.RestoreDirectory = false;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                localGo.Enabled = true;
                try
                {
                    if (openFile.OpenFile() != null)
                    {
                        using (fs = openFile.OpenFile())
                        {
                            fr = new System.IO.StreamReader(openFile.FileName);
                            pathString = fr.ReadToEnd();
                            fullPath = format.toPoints(pathString);
                            gv_workers = 0;
                            gv_cities = 0;
                            for (int i = 0; i < fullPath.Length; i++)
                            {
                                if (fullPath[i].X == midX && fullPath[i].Y == midY)
                                {
                                    gv_workers++;
                                }
                                else
                                {
                                    gv_cities++;
                                }
                            }
                            gv_workers--;
                            gui_cities.Text = gv_cities.ToString();
                            gui_workers.Text = gv_workers.ToString();
                            this.Refresh();
                        }
                    }

                }
                catch (System.IO.IOException ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. " + "Original error: " + ex.Message); 

                }
            }
            if (fs != null)
            {
                fs.Close();
            }
            if (fr != null)
            {
                fr.Close();
            }
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gv_cities = 0;
            gv_workers = 0;
            gui_cities.Text = "";
            gui_workers.Text = "";
            gui_avgLength.Text = "";
            gui_totalLength.Text = "";
            localGo.Enabled = false;
            evoGo.Enabled = false;
            chart.Series.Clear();
            this.Refresh();
        }

        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gui_cities.Text = "100";
            gui_workers.Text = "10";
            gv_workers = 10;
            gv_cities = 100;
            fullPath = initialization.path(gv_cities, gv_workers, panel1.Width, panel1.Height);
            localGo.Enabled = true;
            evoGo.Enabled = true;
            chart.Series.Clear();
            this.Refresh();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //fullPath = local_optimization.completePath(fullPath, gv_workers, false);
            //this.Refresh();
        }

        private void seeDocumentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("In progress");
        }

        private void helpWhereIAmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("In progress");
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            //if (localOptBox.SelectedIndex == 0)         //complete optimization
            //{
            //    fullPath = local_optimization.completePath(fullPath, gv_workers, true);
            //    this.Refresh();
            //}
            //else if (localOptBox.SelectedIndex == 1)    //string inversion only
            //{
            //    fullPath = local_optimization.singlePath(fullPath);
            //    this.Refresh();
            //}
            //else if (localOptBox.SelectedIndex == 2)    //swap only
            //{
            //    fullPath = local_optimization.completePath(fullPath, gv_workers, false);
            //    this.Refresh();
            //}
        }

        private void gui_cb_recomb_CheckedChanged(object sender, EventArgs e)
        {
            if (gui_cb_recomb.Checked == false && gui_cb_mutation.Checked == false)
            {
                MessageBox.Show("At least one of the following must be checked: recombination or mutation");
                gui_cb_recomb.Checked = true;
            }
        }

        private void gui_cb_mutation_CheckedChanged(object sender, EventArgs e)
        {
            if (gui_cb_recomb.Checked == false && gui_cb_mutation.Checked == false)
            {
                MessageBox.Show("At least one of the following must be checked: recombination or mutation");
                gui_cb_mutation.Checked = true;
            }
        }

        private void evoGo_Click(object sender, EventArgs e)
        {
            int popSize = 0;
            int popGrowth = 0;

            try
            {
                popSize = int.Parse(gui_evo_popu.Text);
                popGrowth = int.Parse(gui_evo_growth.Text);
            }
            catch (Exception)
            {
                MessageBox.Show(gv_NoIntMessage, "Evolution Strategies");
            }

            if (gui_evo_strategy.SelectedIndex == 0) // (μ+1)-strategy
            {
                MessageBox.Show("Population growth is set to 1", "(μ+1)-strategy");
                gui_evo_growth.Text = "1";
                popGrowth = 1;
            }
            else
            {
                string msgTitle = "";
                if (gui_evo_strategy.SelectedIndex == 1) // (μ+λ)-strategy
                {
                    msgTitle = "(μ+λ)-strategy";
                }
                else if (gui_evo_strategy.SelectedIndex == 2) // (μ,λ)-strategy
                {
                    msgTitle = "(μ,λ)-strategy";
                }
                if (popGrowth < popSize)
                {
                    MessageBox.Show("Number of offspring (growth) must be higher than population size", msgTitle);
                }
                else
                {
                    //start optimization
                    evo_optimization evo_opt = new evo_optimization();
                    
                    evo_opt.optimize(popSize, popGrowth, 1, fullPath);

                }
            }
        }

    }


    public class local_optimization
    {
        //public static Point[] singlePath(Point[] points)
        //{

        //    Point split = points[0];
        //    bool leftEnd = true;         //needed to split the complete Array into paths

        //    Point[] singlePath = new Point[0];      //will be resized and filled with a part of the complete array

        //    int leftOffset = 0;             //the index to copy from cpmplete Array; initialized
        //    int rightOffset = 0;            //the length of the copy from complete Array; initialized

        //    bool loopBreak = false;
        //    bool intersectFound = true;

        //    while (intersectFound)      // while loop for continuing/restarting after array correction
        //    {
        //        leftEnd = true;         //initialize splitting variables
        //        leftOffset = 0;
        //        rightOffset = 0;

        //        intersectFound = false;
        //        loopBreak = false;
        //        for (int i = 0; i < points.Length; i++) //loop for splitting the complete Array
        //        {
        //            if (loopBreak)
        //            {
        //                intersectFound = true;
        //                singlePath = new Point[0];
        //                break;
        //            }

        //            if (points[i] == split)
        //            {
        //                if (leftEnd)
        //                {
        //                    leftOffset = i;
        //                    leftEnd = false;

        //                }
        //                else
        //                {
        //                    rightOffset = i;
        //                    //leftEnd = true;

        //                    singlePath = null;
        //                    Array.Resize(ref singlePath, rightOffset - leftOffset + 1);
        //                    Array.Copy(points, leftOffset, singlePath, 0, rightOffset - leftOffset + 1);

        //                    //leftOffset = i;
        //                }
        //            }
        //            if (singlePath.Length > 3)
        //            {
        //                for (int j = 0; j < singlePath.Length; j++)     //outer loop for String-inversion inside a path
        //                {
        //                    if (loopBreak)
        //                    {
        //                        Array.Copy(singlePath, 0, points, leftOffset, singlePath.Length);
        //                        break;
        //                    }
        //                    for (int k = 0; k < singlePath.Length; k++) //inner string-inversion loop
        //                    {
        //                        try
        //                        {
        //                            if (singlePath[j] != singlePath[k] //there cant be intersections between the same 2 or 3 points
        //                                & singlePath[j + 1] != singlePath[k]
        //                                & singlePath[j] != singlePath[k + 1]
        //                                & singlePath[j + 1] != singlePath[k + 1])
        //                            {
        //                                if (isIntersect(singlePath[j], singlePath[j + 1], singlePath[k], singlePath[k + 1]))
        //                                {
        //                                    if (k > j)
        //                                    {
        //                                        Point[] tmp = new Point[Math.Abs(k - j)];
        //                                        Array.Copy(singlePath, j + 1, tmp, 0, Math.Abs(k - j));
        //                                        Array.Reverse(tmp);
        //                                        Array.Copy(tmp, 0, singlePath, j + 1, Math.Abs(k - j));
        //                                    }
        //                                    else if (j > k)
        //                                    {
        //                                        Point[] tmp = new Point[Math.Abs(j - (k + 1))];
        //                                        Array.Copy(singlePath, k + 1, tmp, 0, Math.Abs(j - k));
        //                                        Array.Reverse(tmp);
        //                                        Array.Copy(tmp, 0, singlePath, k + 1, Math.Abs(j - k));
        //                                    }
        //                                    loopBreak = true;
        //                                    break;

        //                                }
        //                                else
        //                                {
        //                                    //intersectFound = false;
        //                                } //close IF
        //                            } //close IF
        //                        }
        //                        catch (IndexOutOfRangeException)
        //                        {
        //                            //intersectFound = false;
        //                        }
        //                    } //close inner for-loop (k)
        //                } //close outer for-loop (j)
        //                leftOffset = rightOffset;
        //            } //close IF                
        //        } //close splitting for-loop (i)
        //    } // while loop
        //        return points;
        //} //close method singlePath

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
                    mAB = Convert.ToSingle(B.Y - A.Y) / Convert.ToSingle(B.X - A.X);        //divide by zero! -> vertical line
                    cAB = (A.Y - mAB * A.X);                                                //SOLUTION: rotate by 90° 

                    //line C->D
                    mCD = Convert.ToSingle(D.Y - C.Y) / Convert.ToSingle(D.X - C.X);        //divide by zero! -> vertical line
                    cCD = (C.Y - mCD * C.X);                                                //see above
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

        //public static Point[] completePath(Point[] points, int workers, bool optSinglePaths)
        //{
        //    bool intersectFound = true;
        //    while (intersectFound)
        //    {
        //        intersectFound = false;
        //        int[,] swapped = new int[0, 2];
        //        try
        //        {
        //            for (int i = 0; i < points.Length; i++)
        //            {
        //                try
        //                {

        //                    for (int j = 0; j < points.Length; j++)
        //                    {
        //                        if (points[i] != points[j] &&           // no intersections possible with 2 or 3 points only
        //                                points[i] != points[j + 1] &&
        //                                points[i + 1] != points[j] &&
        //                                points[i + 1] != points[j + 1])
        //                        {
        //                            if (isIntersect(points[i], points[i + 1], points[j], points[j + 1]))
        //                            {
        //                                intersectFound = true;
        //                                bool isSwapped = false;
        //                                Point pointI = new Point();
        //                                Point pointJ = new Point();
        //                                if (isDiffPath(points, i, j))   //check the points are not on the same path
        //                                {
        //                                    for (int k = 0; k < swapped.Length / 2; k++) //check if these points (i, j) have been swapped before
        //                                    {
        //                                        if ((swapped[k, 0] == i && swapped[k, 1] == j) ||
        //                                            (swapped[k, 0] == j && swapped[k, 1] == i))
        //                                        {
        //                                            isSwapped = true;
        //                                        }
        //                                    }
        //                                    if (!isSwapped)
        //                                    {
        //                                        pointI = points[i];
        //                                        pointJ = points[j];
        //                                        points[i] = pointJ;
        //                                        points[j] = pointI;
        //                                        int[,] tmp = new int[swapped.Length / 2 + 1, 2];
        //                                        Array.Copy(swapped, tmp, swapped.Length);
        //                                        swapped = tmp;
        //                                        swapped[swapped.Length / 2 - 1, 0] = i;     //save swap index to prevent 'reswapping' the same points
        //                                        swapped[swapped.Length / 2 - 1, 1] = j;

        //                                        isSwapped = false;
        //                                    }
        //                                }
        //                                else if (isDiffPath(points, i + 1, j))
        //                                {
        //                                    for (int k = 0; k < swapped.Length / 2; k++) //check if these points (i, j) have been swapped before
        //                                    {
        //                                        if ((swapped[k, 0] == i + 1 && swapped[k, 1] == j) ||
        //                                            (swapped[k, 0] == j && swapped[k, 1] == i + 1))
        //                                        {
        //                                            isSwapped = true;
        //                                        }
        //                                    }
        //                                    if (!isSwapped)
        //                                    {
        //                                        pointI = points[i + 1];
        //                                        pointJ = points[j];
        //                                        points[i + 1] = pointJ;
        //                                        points[j] = pointI;
        //                                        int[,] tmp = new int[swapped.Length / 2 + 1, 2];
        //                                        Array.Copy(swapped, tmp, swapped.Length);
        //                                        swapped = tmp;
        //                                        swapped[swapped.Length / 2 - 1, 0] = i + 1;
        //                                        swapped[swapped.Length / 2 - 1, 1] = j;
        //                                        isSwapped = false;
        //                                    }
        //                                }
        //                                else if (isDiffPath(points, i, j + 1))
        //                                {
        //                                    for (int k = 0; k < swapped.Length / 2; k++) //check if these points (i, j) have been swapped before
        //                                    {
        //                                        if ((swapped[k, 0] == i && swapped[k, 1] == j + 1) ||
        //                                            (swapped[k, 0] == j + 1 && swapped[k, 1] == i))
        //                                        {
        //                                            isSwapped = true;
        //                                        }
        //                                    }
        //                                    if (!isSwapped)
        //                                    {
        //                                        pointI = points[i];
        //                                        pointJ = points[j + 1];
        //                                        points[i] = pointJ;
        //                                        points[j + 1] = pointI;
        //                                        int[,] tmp = new int[swapped.Length / 2 + 1, 2];
        //                                        Array.Copy(swapped, tmp, swapped.Length);
        //                                        swapped = tmp;
        //                                        swapped[swapped.Length / 2 - 1, 0] = i;
        //                                        swapped[swapped.Length / 2 - 1, 1] = j + 1;
        //                                        isSwapped = false;
        //                                    }
        //                                }
        //                                else if (isDiffPath(points, i + 1, j + 1))
        //                                {
        //                                    for (int k = 0; k < swapped.Length / 2; k++) //check if these points (i, j) have been swapped before
        //                                    {
        //                                        if ((swapped[k, 0] == i + 1 && swapped[k, 1] == j + 1) ||
        //                                            (swapped[k, 0] == j + 1 && swapped[k, 1] == i + 1))
        //                                        {
        //                                            isSwapped = true;
        //                                        }
        //                                    }
        //                                    if (!isSwapped)
        //                                    {
        //                                        pointI = points[i + 1];
        //                                        pointJ = points[j + 1];
        //                                        points[i + 1] = pointJ;
        //                                        points[j + 1] = pointI;
        //                                        int[,] tmp = new int[swapped.Length / 2 + 1, 2];
        //                                        Array.Copy(swapped, tmp, swapped.Length);
        //                                        swapped = tmp;
        //                                        swapped[swapped.Length / 2 - 1, 0] = i + 1;
        //                                        swapped[swapped.Length / 2 - 1, 1] = j + 1;
        //                                        isSwapped = false;
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    if (optSinglePaths)
        //                                    {
        //                                        //single path ontimization
        //                                        if (points[i] != points[0] && points[j] != points[0])
        //                                        {
        //                                            if (j > i)
        //                                            {
        //                                                Point[] tmp = new Point[(j - i)];
        //                                                Array.Copy(points, i + 1, tmp, 0, (j - i));
        //                                                Array.Reverse(tmp);
        //                                                Array.Copy(tmp, 0, points, i + 1, (j - i));
        //                                            }
        //                                            else if (i > j)
        //                                            {
        //                                                Point[] tmp = new Point[(i - j + 1)];
        //                                                Array.Copy(points, j, tmp, 0, (i - j + 1));
        //                                                Array.Reverse(tmp);
        //                                                Array.Copy(tmp, 0, points, j, (i - j + 1));
        //                                            }
        //                                        } //if
        //                                    } //if
        //                                } //if
        //                            } //if (isIntersect)
        //                        } //if (4 diff points)                        
        //                    } //inner for-loop (j)
        //                }
        //                catch (IndexOutOfRangeException)
        //                {
        //                    intersectFound = false;
        //                }
        //            } //outer for-loop (i)
        //        }
        //        catch (IndexOutOfRangeException)
        //        {
        //            intersectFound = false;
        //        }
        //    } //while-loop

        //    return points;

        //} //static completePath

        public static bool isDiffPath(Point[] points, int indexA, int indexB)
        {
            try
            {
                if (points[indexA] == points[0])
                {
                    return false;
                }
                if (points[indexB] == points[0])
                {
                    return false;
                }

                Point[] localPoints = new Point[Math.Abs(indexA - indexB)];
                if (indexA > indexB)
                {
                    Array.Copy(points, indexB, localPoints, 0, localPoints.Length);
                }
                else if (indexA < indexB)
                {
                    Array.Copy(points, indexA, localPoints, 0, localPoints.Length);                    
                }

                for (int i = 0; i < localPoints.Length; i++)
                {
                    if (localPoints[i] == points[0])
                    {
                        return true;
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                //is there a correct array of points?
                //return false;
            }
            return false;
        }

    }

    public class evo_optimization
    {
        private int popSize;
        private int popGrowth;
        private int strategy;
        private Point[] brace = new Point[1];
        private Point[] first_individual;

        private Point[] population = new Point[0];
        private Point[] offsprings = new Point[0];

        private Double[] pop_evaluation = new Double[0];
        private Double[] offspr_evalualtion = new Double[0];

        private Random rnd = new Random();

        public void optimize(int popSize, int popGrowth, int strategy, Point[] points)
        {
            this.popSize = popSize;
            this.popGrowth = popGrowth;
            this.strategy = strategy;

            first_individual = points;

            create_population(points);          // create initial population of {greek-My} (popSize) individuals

            evaluate(population);

            create_offsprings();

            Double[] tmp_eval = pop_evaluation;
            evaluate(offsprings);
            offspr_evalualtion = pop_evaluation;
            pop_evaluation = tmp_eval;

            select_best();

        }

        public void create_population(Point[] points)
        {
        brace[0] = new Point(-1, -1);
            int oldLength = population.Length;
            Array.Resize(ref population, population.Length + points.Length);
            Array.Copy(points, 0, population, oldLength, points.Length);

            for (int i = 0; i < (popSize - 1); i++)
            {
                Point[] new_points = new Point[points.Length];
                oldLength = population.Length;
                Array.Resize(ref population, population.Length + 1);
                Array.Copy(brace, 0, population, oldLength, 1);
                new_points = reconnect_points(points);
                oldLength = population.Length;
                Array.Resize(ref population, population.Length + new_points.Length);
                Array.Copy(new_points, 0, population, oldLength, new_points.Length);
            }
        }

        private void create_offsprings()
        {
            Point[] offspring = new Point[0];

            Point[] pointsA = new Point[0];
            Point[] pointsB = new Point[0];
            for (int i = 0; i < popGrowth; i++)
            {
                int parentA = rnd.Next(popSize);
                int parentB = 0;
                Boolean random_bool = true;
                while (random_bool)
                {
                    parentB = rnd.Next(popSize-1);
                    if (parentA != parentB)
                    {
                        random_bool = false;
                    }
                }
                int k = 0;
                int start = 0;
                int m = 0 ;
                Array.Resize(ref pointsA, 0);
                Array.Resize(ref pointsB, 0);
                for (m = 0; m < population.Length; m++)
                {
                    if (population[m].X == -1)
                    {
                        //split
                        Point[] individual = new Point[m-start];
                        Array.Copy(population, start, individual, 0, individual.Length);
                        if (parentA == k) { pointsA = individual; }
                        if (parentB == k) { pointsB = individual; }
                        start = m+1;
                        k++;
                    }
                }
                if (pointsA.Length == 0) 
                {
                    Array.Resize(ref pointsA, population.Length - start);
                    Array.Copy(population, start, pointsA, 0, population.Length - start); 
                }
                if (pointsB.Length == 0) 
                {
                    Array.Resize(ref pointsB, population.Length - start);
                    Array.Copy(population, start, pointsB, 0, population.Length - start);
                }

                offspring = order_crossover(pointsA, pointsB);
                offspring = swap(offspring);
                int oldLength = offsprings.Length;
                Array.Resize(ref offsprings, offsprings.Length + offspring.Length+1);
                Array.Copy(offspring, 0, offsprings, oldLength, offspring.Length);
                offsprings[offsprings.Length - 1] = new Point(-1, -1);
            }

            //offspring = swap(offspring);
        }

        private Point[] order_crossover(Point[] pointsA, Point[] pointsB)
        {
            Point[] offspring = new Point[pointsA.Length];
            Point[] tmp = new Point[0];

            int cutA = rnd.Next(pointsA.Length);
            int cutB = 0;
            Boolean cut = true;
            while (cut)
            {
                cutB = rnd.Next(pointsA.Length);
                if (cutA != cutB) { cut = false; }
            }
            if (cutA < cutB) 
            { 
                Array.Copy(pointsA, cutA, offspring, cutA, cutB - cutA);
                Array.Resize(ref tmp, cutB - cutA);
                Array.Copy(pointsA, cutA, tmp, 0, cutB - cutA);
            }
            else if (cutB < cutA) 
            {
                Array.Copy(pointsA, cutB, offspring, cutB, cutA - cutB);
                Array.Resize(ref tmp, cutA - cutB);
                Array.Copy(pointsA, cutB, tmp, 0, cutA - cutB);
            }
            int tmp2i = 0;
            for (int i = 0; i < pointsB.Length; i++)
            {
                int tmpI = i;
                if ((offspring[i].X == 0) && (offspring[i].Y == 0) )
                {
                    if (pointsB[tmp2i] != pointsB[0])
                    {
                        Boolean cross = true;
                        while (cross)
                        {
                            cross = false;
                            for (int j = 0; j < offspring.Length; j++)
                            {
                                try
                                {
                                    if (pointsB[tmp2i] == offspring[j])
                                    {
                                        //i++;
                                        tmp2i++;
                                        cross = true;
                                        break;
                                        //Punkt schon einmal in offspring
                                    }
                                }
                                catch (System.IndexOutOfRangeException)
                                {
                                    tmp2i--;
                                    cross = false;
                                    break;
                                }
                            }
                            if (!cross)
                            {
                                offspring[tmpI] = pointsB[tmp2i];
                            }
                        }
                    }
                    else
                    {
                        offspring[tmp2i] = pointsB[tmp2i];
                    }
                    tmp2i++;
                }
                i = tmpI;
            }
            //offspring[0] = pointsA[0];
            //offspring[offspring.Length - 1] = pointsA[0];
                return offspring;
        }

        private Point[] swap(Point[] points)
        {
            Point tmpA = new Point();
            Point tmpB = new Point();
            int pointA = 0;
            int pointB = 0;

            Boolean swap = true;
            while (swap)
            {
                pointA = rnd.Next(points.Length);
                if (points[pointA] != points[0])
                {
                    swap = false;
                }
                Boolean diverse = true;
                while (diverse)
                {
                    pointB = rnd.Next(points.Length);
                    if ((pointA != pointB) && (points[pointB] != points[0]))
                    {
                        diverse = false;
                    }
                }
            }
            tmpA = points[pointA];
            tmpB = points[pointB];
            points[pointA] = tmpB;
            points[pointB] = tmpA;

            return points;
        }

        private void evaluate(Point[] population)
        {
            Point[] eval_points = new Point[0];
            int j = 0;
            // split collection at [-1, -1]
            for (int i = 0; i < population.Length; i++)
            {
                if (population[i].X == -1)
                {
                    Array.Resize(ref pop_evaluation, pop_evaluation.Length + 1);
                    pop_evaluation[j] = eval_individual(eval_points);
                    Array.Resize(ref eval_points, 0);
                    j++;
                }
                else if (i==population.Length - 1)
                {
                    Array.Resize(ref eval_points, eval_points.Length + 1);
                    eval_points[eval_points.Length - 1] = eval_points[0];
                    Array.Resize(ref pop_evaluation, pop_evaluation.Length + 1);
                    pop_evaluation[j] = eval_individual(eval_points);
                    Array.Resize(ref eval_points, 0);
                    j++;

                }
                else
                {
                    Array.Resize(ref eval_points, eval_points.Length + 1);
                    Array.Copy(population, i, eval_points, eval_points.Length-1, 1);
                }
            }
        }

        private Double eval_individual(Point[] points)
        {
            Double length = 0;
            for (int i = 0; i < points.Length - 1; i++)
            {
                length += Math.Sqrt(Math.Pow(points[i].X - points[i + 1].X, 2) + Math.Pow(points[i].Y - points[i + 1].Y, 2));
            }
            return length;
        }

        public Point[] reconnect_points(Point[] points)
        {
            Point mid = new Point(points[0].X, points[0].Y);
            Point[] scramble_points = new Point[0];
            int[] ret_points = new int[0];
            
            for (int i = 0; i < points.Length; i++)
            {
                if (points[i] == mid)
                {
                    //save indexes for later insertion
                    Array.Resize(ref ret_points, (ret_points.Length+1));
                    ret_points[ret_points.Length-1] = i;
                }
                else
                {
                    Array.Resize(ref scramble_points, (scramble_points.Length + 1));
                    scramble_points[scramble_points.Length-1] = points[i];
                }
            }

            // shuffle points
            for (int i = 0; i < scramble_points.Length; i++)
            {
                int rnd_index1 = rnd.Next(scramble_points.Length);
                int rnd_index2 = rnd.Next(scramble_points.Length);
                Point tmp2 = new Point(scramble_points[rnd_index1].X, scramble_points[rnd_index1].Y);
                Point tmp1 = new Point(scramble_points[rnd_index2].X, scramble_points[rnd_index2].Y);
                scramble_points[rnd_index2] = tmp2;
                scramble_points[rnd_index1] = tmp1;
            }
            
            //insert mid points
            for (int i = 0; i < ret_points.Length; i++)
            {
                Point[] tmp = new Point[scramble_points.Length - ret_points[i]]; // second part
                Array.Copy(scramble_points, ret_points[i], tmp, 0, scramble_points.Length - ret_points[i]);
                //Array.Copy(scramble_points, tmp, scramble_points.Length);
                Array.Resize(ref scramble_points, scramble_points.Length + 1);
                scramble_points[ret_points[i]] = mid;
                Array.Copy(tmp, 0, scramble_points, ret_points[i] + 1, tmp.Length);

            }
                return scramble_points;
        }

        private void select_best()
        {

        }
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
                    if (i == points.Length - 1)
                    {
                        pathString += points[i].X.ToString() + "," + points[i].Y.ToString() + ";";
                    }
                    else
                    {
                        pathString += points[i].X.ToString() + "," + points[i].Y.ToString() + "|";
                    }
                }

            }
            catch (System.IndexOutOfRangeException)
            {

            }

            return pathString;
        }

        public Point[] toPoints(String pathString)
        {
            Point[] fullPath = new Point[1];
            string point = "";

            bool newPoint = true;
            int i = 0;

            while (newPoint)
            {
                if (pathString.Length != 0)
                {
                    try
                    {
                        char[] token = new char[1];
                        pathString.CopyTo(0, token, 0, 1);
                        pathString = pathString.Remove(0, 1);
                        if (token[0].ToString() == ",")
                        {
                            fullPath[i].X = int.Parse(point);
                            point = "";
                        }
                        else if (token[0].ToString() == "|")
                        {
                            fullPath[i].Y = int.Parse(point);
                            point = "";
                            i++;
                            Array.Resize(ref fullPath, i+1);
                        }
                        else if (token[0].ToString() == ";")
                        {
                            fullPath[i].Y = int.Parse(point);
                            newPoint = false;
                        }
                        else
                        {
                            point += token[0].ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Problem beim Parsen vom String:" + ex.Message);
                    }
                }
            }

            return fullPath;
        }
    }

}
