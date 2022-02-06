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
    public partial class CategoryForm : Form
    {
        private bool isNew = true;

        public CategoryForm()
        {
            InitializeComponent();
        }

        private void CategoryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Show();
        }

        private void CategoryForm_Load(object sender, EventArgs e)
        {
            this.LoadCategory();
        }

        private void LoadCategory()
        {
            string query = "select * from CategoryTable";
            if (string.IsNullOrEmpty(txtSearch.Text) == false)
            {
                query = query + " where C_ID like '%" + txtSearch.Text + "%'";
            }

            DataTable dt = DataAccess.GetData(query);

            if (dt == null)
                return;

            dgvCategory.AutoGenerateColumns = false;
            dgvCategory.DataSource = dt;
            dgvCategory.Refresh();
            dgvCategory.ClearSelection();

            this.newCategory();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            this.LoadCategory();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
           
            this.LoadCategory();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtcName.Text;
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Category Name is Required");
                txtcName.Focus();
                return;
            }

            string query = "";

            if (isNew == true)
            {
                query = "insert into CategoryTable(C_Name) values('" + name + "')";
                MessageBox.Show("Category Inserted");
            }
            else
            {
                query = "update CategoryTable set C_Name='" + name + "' where C_ID = " + txtID.Text + "";
                MessageBox.Show("Updated Category");
            }

            if (DataAccess.ExecuteQuery(query) == true)
            {
                this.LoadCategory();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.newCategory();
        }
        private void newCategory()
        {
            isNew = true;
            txtID.Text = "";
            txtcName.Text = "";
            dgvCategory.ClearSelection();

        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (isNew == true)
            {
                MessageBox.Show("Invalid Load Existing  Data First");
                return;
            }
            string query = "delete from CategoryTable where C_ID = " + txtID.Text + "";
            if (DataAccess.ExecuteQuery(query) == true)
            {
                MessageBox.Show("Deleted Data");
                this.LoadCategory();
                this.newCategory();
            }
        }

        private void dgvCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string id = dgvCategory.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtID.Text = id;
                this.LoadSingleCategory();
            }
        }
        private void LoadSingleCategory()
        {
            string query = "select * from CategoryTable where C_ID=" + txtID.Text + "";
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
            txtcName.Text = dt.Rows[0]["C_Name"].ToString();
            
        }

        private void dgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
