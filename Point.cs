using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace laba_kompgraf
{
    class Point
    {
        
        public Point(int x,int y)
        {
            X = x;
            Y = y;
        }
        public Point Clone()
        {
            return new Point(X, Y);
        }
        public System.Drawing.Point ToPoint()
        {
            return new System.Drawing.Point(X, Y);
        }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
