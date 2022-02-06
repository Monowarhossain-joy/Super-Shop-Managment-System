using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Project
{

    public partial class productForm : Form
    {
        private bool isNew = true;
        public productForm()
        {
            InitializeComponent();
        }

        private void productForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.Owner.Show();
        }

        private void productForm_Load(object sender, EventArgs e)
        {
            this.LoadProduct();

        }

        private void LoadProduct()
        {
            string query = "select * from ProductTable ";
            DataTable dt = DataAccess.GetData(query);

            if (dt == null)
                return;

            dgvProduct.AutoGenerateColumns = false;
            dgvProduct.DataSource = dt;
            dgvProduct.Refresh();
            dgvProduct.ClearSelection();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            this.LoadProduct();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.LoadProduct();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string fullName = txtName.Text;
            if (string.IsNullOrEmpty(fullName))
            {
                MessageBox.Show("Product Name is Required");
                txtName.Focus();
                return;

            }
          

            string qua = txtQuantity.Text;
            if (string.IsNullOrEmpty(qua))
            {
                MessageBox.Show("Product Quantity is Required");
                txtQuantity.Focus();
                return;

            }

            string price = txtPrice.Text;
            if (string.IsNullOrEmpty(price))
            {
                MessageBox.Show("Product Price is Required");
                txtPrice.Focus();
                return;

            }

            DateTime dtp;
            DateTime dt2;
           // try
           // {
                //dtp = Convert.ToDateTime(dtpBuying.Text);

                 dtp = Convert.ToDateTime(dtpBuying.Text);
                 dt2 = DateTime.Now;
                 if (dtp.Date == dt2.Date)
                 {
                     MessageBox.Show(" Buying");
                     
                 }
                else if (dtp.Date > dt2.Date)
                {
                    MessageBox.Show("Invalid BuyingDate");
                    dtpBuying.Focus();
                    return;
                }
               else if (dtp.Date < dt2.Date)
                {
                    MessageBox.Show("Invalid BuyingDate2");
                    dtpBuying.Focus();
                    return;
                }
                
                
           // }
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Invalid BuyingDate");
            //    dtpBuying.Focus();
            //    return;
            //}

            string category = cmbCategory.Text;
            if (string.IsNullOrEmpty(category))
            {
                MessageBox.Show("Invalid Category Product");
                cmbCategory.Focus();
                return;
            }
            string type = cmbType.Text;
            if (string.IsNullOrEmpty(type))
            {
                MessageBox.Show("Invalid Product Type");
                cmbType.Focus();
                return;
            }

            //Data Base

            string query = "";

            if (isNew == true)
            {
                query = "insert into ProductTable(P_Name,P_Quantity,P_Price,P_BuyingDate,P_Cetagory,P_Type) values('"+txtName+"','"+txtQuantity+"','"+txtPrice+"','"+dtpBuying+"','"+cmbCategory+"','"+cmbType+"')";
                MessageBox.Show("Product Inserted");
            }
            else
            {
                query = "update ProductTable set P_Name='" + fullName + "',P_Quantity='" + qua + "',P_Price='" + price + "',P_BuyingDate='" + dtp + "',P_Cetagory='" + category + "',P_Type='" + type + "' where P_ID = " + txtID.Text + "";
                MessageBox.Show("Updated Product");
            }

            if (DataAccess.ExecuteQuery(query) == true)
            {
                this.LoadProduct();
            }

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.newProduct();
        }
        private void newProduct()
        {
            isNew = true;
            txtID.Text = "";
            txtName.Text = "";
            txtQuantity.Text = "";
            txtPrice.Text = "";
            dtpBuying.Text = "";
            cmbCategory.Text = "";
            cmbType.Text = "";
            dgvProduct.ClearSelection();

        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (isNew == true)
            {
                MessageBox.Show("Invalid Load Existing  Data First");
                return;
            }
            string query = "delete from ProductTable where P_ID = " + txtID.Text + "";
            if (DataAccess.ExecuteQuery(query) == true)
            {
                MessageBox.Show("Deleted Product");
                this.LoadProduct();
                this.newProduct();
            }
        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string id = dgvProduct.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtID.Text = id;
                this.LoadSingleProduct();
            }
        }
        private void LoadSingleProduct()
        {
            string query = "select * from ProductTable where P_ID=" + txtID.Text + "";
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
            txtName.Text = dt.Rows[0]["P_Name"].ToString();
            txtQuantity.Text = dt.Rows[0]["P_Quantity"].ToString();
            txtPrice.Text = dt.Rows[0]["P_Price"].ToString();
            dtpBuying.Text = dt.Rows[0]["P_BuyingDate"].ToString();
            cmbCategory.Text = dt.Rows[0]["P_Cetagory"].ToString();
            cmbType.Text = dt.Rows[0]["P_Type"].ToString();

        }
    }
}
