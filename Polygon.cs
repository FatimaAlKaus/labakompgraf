using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba_kompgraf
{
    class Polygon
    {
        public List<Point> points;
        private Pen pen;
        public Polygon()
        {
            points = new List<Point>();
            pen = new Pen(Color.Red, 1);
        }
        public Polygon(Color color)
        {
            points = new List<Point>();
            pen = new Pen(color, 1);

        }

        public Polygon Clone()
        {
            Polygon polygon = new Polygon();
            foreach (var point in points)
            {
                polygon.points.Add(point.Clone());
            }
            return polygon;
        }
        public Polygon Clone(Color color)
        {
            Polygon polygon = new Polygon(color);
            foreach (var point in points)
            {
                polygon.points.Add(point.Clone());
            }
            return polygon;
        }

        public void Move(int x, int y)
        {
            for (int i = 0; i < points.Count; i++)
            {
                points[i].X += x;
                points[i].Y += y;
            }
        }
        public void Spin(int k)
        {
            double a = Math.PI * k / 180; // Угол поворота
            for (int i = 0; i < points.Count; i++)
            {
                double bx = points[i].X;
                points[i].X = (int)(points[i].X * Math.Cos(a) - points[i].Y * Math.Sin(a));
                points[i].Y = (int)(bx* Math.Sin(a) + points[i].Y * Math.Cos(a));
            }
        }
        public void Show(Graphics g)
        {

            //points = points.OrderBy(p => p.X).ToList();
            //points = points.OrderBy(p => p.Y).ToList();
            foreach (var point in points)
            {
                g.DrawEllipse(pen, point.X - 10, point.Y - 10, 20, 20);
            }
            for (int i = 0; i < points.Count - 1; i++)
            {
                g.DrawLine(pen, points[i].X, points[i].Y, points[i + 1].X, points[i + 1].Y);
            }
            if (points.Count != 0) g.DrawLine(pen, points.First().X, points.First().Y, points.Last().X, points.Last().Y);
        }
    }
}
