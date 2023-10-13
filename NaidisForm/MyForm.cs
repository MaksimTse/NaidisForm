using System;
using System.Drawing;
using System.Windows.Forms;

namespace NaidisForm
{
    public partial class MyForm : Form
    {
        private TrianglePictureBoxControl trianglePictureBox;
        private Label heightLabel;
        private Label medianLabel;
        private TextBox sideATextBox;
        private TextBox sideBTextBox;
        private TextBox sideCTextBox;

        private void CalculateTriangle(object sender, EventArgs e)
        {
            double sideA, sideB, sideC;
            if (double.TryParse(sideATextBox.Text, out sideA) &&
                double.TryParse(sideBTextBox.Text, out sideB) &&
                double.TryParse(sideCTextBox.Text, out sideC))
            {
                Triangle triangle = new Triangle(sideA, sideB, sideC);
                double height = triangle.Height();
                double median = triangle.Median();

                heightLabel.Text = $"Высота: {height}";
                medianLabel.Text = $"Медиана: {median}";

                trianglePictureBox.Invalidate();
            }
        }
        public MyForm()
        {
            InitializeComponent();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;

            trianglePictureBox = new TrianglePictureBoxControl();
            heightLabel = new Label();
            medianLabel = new Label();
            sideATextBox = new TextBox();
            sideBTextBox = new TextBox();
            sideCTextBox = new TextBox();

            trianglePictureBox.Location = new Point(10, 10);
            trianglePictureBox.Size = new Size(200, 200);
            heightLabel.Location = new Point(10, 220);
            medianLabel.Location = new Point(10, 240);
            sideATextBox.Location = new Point(10, 260);
            sideBTextBox.Location = new Point(10, 280);
            sideCTextBox.Location = new Point(10, 300);

            this.Controls.Add(trianglePictureBox);
            this.Controls.Add(heightLabel);
            this.Controls.Add(medianLabel);
            this.Controls.Add(sideATextBox);
            this.Controls.Add(sideBTextBox);
            this.Controls.Add(sideCTextBox);

            sideATextBox.TextChanged += CalculateTriangle;
            sideBTextBox.TextChanged += CalculateTriangle;
            sideCTextBox.TextChanged += CalculateTriangle;
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
                    return true;
                    else return false;
                }
            }
        }
    }
}