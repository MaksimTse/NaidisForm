using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NaidisForm
{
    public partial class TrianglePictureBoxControl : PictureBox
    {
        private double sideA = 3;
        private double sideB = 4;
        private double sideC = 5;

        public TrianglePictureBoxControl()
        {
            InitializeComponent();
        }

        public void SetTriangleSides(double a, double b, double c)
        {
            sideA = a;
            sideB = b;
            sideC = c;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black);

            double p1x = 50;
            double p1y = 150;
            double p2x = p1x + sideA * 10;
            double p2y = p1y;
            double heightX = p1x + (sideA / 2) * 10; 
            double heightY = p1y - (Math.Sqrt(sideB * sideB - (sideA / 2) * (sideA / 2))) * 10;

            Point p1 = new Point((int)p1x, (int)p1y);
            Point p2 = new Point((int)p2x, (int)p2y);
            Point p3 = new Point((int)heightX, (int)heightY);

            g.DrawLine(pen, p1, p2);
            g.DrawLine(pen, p2, p3);
            g.DrawLine(pen, p3, p1);
        }
    }

}
