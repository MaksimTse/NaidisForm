using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Drawing;
using System.Reflection.Metadata;
using System.Windows.Forms;

namespace NaidisForm
{
    public partial class TreeForm : Form
    {
        TreeView tree;
        Button btn;
        ContextMenuStrip contextMenu;
        Label lbl;
        TextBox txt_box;
        RadioButton r1, r2;
        CheckBox c1, c2;
        PictureBox pb;
        ListBox lb;

        private MyForm triangleForm;
        bool isBtnVisible = false;
        bool isLblVisible = false;
        bool isBoxVisible = false;
        bool isRBtnVisible = false;
        bool isChkVisible = false;
        bool isPicVisible = false;
        bool isLBVisible = false;


        public TreeForm()
        {
            this.Height = 600;
            this.Width = 1200;
            this.Text = "Vorm põhielementidega";
            this.tree = new TreeView();
            tree.Dock = DockStyle.Left;
            tree.BorderStyle = BorderStyle.Fixed3D;
            tree.AfterSelect += Tree_AfterSelect;

            TreeNode treeNode = new TreeNode("Elemendid");

            treeNode.Nodes.Add(new TreeNode("Nupp-Button"));
            btn = new Button();
            btn.Height = 40;
            btn.Width = 100;
            btn.Text = "Valjuta mind!";
            btn.Location = new Point(150, 150);
            btn.Click += Btn_Click;
            btn.MouseEnter += Btn_MouseEnter;
            btn.MouseUp += Btn_MouseUp;
            btn.MouseDoubleClick += Btn_MouseDoubleClick;

            contextMenu = new ContextMenuStrip();
            ToolStripMenuItem changeColorMenuItem = new ToolStripMenuItem("Сменить цвет на черный");
            changeColorMenuItem.Click += ChangeColorMenuItem_Click;
            contextMenu.Items.Add(changeColorMenuItem);

            btn.ContextMenuStrip = contextMenu;

            treeNode.Nodes.Add(new TreeNode("Silt-Label"));
            lbl=new Label();
            lbl.Text = "Pealkiri";
            lbl.Location = new Point(150, 0);
            lbl.Size=new Size(this.Width, btn.Location.Y);
            lbl.BackColor= Color.White;
            lbl.BorderStyle= BorderStyle.Fixed3D;
            lbl.Font = new Font("Tahoma", 24);

            //Tekskast
            treeNode.Nodes.Add(new TreeNode("Tekstkast-Textbox"));
            txt_box = new TextBox();
            txt_box.BorderStyle = BorderStyle.Fixed3D;
            txt_box.Height = 50;
            txt_box.Width = 100;
            txt_box.Text = "";
            txt_box.Location = new Point(tree.Width, btn.Top + btn.Height + 5);
            txt_box.KeyDown += new KeyEventHandler(Txt_box_KeyDown);

            //Radiobutton
            treeNode.Nodes.Add(new TreeNode("Radionupp-Radiobutton"));
            r1=new RadioButton();
            r1.Text = "Valik1";
            r1.Location= new Point(tree.Width, txt_box.Top + txt_box.Height + 5);// tree.Width, txt_box.Location.Y+txt_box.Height
            r1.CheckedChanged += new EventHandler(Radiobuttons_Changed);
            r2=new RadioButton();
            r2.Text = "Valik2";
            r2.Location = new Point(r1.Location.X + r1.Width, txt_box.Top + txt_box.Height + 5);
            r2.CheckedChanged += new EventHandler(Radiobuttons_Changed);

            //CheckBox
            treeNode.Nodes.Add(new TreeNode("CheckBox"));
            c1=new CheckBox();
            c1.Text = "Valik 1.1";
            c1.Location = new Point(tree.Width, r1.Top + r1.Height + 5);
            c1.CheckedChanged += new EventHandler(CheckBoxes_Changed);
            c2= new CheckBox();
            c2.Text = "Valik 2.2";
            c2.Location = new Point(c1.Location.X + c1.Width, r1.Top + r1.Height + 5);
            c1.CheckedChanged += new EventHandler(CheckBoxes_Changed);

            treeNode.Nodes.Add(new TreeNode("Picture"));
            pb=new PictureBox();
            pb.Location = new Point(tree.Width, c1.Top + c1.Height + 5);
            pb.Image = new Bitmap("../../../Untitled.jpg");
            pb.Size = new Size(260, 200);
            pb.SizeMode = PictureBoxSizeMode.Zoom;
            pb.BorderStyle= BorderStyle.Fixed3D;

            treeNode.Nodes.Add(new TreeNode("ListBox"));
            lb = new ListBox();
            lb.Items.Add("roheline");
            lb.Items.Add("sinine");
            lb.Items.Add("hall");
            lb.Items.Add("kollane");
            lb.Location= new Point(tree.Width, pb.Location.Y +pb.Height);


            treeNode.Nodes.Add(new TreeNode("DataGridView"));
            DataSet ds = new DataSet("XML fail. Menüü");
            ds.ReadXml(@"..\..\..\food_menu.xml");
            DataGridView dataGrid = new DataGridView();
            dataGrid.Location= new Point(tree.Width+pb.Width, pb.Location.Y);
            dataGrid.Height = 175;
            dataGrid.Width = 780;
            dataGrid.DataSource= ds;
            dataGrid.AutoGenerateColumns = true;
            dataGrid.DataMember= "Food";
            dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;


            treeNode.Nodes.Add(new TreeNode("Triangle"));
            


            tree.Nodes.Add(treeNode);
            this.Controls.Add(tree);
            this.Controls.Add(btn);
            this.Controls.Add(lbl);
            this.Controls.Add(txt_box);
            this.Controls.Add(r1);
            this.Controls.Add(r2);
            this.Controls.Add(c1);
            this.Controls.Add(c2);
            this.Controls.Add(pb);
            this.Controls.Add(lb);
            this.Controls.Add(dataGrid);
            lbl.Visible = false;
            btn.Visible = false;
            txt_box.Visible = false;
            r1.Visible = false;
            r2.Visible = false;
            c1.Visible = false;
            c2.Visible = false;
            pb.Visible = false;
            lb.Visible = false;
        }

