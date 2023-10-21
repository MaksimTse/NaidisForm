using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NaidisForm
{
    public partial class TriangleForm2 : Form
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
        private Button btnSaveToFile;

        public TriangleForm2()
        {
            InitializeComponent();
            InitializeUI();

            this.BackColor = Color.Pink;

            // btnSize
            btnSaveToFile.Size = new Size(100, 30);
            btnClear.Size = new Size(100, 30);
            btnDrawTriangle.Size = new Size(100, 30);

            // btnPlace
            btnSaveToFile.Location = new Point(230, 220);
            btnClear.Location = new Point(120, 220);
            btnDrawTriangle.Location = new Point(10, 220);

            // btnBG
            btnSaveToFile.BackColor = Color.Pink;
            btnClear.BackColor = Color.Pink;
            btnDrawTriangle.BackColor = Color.Pink;

            // btnFG
            btnSaveToFile.ForeColor = Color.White;
            btnClear.ForeColor = Color.White;
            btnDrawTriangle.ForeColor = Color.White;

            // txtBG
            txtPointA.BackColor = Color.LightPink;
            txtPointB.BackColor = Color.LightPink;
            txtPointC.BackColor = Color.LightPink;
            txtHeight.BackColor = Color.LightPink;

            // txtFG
            txtPointA.ForeColor = Color.Black;
            txtPointB.ForeColor = Color.Black;
            txtPointC.ForeColor = Color.Black;
            txtHeight.ForeColor = Color.Black;

            // ListBoxBG
            lstTriangleInfo.BackColor = Color.Pink;

            // ListBoxFG
            lstTriangleInfo.ForeColor = Color.Black;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Invalidate();
            lstTriangleInfo.Items.Clear();
        }

        private void InitializeUI()
        {

            btnSaveToFile = new Button();
            btnSaveToFile.Text = "Salvesta";
            btnSaveToFile.Location = new Point(10, 220);
            btnSaveToFile.Click += btnSaveToFile_Click;

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
            radioSides.Text = "Külge sisend";
            radioSides.Location = new Point(10, 130);
            radioSides.Checked = true;
            radioSides.CheckedChanged += RadioSides_CheckedChanged;

            radioHeight = new RadioButton();
            radioHeight.Text = "Kõrguse sisend";
            radioHeight.Location = new Point(10, 160);
            radioHeight.CheckedChanged += RadioHeight_CheckedChanged;

            btnDrawTriangle = new Button();
            btnDrawTriangle.Text = "Visand";
            btnDrawTriangle.Location = new Point(10, 190);
            btnDrawTriangle.Click += btnDrawTriangle_Click;

            btnClear = new Button();
            btnClear.Text = "Selge";
            btnClear.Location = new Point(10, 250);
            btnClear.Click += btnClear_Click;

            lstTriangleInfo = new ListBox();
            lstTriangleInfo.Location = new Point(10, 280);
            lstTriangleInfo.Size = new Size(300, 100);

            Controls.Add(btnSaveToFile);
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
        private void btnSaveToFile_Click(object sender, EventArgs e)
        {
            using (StreamWriter writer = new StreamWriter("triangle_data.txt", true))
            {
                writer.WriteLine("Kolmnurga küljed:");
                writer.WriteLine($"Külg A: {txtPointA.Text}");
                writer.WriteLine($"Külg B: {txtPointB.Text}");
                writer.WriteLine($"Külg C: {txtPointC.Text}");


                Triangle triangle = new Triangle(double.Parse(txtPointA.Text), double.Parse(txtPointB.Text), double.Parse(txtPointC.Text));
                double surface = triangle.Surface();
                double height = triangle.Height();
                double perimeter = triangle.Perimeter();

                writer.WriteLine($"Kõrgus: {height}");
                writer.WriteLine($"Pindala: {surface}");
                writer.WriteLine($"Perimeeter:  {perimeter}");

                writer.WriteLine("Kolmnurga tüüp: " + DetermineTriangleType(double.Parse(txtPointA.Text), double.Parse(txtPointB.Text), double.Parse(txtPointC.Text)));
                writer.WriteLine();
            }

            MessageBox.Show("Kolmnurga andmed lisatud triangle_data.txt");
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
                        double angleB = Math.Acos((pointA * pointA + pointC * pointC - pointB * pointB) / (2 * pointA * pointC));

                        double xA = centerX;
                        double yA = centerY;
                        double xB = xA + pointA;
                        double yB = yA;
                        double xC = centerX + pointC * Math.Cos(angleB);
                        double yC = centerY - pointC * Math.Sin(angleB);

                        // Create a Graphics object for drawing on the form
                        using (Graphics graphics = CreateGraphics())
                        {
                            // Create a Pen for drawing the triangle
                            using (Pen pen = new Pen(Color.Black))
                            {
                                graphics.DrawLine(pen, (float)xA, (float)yA, (float)xB, (float)yB);
                                graphics.DrawLine(pen, (float)xB, (float)yB, (float)xC, (float)yC);
                                graphics.DrawLine(pen, (float)xC, (float)yC, (float)xA, (float)yA);
                            }
                        }

                        // Create a new Triangle object
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

                    using (Graphics graphics = CreateGraphics())
                    {
                        using (Pen pen = new Pen(Color.Black))
                        {
                            graphics.DrawLine(pen, (float)xA, (float)yA, (float)xB, (float)yB);
                            graphics.DrawLine(pen, (float)xB, (float)yB, (float)xC, (float)yC);
                            graphics.DrawLine(pen, (float)xC, (float)yC, (float)xA, (float)yA);
                        }
                    }

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
