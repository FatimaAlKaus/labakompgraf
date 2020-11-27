using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba_kompgraf
{
    public partial class Form1 : Form
    {
        bool pkm = false;
        Polygon polygon;
        Point selectedPoint = null;
        _3D _3D;
        Polygon curssorPolygon;
        public Form1()
        {
            InitializeComponent();
            this.MouseWheel += Form1_MouseWheel;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint, true);
            UpdateStyles();
            _3D = new _3D();
            polygon = new Polygon();
            _3D.Add(polygon);

        }

        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (pkm)
            {
                if (curssorPolygon != null)
                {
                    if (e.Delta > 0)

                        curssorPolygon.Spin(-10);
                    else
                        curssorPolygon.Spin(10);
                    curssorPolygon.Move(e.X - curssorPolygon.points.First().X, e.Y - curssorPolygon.points.First().Y);
                }
            }
            else
            {
                if (curssorPolygon != null)
                {
                    if (e.Delta > 0)
                    {
                        for (int i = 0; i < curssorPolygon.points.Count; i++)
                        {
                            curssorPolygon.points[i].X = (int)(curssorPolygon.points[i].X * 1.1);
                            curssorPolygon.points[i].Y = (int)(curssorPolygon.points[i].Y * 1.1);
                        }
                    }

                    else
                    {
                        for (int i = 0; i < curssorPolygon.points.Count; i++)
                        {
                            curssorPolygon.points[i].X = (int)(curssorPolygon.points[i].X * 0.9);
                            curssorPolygon.points[i].Y = (int)(curssorPolygon.points[i].Y * 0.9);
                        }
                    }

                }
                curssorPolygon?.Move(e.X - curssorPolygon.points.First().X, e.Y - curssorPolygon.points.First().Y);
            }
            Refresh();

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

            if (curssorPolygon != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    _3D.Add(curssorPolygon.Clone());
                    curssorPolygon = null;
                }
                else
                {
                    pkm = true;
                }
                Refresh();
            }
            else
            {
                foreach (var po in _3D.polygons)
                {
                    foreach (var point in po.points)
                    {
                        if (new Rectangle(point.X - 10, point.Y - 10, 20, 20).Contains(e.X, e.Y))
                        {
                            selectedPoint = point;
                            _3D.Selectd(selectedPoint);
                        }
                    }
                }
                if (selectedPoint == null)
                {
                    if (_3D.polygons.Count == 0)
                    {
                        if (e.Button == MouseButtons.Left)
                        {
                            Polygon polygon = new Polygon();
                            polygon.points.Add(new Point(e.X, e.Y));
                        }
                        else pkm = true;

                    }
                    else
                    {

                        _3D.Add(new Point(e.X, e.Y));

                    }

                }
            }


            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            _3D.Show(e.Graphics);
            //polygon.Show(e.Graphics);
            curssorPolygon?.Show(e.Graphics);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (curssorPolygon != null)
            {
                curssorPolygon.Move(e.X - curssorPolygon.points.First().X, e.Y - curssorPolygon.points.First().Y);
                Refresh();
            }
            else
            {
                if (selectedPoint != null)
                {
                    _3D.Move(e.X, e.Y, e);
                    Refresh();
                }

            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            selectedPoint = null;
            if (e.Button == MouseButtons.Right)
            {
                pkm = false;
            }
            // _3D.Selectd(selectedPoint);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.C))
            {
                curssorPolygon = polygon.Clone(Color.Blue);

                // Refresh();
            }
            if (e.KeyCode == Keys.Delete && selectedPoint != null)
            {
                _3D.Remove(selectedPoint);

                Refresh();
            }
        }
    }
}
