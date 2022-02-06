using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            productForm pf = new productForm();
            pf.Show(this);
            this.Hide();
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            EmployeeForm ef = new EmployeeForm();
            ef.Show(this);
            this.Hide();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            CustomerForm cf = new CustomerForm();
            cf.Show(this);
            this.Hide();
        }

        private void Dashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Do you really want to Exit?", "closing alert !!!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Application.Exit();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            OrdersForm of = new OrdersForm();
            of.Show(this);
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CategoryForm cf = new CategoryForm();
            cf.Show(this);
            this.Hide();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            LoginForm lg=new LoginForm();
            lg.Show();
            this.Hide();
        }
    }
}