        private void Txt_box_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string inputText = txt_box.Text.ToLower();
                if (inputText.StartsWith("title "))
                {
                    string newText = inputText.Substring(6);
                    this.Text = newText;
                }
                else if (inputText == "secret")
                {
                    lbl.Text = "Correct password!";
                }
                else if (inputText == "exit")
                {
                    Close();
                }
                else if (inputText == "tekst")
                {
                    string tekst = Interaction.InputBox("Sisesta pealkiri", "Pealkiri muutmine", "Uus pealkiri");

                    if (tekst == "")
                    {
                        this.Close();
                    }
                    else
                    {
                        lbl.Text = tekst;
                    }
                }
                if (inputText.StartsWith("fg ") && inputText.Length > 3)
                {
                    string colorInput = inputText.Substring(3);

                    try
                    {
                        Color textColor = ColorTranslator.FromHtml(colorInput);
                        this.ForeColor = textColor;
                    }
                    catch (Exception)
                    {
                       
                    }
                }
                else if (inputText.StartsWith("bg ") && inputText.Length > 3)
                {
                    string colorInput = inputText.Substring(3);

                    try
                    {
                        Color textColor = ColorTranslator.FromHtml(colorInput);
                        this.BackColor = textColor;
                    }
                    catch (Exception)
                    {

                    }
                }
                else if (inputText.StartsWith("cr ") && inputText.Length > 3)
                {
                    string LBInput = inputText.Substring(3);

                    if (!lb.Items.Contains(LBInput))
                    {
                        try
                        {
                            lb.Items.Add(LBInput);
                        }
                        catch (Exception)
                        {

                        }
                    }
                    else
                    {
                        MessageBox.Show("Этот элемент уже существует в ListBox'e.");
                    }
                }
                else if (inputText.StartsWith("dl ") && inputText.Length > 3)
                {
                    string LBInput = inputText.Substring(3);

                    if (lb.Items.Contains(LBInput))
                    {
                        try
                        {
                            lb.Items.Remove(LBInput);
                        }
                        catch (Exception)
                        {

                        }
                    }
                    else
                    {
                        MessageBox.Show("Этот элемент уже удален в ListBox'e.");
                    }
                }
                else
                {
                    lbl.Text = inputText;
                }

