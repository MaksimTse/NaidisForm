using System;
using System.Drawing;
using System.Windows.Forms;

namespace NaidisForm
{
    public partial class TreeForm : Form
    {
        TreeView tree;
        Button btn;
        ContextMenuStrip contextMenu;
        Label lbl;
        bool isBtnVisible = false;
        bool isLblVisible = false;

        public TreeForm()
        {
            this.Height = 600;
            this.Width = 800;
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


            tree.Nodes.Add(treeNode);
            this.Controls.Add(tree);
            this.Controls.Add(btn);
            this.Controls.Add(lbl);
            lbl.Visible = false;
            btn.Visible = false;
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