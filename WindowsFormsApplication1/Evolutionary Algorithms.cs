using System;
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
            this.Refresh();
        }

        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            gui_cities.Text = "100";
            gui_workers.Text = "10";
            gv_workers = 10;
            gv_cities = 100;
            this.Refresh();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            fullPath = local_optimization.completePath(fullPath, gv_workers, false);
            this.Refresh();
        }
    }


    public class local_optimization
    {
        public static Point[] singlePath(Point[] points)
        {

            Point split = points[0];
            bool leftEnd = true;         //needed to split the complete Array into paths

            Point[] singlePath = new Point[0];      //will be resized and filled with a part of the complete array

            int leftOffset = 0;             //the index to copy from cpmplete Array; initialized
            int rightOffset = 0;            //the length of the copy from complete Array; initialized

            bool loopBreak = false;
            bool intersectFound = true;

            while (intersectFound)      // while loop for continuing/restarting after array correction
            {
                leftEnd = true;         //initialize splitting variables
                leftOffset = 0;
                rightOffset = 0;

                intersectFound = false;
                loopBreak = false;
                for (int i = 0; i < points.Length; i++) //loop for splitting the complete Array
                {
                    if (loopBreak)
                    {
                        intersectFound = true;
                        singlePath = new Point[0];
                        break;
                    }

                    if (points[i] == split)
                    {
                        if (leftEnd)
                        {
                            leftOffset = i;
                            leftEnd = false;

                        }
                        else
                        {
                            rightOffset = i;
                            //leftEnd = true;

                            singlePath = null;
                            Array.Resize(ref singlePath, rightOffset - leftOffset + 1);
                            Array.Copy(points, leftOffset, singlePath, 0, rightOffset - leftOffset + 1);

                            //leftOffset = i;
                        }
                    }
                    if (singlePath.Length > 3)
                    {
                        for (int j = 0; j < singlePath.Length; j++)     //outer loop for String-inversion inside a path
                        {
                            if (loopBreak)
                            {
                                Array.Copy(singlePath, 0, points, leftOffset, singlePath.Length);
                                break;
                            }
                            for (int k = 0; k < singlePath.Length; k++) //inner string-inversion loop
                            {
                                try
                                {
                                    if (singlePath[j] != singlePath[k] //there cant be intersections between the same 2 or 3 points
                                        & singlePath[j + 1] != singlePath[k]
                                        & singlePath[j] != singlePath[k + 1]
                                        & singlePath[j + 1] != singlePath[k + 1])
                                    {
                                        if (isIntersect(singlePath[j], singlePath[j + 1], singlePath[k], singlePath[k + 1]))
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
                                                Point[] tmp = new Point[Math.Abs(j - (k + 1))];
                                                Array.Copy(singlePath, k + 1, tmp, 0, Math.Abs(j - k));
                                                Array.Reverse(tmp);
                                                Array.Copy(tmp, 0, singlePath, k + 1, Math.Abs(j - k));
                                            }
                                            loopBreak = true;
                                            break;

                                        }
                                        else
                                        {
                                            //intersectFound = false;
                                        } //close IF
                                    } //close IF
                                }
                                catch (IndexOutOfRangeException)
                                {
                                    //intersectFound = false;
                                }
                            } //close inner for-loop (k)
                        } //close outer for-loop (j)
                        leftOffset = rightOffset;
                    } //close IF                
                } //close splitting for-loop (i)
            } // while loop
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

        public static Point[] completePath(Point[] points, int workers, bool optSinglePaths)
        {
            //intersections on different paths have to be changed by swapping points between paths

            try
            {
                bool intersectFound = true;
                bool loopbreak = false;
                while (intersectFound)
                {
                    intersectFound = false;
                    loopbreak = false; // problem: endless loop (toggling points: switching the path on and on)
                    for (int i = 0; i < points.Length; i++)
                    {
                        if (loopbreak){
                            break;
                        }
                        for (int j = 0; j < points.Length; j++)
                        {
                            if (points[i] != points[j] &&           // no intersections possible with 2 or 3 points only
                                    points[i] != points[j + 1] &&
                                    points[i + 1] != points[j] &&
                                    points[i + 1] != points[j + 1])
                            {
                                if (isIntersect(points[i], points[i + 1], points[j], points[j + 1]))
                                {
                                    Point pointI = new Point();
                                    Point pointJ = new Point();
                                    if (isDiffPath(points, i, j))   //check the points are not on the same path
                                    {
                                        intersectFound = true;
                                        pointI = points[i];
                                        pointJ = points[j];
                                        points[i] = pointJ;
                                        points[j] = pointI;
                                        loopbreak = true;
                                        break;
                                    }
                                    else if (isDiffPath(points, i + 1, j))
                                    {
                                        intersectFound = true;
                                        pointI = points[i + 1];
                                        pointJ = points[j];
                                        points[i + 1] = pointJ;
                                        points[j] = pointI;
                                        loopbreak = true;
                                        break;
                                    }
                                    else if (isDiffPath(points, i, j + 1))
                                    {
                                        intersectFound = true;
                                        pointI = points[i];
                                        pointJ = points[j + 1];
                                        points[i] = pointJ;
                                        points[j + 1] = pointI;
                                        loopbreak = true;
                                        break;
                                    }
                                    else if (isDiffPath(points, i + 1, j + 1))
                                    {
                                        intersectFound = true;
                                        pointI = points[i + 1];
                                        pointJ = points[j + 1];
                                        points[i + 1] = pointJ;
                                        points[j + 1] = pointI;
                                        loopbreak = true;
                                        break;
                                    }
                                    else 
                                    {
                                        //single path optimization ??
                                    }
                                } //if (isIntersect)
                            } //if (4 diff points)                        
                        } //inner for-loop (j)
                    } //outer for-loop (i)
                } //while-loop
            }
            catch (IndexOutOfRangeException)
            {

            }

                return points;
        } //static completePath

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