                lbl.Visible = true;
                txt_box.Clear();
            }
        }


        private void CheckBoxes_Changed(object? sender, EventArgs e)
        {
            bool isChecked1 = c1.Checked;
            bool isChecked2 = c2.Checked;

            if (isChecked1 && !isChecked2)
            {
                this.BackColor = Color.White;
                this.ForeColor = Color.Black;
                btn.ForeColor = Color.Black;
                lbl.ForeColor = Color.Black;
            }
            else if (isChecked2 && !isChecked1)
            {
                this.BackColor = Color.Black;
                this.ForeColor = Color.White;
                btn.ForeColor = Color.White;
                lbl.ForeColor = Color.Black;
            }
            else if (isChecked1 && isChecked2)
            {
                this.BackColor = Color.Gray;
                this.ForeColor = Color.Blue;
                btn.ForeColor = Color.Blue;
                lbl.ForeColor = Color.Blue;
            }
            else
            {
                this.BackColor = SystemColors.Control;
                this.ForeColor = SystemColors.ControlText;
                btn.ForeColor = SystemColors.ControlText;
                lbl.ForeColor = SystemColors.ControlText;
            }
        }

        private void Radiobuttons_Changed(object? sender, EventArgs e)
        {
            if (r1.Checked)
            {
                this.BackColor = Color.White;
                this.ForeColor = Color.Black;
                btn.ForeColor = Color.Black;
                lbl.ForeColor = Color.Black;
            }
            else if (r2.Checked)
            {
                this.BackColor = Color.Black;
                this.ForeColor = Color.White;
                btn.ForeColor = Color.White;
                lbl.ForeColor = Color.Black;
            }
        }



        private void Tree_AfterSelect(object? sender, TreeViewEventArgs e)
        {
            if (e.Node.Text == "Nupp-Button")
            {
                tree.SelectedNode = null;
                isBtnVisible = !isBtnVisible; 
                btn.Visible = isBtnVisible;
            }
            else if (e.Node.Text == "Silt-Label")
            {
                tree.SelectedNode= null;
                isLblVisible = !isLblVisible;
                lbl.Visible = isLblVisible;
            }
            else if (e.Node.Text == "Tekstkast-Textbox")
            {
                tree.SelectedNode = null;
                isBoxVisible = !isBoxVisible;
                txt_box.Visible = isBoxVisible;
            }
            else if (e.Node.Text == "Radionupp-Radiobutton")
            {
                tree.SelectedNode = null;
                isRBtnVisible = !isRBtnVisible;
                r1.Visible = isRBtnVisible;
                r2.Visible = isRBtnVisible;
            }
            else if (e.Node.Text == "CheckBox")
            {
                tree.SelectedNode = null;
                isChkVisible = !isChkVisible;
                c1.Visible = isChkVisible;
                c2.Visible = isChkVisible;
            }
            else if (e.Node.Text == "Picture")
            {
                tree.SelectedNode = null;
                isPicVisible = !isPicVisible;
                pb.Visible = isPicVisible;
            }
            else if (e.Node.Text == "ListBox")
            {
                tree.SelectedNode = null;
                isLBVisible = !isLBVisible;
                lb.Visible = isLBVisible;
            }
            else if (e.Node.Text == "Triangle")
            {
                tree.SelectedNode = null;
                triangleForm = new MyForm();
                triangleForm.Show();
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            if (btn.BackColor == Color.Aqua)
            {
                btn.BackColor = Color.Chocolate;
            }
            else
            {
                btn.BackColor = Color.Aqua;
            }
        }

        private void ChangeColorMenuItem_Click(object sender, EventArgs e)
        {
            btn.BackColor = Color.Black;
        }

        private void Btn_MouseEnter(object sender, EventArgs e)
        {
            btn.BackColor = Color.Red;
        }

        private void Btn_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                btn.BackColor = Color.White;
            }
        }

        private void Btn_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) 
            {
                MessageBox.Show("Double");
            }
        }
    }
}