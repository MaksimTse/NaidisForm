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
    public partial class TriangleForm : Form
    {
        private TextBox txtPointA;
        private TextBox txtPointB;
        private TextBox txtPointC;
        private TextBox txtHeight;
        private RadioButton radioSides;
        private RadioButton radioHeight;
        private Button btnDrawTriangle;
        private Button btnClear;
        private ListBox lstTriangleInfo;

        public TriangleForm()
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
            txtPointA.PlaceholderText = "Enter A side";
            txtPointA.Location = new Point(10, 10);
            txtPointA.Size = new Size(100, 20);

            txtPointB = new TextBox();
            txtPointB.PlaceholderText = "Enter B side";
            txtPointB.Location = new Point(10, 40);
            txtPointB.Size = new Size(100, 20);

            txtPointC = new TextBox();
            txtPointC.PlaceholderText = "Enter C side";
            txtPointC.Location = new Point(10, 70);
            txtPointC.Size = new Size(100, 20);

            txtHeight = new TextBox();
            txtHeight.PlaceholderText = "Enter height";
            txtHeight.Location = new Point(10, 100);
            txtHeight.Size = new Size(100, 20);
            txtHeight.Visible = false;

            radioSides = new RadioButton();
            radioSides.Text = "Ввод сторон";
            radioSides.Location = new Point(10, 130);
            radioSides.Checked = true;
            radioSides.CheckedChanged += RadioSides_CheckedChanged;

            radioHeight = new RadioButton();
            radioHeight.Text = "Ввод высоты";
            radioHeight.Location = new Point(10, 160);
            radioHeight.CheckedChanged += RadioHeight_CheckedChanged;

            btnDrawTriangle = new Button();
            btnDrawTriangle.Text = "Отрисовать";
            btnDrawTriangle.Location = new Point(10, 190);
            btnDrawTriangle.Click += btnDrawTriangle_Click;

            btnClear = new Button();
            btnClear.Text = "Очистить";
            btnClear.Location = new Point(10, 220);
            btnClear.Click += btnClear_Click;

            lstTriangleInfo = new ListBox();
            lstTriangleInfo.Location = new Point(10, 250);
            lstTriangleInfo.Size = new Size(300, 100);

            Controls.Add(lstTriangleInfo);
            Controls.Add(btnClear);
            Controls.Add(txtPointA);
            Controls.Add(txtPointB);
            Controls.Add(txtPointC);
            Controls.Add(txtHeight);
            Controls.Add(radioSides);
            Controls.Add(radioHeight);
            Controls.Add(btnDrawTriangle);
        }


        private void btnDrawTriangle_Click(object sender, EventArgs e)
        {
            double pointA, pointB, pointC, height;

            if (radioSides.Checked)
            {
                if (double.TryParse(txtPointA.Text, out pointA) && double.TryParse(txtPointB.Text, out pointB) && double.TryParse(txtPointC.Text, out pointC))
                {
                    if (pointA + pointB > pointC && pointA + pointC > pointB && pointB + pointC > pointA)
                    {
                        lstTriangleInfo.Items.Clear();

                        double centerX = 500;
                        double centerY = 250;
                        double angleA = 0;
                        double angleB = Math.Acos((pointA * pointA + pointC * pointC - pointB * pointB) / (2 * pointA * pointC));
                        double angleC = Math.Acos((pointA * pointA + pointB * pointB - pointC * pointC) / (2 * pointA * pointB));

                        double xA = centerX;
                        double yA = centerY;
                        double xB = xA + pointA;
                        double yB = yA;
                        double xC = centerX + pointC * Math.Cos(angleB);
                        double yC = centerY - pointC * Math.Sin(angleB);

                        Graphics graphics = CreateGraphics();
                        Pen pen = new Pen(Color.Black);
                        graphics.DrawLine(pen, (float)xA, (float)yA, (float)xB, (float)yB);
                        graphics.DrawLine(pen, (float)xB, (float)yB, (float)xC, (float)yC);
                        graphics.DrawLine(pen, (float)xC, (float)yC, (float)xA, (float)yA);

                        Triangle triangle = new Triangle(pointA, pointB, pointC);

                        lstTriangleInfo.Items.Add($"Külg A: {pointA}");
                        lstTriangleInfo.Items.Add($"Külg B: {pointB}");
                        lstTriangleInfo.Items.Add($"Külg C: {pointC}");
                        lstTriangleInfo.Items.Add($"Ümbermõõt: {triangle.Perimeter()}");
                        lstTriangleInfo.Items.Add($"Pindala: {triangle.Surface()}");
                        lstTriangleInfo.Items.Add($"Mediaan: {triangle.Median()}");
                        lstTriangleInfo.Items.Add($"Kõrgus: {triangle.Height()}");
                        lstTriangleInfo.Items.Add($"Kolmnurk tüüp: {DetermineTriangleType(pointA, pointB, pointC)}");
                        lstTriangleInfo.Items.Add($"On olemas: {triangle.ExistTriangle}");
                    }
                    else
                    {
                        MessageBox.Show("Antud külgedega kolmnurka ei ole olemas.");
                    }
                }
                else
                {
                    MessageBox.Show("Palun sisestage kolmnurga külgede õiged väärtused.");
                }
            }
            else
            {
                if (double.TryParse(txtPointA.Text, out pointA) && double.TryParse(txtHeight.Text, out height))
                {
                    double centerX = 500;
                    double centerY = 250;
                    double angleA = 0;
                    double angleB = Math.PI / 2;
                    double angleC = Math.Asin(height / pointA);

                    double xA = centerX;
                    double yA = centerY;
                    double xB = xA + pointA;
                    double yB = yA;
                    double xC = centerX + pointA * Math.Cos(angleC);
                    double yC = centerY - pointA * Math.Sin(angleC);

                    lstTriangleInfo.Items.Clear();

                    Graphics graphics = CreateGraphics();
                    Pen pen = new Pen(Color.Black);
                    graphics.DrawLine(pen, (float)xA, (float)yA, (float)xB, (float)yB);
                    graphics.DrawLine(pen, (float)xB, (float)yB, (float)xC, (float)yC);
                    graphics.DrawLine(pen, (float)xC, (float)yC, (float)xA, (float)yA);

                    Triangle triangle = new Triangle(pointA, pointA, pointA);

                    lstTriangleInfo.Items.Add($"Külg A: {pointA}");
                    lstTriangleInfo.Items.Add($"Külg B: {pointA}");
                    lstTriangleInfo.Items.Add($"Külg C: {pointA}");
                    lstTriangleInfo.Items.Add($"Ümbermõõt: {triangle.Perimeter()}");
                    lstTriangleInfo.Items.Add($"Pindala: {triangle.Surface()}");
                    lstTriangleInfo.Items.Add($"Mediaan: {triangle.Median()}");
                    lstTriangleInfo.Items.Add($"Kõrgus: {triangle.Height()}");
                    lstTriangleInfo.Items.Add($"Kolmnurk tüüp: {DetermineTriangleType(pointA, pointA, pointA)}");
                    lstTriangleInfo.Items.Add($"On olemas: {triangle.ExistTriangle}");
                }
                else
                {
                    MessageBox.Show("Palun sisestage õiged väärtused.");
                }
            }
        }
        private string DetermineTriangleType(double a, double b, double c)
        {
            if (a == b && b == c)
            {
                return "Võrdhaarne kolmnurk";
            }
            else if (a == b || b == c || a == c)
            {
                return "Võrdkülgne kolmnurk";
            }
            else
            {
                return "Erikülgne kolmnurk";
            }
        }
        private void RadioSides_CheckedChanged(object sender, EventArgs e)
        {
            txtPointA.Visible = true;
            txtPointB.Visible = true;
            txtPointC.Visible = true;
            txtHeight.Visible = false;
        }

        private void RadioHeight_CheckedChanged(object sender, EventArgs e)
        {
            txtPointA.Visible = true;
            txtPointB.Visible = false;
            txtPointC.Visible = false;
            txtHeight.Visible = true;
        }
    }
}
