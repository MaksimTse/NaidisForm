﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace NaidisForm
{
    public partial class MyForm : Form
    {
        private TextBox txtPointA;
        private TextBox txtPointB;
        private TextBox txtPointC;
        private Button btnDrawTriangle;
        private Button btnClear;
        private ListBox lstTriangleInfo;

        public MyForm()
        {
            InitializeComponent();
            InitializeUI();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Invalidate();
            lstTriangleInfo.Items.Clear();
        }

        private void InitializeUI()
        {
            txtPointA = new TextBox();
            txtPointA.Location = new Point(10, 10);
            txtPointA.Size = new Size(100, 20);

            txtPointB = new TextBox();
            txtPointB.Location = new Point(10, 40);
            txtPointB.Size = new Size(100, 20);

            txtPointC = new TextBox();
            txtPointC.Location = new Point(10, 70);
            txtPointC.Size = new Size(100, 20);

            btnDrawTriangle = new Button();
            btnDrawTriangle.Text = "Отрисовать";
            btnDrawTriangle.Location = new Point(10, 100);
            btnDrawTriangle.Click += btnDrawTriangle_Click;

            btnClear = new Button();
            btnClear.Text = "Очистить";
            btnClear.Location = new Point(10, 130);
            btnClear.Click += btnClear_Click;

            lstTriangleInfo = new ListBox();
            lstTriangleInfo.Location = new Point(10, 160);
            lstTriangleInfo.Size = new Size(300, 100);

            Controls.Add(lstTriangleInfo);
            Controls.Add(btnClear);
            Controls.Add(txtPointA);
            Controls.Add(txtPointB);
            Controls.Add(txtPointC);
            Controls.Add(btnDrawTriangle);
        }

        private void btnDrawTriangle_Click(object sender, EventArgs e)
        {
            double pointA, pointB, pointC;
            if (double.TryParse(txtPointA.Text, out pointA) && double.TryParse(txtPointB.Text, out pointB) && double.TryParse(txtPointC.Text, out pointC))
            {
                if (pointA + pointB > pointC && pointA + pointC > pointB && pointB + pointC > pointA)
                {
                    double centerX = 500;
                    double centerY = 250;

                    double angleA = 0;
                    double angleB = Math.Acos((pointA * pointA + pointC * pointC - pointB * pointB) / (2 * pointA * pointC));
                    double angleC = Math.Acos((pointA * pointA + pointB * pointB - pointC * pointC) / (2 * pointA * pointB));

                    double xA = centerX + pointA * Math.Cos(angleA);
                    double yA = centerY - pointA * Math.Sin(angleA);

                    double xB = centerX + pointB * Math.Cos(Math.PI - angleB);
                    double yB = centerY - pointB * Math.Sin(Math.PI - angleB);

                    double xC = centerX + pointC * Math.Cos(Math.PI - angleC);
                    double yC = centerY - pointC * Math.Sin(Math.PI - angleC);

                    Graphics graphics = CreateGraphics();
                    Pen pen = new Pen(Color.Black);
                    graphics.DrawLine(pen, (float)xA, (float)yA, (float)xB, (float)yB);
                    graphics.DrawLine(pen, (float)xB, (float)yB, (float)xC, (float)yC);
                    graphics.DrawLine(pen, (float)xC, (float)yC, (float)xA, (float)yA);

                    Triangle triangle = new Triangle(pointA, pointB, pointC);

                    lstTriangleInfo.Items.Clear();
                    lstTriangleInfo.Items.Add($"Külg A: {pointA}");
                    lstTriangleInfo.Items.Add($"Külg B: {pointB}");
                    lstTriangleInfo.Items.Add($"Külg C: {pointC}");
                    lstTriangleInfo.Items.Add($"Primeter: {triangle.Perimeter()}");
                    lstTriangleInfo.Items.Add($"Pindala: {triangle.Surface()}");
                    lstTriangleInfo.Items.Add($"Mediaan: {triangle.Median()}");
                    lstTriangleInfo.Items.Add($"Kõrgus: {triangle.Height()}");
                    lstTriangleInfo.Items.Add($"Olemas?: {triangle.ExistTriangle}");
                }
                else
                {
                    MessageBox.Show("Треугольник с заданными сторонами не существует.");
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите корректные значения сторон треугольника.");
            }
        }
        class Triangle
        {

            public double a;
            public double b;
            public double c;
            public double height;
            public double median;
            public Triangle(double A, double B, double C)
            {
                a = A;
                b = B;
                c = C;
            }

            public string outputA()
            {
                return Convert.ToString(a);
            }
            public string outputB()
            {
                return Convert.ToString(b);
            }
            public string outputC()
            {
                return Convert.ToString(c);
            }
            public double Perimeter()
            {
                double p = 0;
                p = a + b + c;
                return p;
            }
            public double Surface()
            {
                double s = 0;
                double p = 0;
                p = (a + b + c) / 2;
                s= Math.Sqrt((p * (p - a) + (p - b) + (p - c)));
                return s;
            }
            public double Height()
            {
                double s = Surface();
                double height = 2 * s / a;
                return height;
            }
            public double Median()
            {
                median = 0.5 * a;
                return median;
            }

            public double GetSetA
            {
                get
                { return a; }
                set
                { a = value; }
            }
            public double GetSetB
            {
                get
                { return b; }
                set
                { b = value; }
            }
            public double GetSetC
            {
                get
                { return c; }
                set
                { c = value; }
            }
            public bool ExistTriangle
            {
                get
                {
                    if ((a + b < c) && (b + c < a) && (a + c < b))
                    return false;
                    else return true;
                }
            }
        }
    }
}