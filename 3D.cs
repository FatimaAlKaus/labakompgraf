using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba_kompgraf
{
    class _3D
    {
        public List<Polygon> polygons;
        public List<Point> selectedPoints = new List<Point>();
        Point point;
        public _3D()
        {
            polygons = new List<Polygon>();
        }
        public void Add(Polygon polygon)
        {
            polygons.Add(polygon);
        }
        public void Remove(Point point)
        {
            Selectd(point);
            for (int i = 0; i < polygons.Count; i++)
            {
                for (int j = 0; j < polygons[i].points.Count; j++)
                {
                    if (selectedPoints.Contains(polygons[i].points[j]))
                    {
                        polygons[i].points.Remove(polygons[i].points[j]);
                        if (polygons[i].points.Count == 0)
                        {
                            polygons.Remove(polygons[i]);
                        }
                    }
                }
            }
        }
        public void Selectd(Point selPoint)
        {
            point = selPoint;
            selectedPoints = new List<Point>();
            if (selectedPoints != null)
            {

                int k = 0;
                for (int j = 0; j < polygons.Count; j++)
                {
                    for (int i = 0; i < polygons[j].points.Count; i++)
                    {
                        if (selPoint == polygons[j].points[i])
                        {
                            k = i;

                        }
                    }
                }
                for (int i = 0; i < polygons.Count; i++)
                {
                    selectedPoints.Add(polygons[i].points[k]);
                }
            }
            else
            {
                selectedPoints = null;
            }
        }
        public void Move(int x, int y,MouseEventArgs e)
        {
           
            if (e.Button == MouseButtons.Right)
            {
                if (selectedPoints != null)
                {

                    var dx = x - point.X;
                    var dy = y - point.Y;


                    for (int i = 0; i < selectedPoints.Count; i++)
                    {
                        if (selectedPoints[i] != point)
                        {
                            selectedPoints[i].X += dx;
                            selectedPoints[i].Y += dy;
                        }
                        else
                        {
                            selectedPoints[i].X = x;
                            selectedPoints[i].Y = y;
                        }
                    }
                }
            }
            if (e.Button == MouseButtons.Left)
            {
                point.X = x;
                point.Y = y;
            }

        }
        public void Add(Point point)
        {
            //if(polygons.First().points.Count==0)
            polygons.First().points.Add(point);

            for (int i = 1; i < polygons.Count; i++)
            {
                var dx = -polygons.First().points.First().X + polygons[i].points.First().X;
                var dy = -polygons.First().points.First().Y + polygons[i].points.First().Y;

                polygons[i].points.Add(new Point(point.X + dx, point.Y + dy));
            }

        }
        public void Show(Graphics graphics)
        {
            foreach (var polygon in polygons)
            {
                polygon.Show(graphics);
            }
            for (int j = 0; j < polygons.Count - 1; j++)
            {
                for (int i = 0; i < polygons.First().points.Count; i++)
                {
                    graphics.DrawLine(new Pen(Color.Black, 1), polygons[j].points[i].ToPoint(), polygons[j + 1].points[i].ToPoint());
                }
            }
        }
    }
}
