using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace NaidisForm
{
    public partial class MyForm : Form
    {
        class Triangle
        {
            public double a;
            public double b;
            public double c;
            public double height;
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
            public double CalculateHeight()
            {
                double s = Surface();
                double height = 2 * s / a;
                return height;
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