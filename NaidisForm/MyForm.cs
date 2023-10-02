using System.ComponentModel;

namespace NaidisForm
{
    public partial class MyForm : Form
    {
        Button btn;
        public MyForm()
        {
            this.Height = 200;
            this.Width = 200;
            btn = new Button();
            btn.Height = 40;
            btn.Width = 100;
            btn.Text = "Valjuta mind!";
            btn.Location = new Point(10, 20);

            this.Controls.Add(btn);
        }
        public MyForm(int x, int y, string nimetus)
        {
            this.Height = x;
            this.Width = y;
            this.Text = nimetus;
            btn = new Button();
            btn.Height = 40;
            btn.Width = 100;
            btn.Text = "Click";
            btn.Location = new Point(10, 20);

            this.Controls.Add(btn);
            btn.Click += Btn_Click;
        }

        private void Btn_Click(object? sender, EventArgs e)
        {
            MyForm form = new MyForm();
            form.ShowDialog();
        }
    }
}