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
    public partial class CustomerForm : Form
    {
        private bool isNew = true;
        public CustomerForm()
        {
            InitializeComponent();
        }

        private void CustomerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Owner.Show();
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            this.LoadCustomer();
        }
        private void LoadCustomer()
        {
            string query = "select * from CustomerTable";
            DataTable dt = DataAccess.GetData(query);

            if (dt == null)
                return;

            dgvCustomer.AutoGenerateColumns = false;
            dgvCustomer.DataSource = dt;
            dgvCustomer.Refresh();
            dgvCustomer.ClearSelection();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.LoadCustomer();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text ="";
            this.LoadCustomer();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.newCustomer();
        }
        private void newCustomer()
        {
            isNew = true;
            txtID.Text = "";
            txtName.Text = "";
            txtPhone.Text = "";
            cmbMember.Text = "";
            dtMember.Text = "";
          
            dgvCustomer.ClearSelection();

        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string id = dgvCustomer.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtID.Text = id;
                this.LoadSingleCustomer();
            }
        }
        private void LoadSingleCustomer()
        {
            string query = "select * from CustomerTable where CU_ID=" + txtID.Text + "";
            DataTable dt = DataAccess.GetData(query);
            if (dt == null)
            {
                return;
            }

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("invalid id");
                return;
            }

            isNew = false;
            txtName.Text = dt.Rows[0]["CU_Name"].ToString();
            txtPhone.Text = dt.Rows[0]["CU_Phone"].ToString();
            cmbMember.Text = dt.Rows[0]["CU_Member"].ToString();
           dtMember.Text = dt.Rows[0]["CU_Date"].ToString();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string fullName = txtName.Text;
            if (string.IsNullOrEmpty(fullName))
            {
                MessageBox.Show("Customer Name is Required");
                txtName.Focus();
                return;

            }

            string phone = txtPhone.Text;
            if (string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Phone Number is Required");
                txtPhone.Focus();
                return;

            }

            string member = cmbMember.Text;
            if (string.IsNullOrEmpty(member))
            {
                MessageBox.Show("Membershio is Required");
                cmbMember.Focus();
                return;

            }

            DateTime dtp;
            try
            {
                dtp = Convert.ToDateTime(dtMember.Text);
               // int date = (DateTime.Now - dtp).Days;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid BuyingDate");
                dtMember.Focus();
                return;
            }


            //Data Base

            string query = "";

            if (isNew == true)
            {
                query = "insert into CustomerTable(CU_Name,CU_Phone,CU_Member,CU_Date) values('" + txtName + "','" + txtPhone + "','" + cmbMember + "','" + dtMember + "')";
                MessageBox.Show("Customer Inserted");
            }
            else
            {
                query = "update CustomerTable set CU_Name='" + fullName + "',CU_Phone='" + phone + "',CU_Member='" + member + "',CU_Date='" + dtp + "' where CU_ID = " + txtID.Text + "";
                MessageBox.Show("Updated Coustomer");
            }

            if (DataAccess.ExecuteQuery(query) == true)
            {
                this.LoadCustomer();
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (isNew == true)
            {
                MessageBox.Show("Invalid Load Existing  Data First");
                return;
            }
            string query = "delete from CustomerTable where CU_ID = " + txtID.Text + "";
            if (DataAccess.ExecuteQuery(query) == true)
            {
                MessageBox.Show("Deleted Customer");
                this.LoadCustomer();
                this.newCustomer();
            }
        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
