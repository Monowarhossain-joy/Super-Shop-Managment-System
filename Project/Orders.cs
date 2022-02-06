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
    public partial class OrdersForm : Form
    {
        private bool isNew = true;

        public OrdersForm()
        {
            InitializeComponent();
        }

        private void OrdersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Owner.Show();
        }

        private void OrdersForm_Load(object sender, EventArgs e)
        {
            this.LoadOrder();
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
        private void LoadOrder()
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

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
               string id = dgvCustomer.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtID.Text = id;
                this.LoadSingleOrder();
            }

        }

        private void LoadSingleOrder()
        {
            string query = "select * from CustomerTable where CU_ID=" + txtID.Text + "";
           
            DataTable dt = DataAccess.GetData(query);
            if (dt == null)
            {
                return;
            }

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("invalid Order ID");
                return;
            }

            isNew = false;
            txtName.Text = dt.Rows[0]["CU_Name"].ToString();

            if (DataAccess.ExecuteQuery(query) == true)
            {
                this.LoadOrder();
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

            

        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            product = dgvOrder.SelectedRows[0].Cells[1].Value.ToString();
            totalStock = Convert.ToInt32(dgvOrder.SelectedRows[0].Cells[2].Value.ToString());
            price = Convert.ToInt32(dgvOrder.SelectedRows[0].Cells[3].Value.ToString());
            //orderProducttextBox1.Text = dgvOrder.SelectedRows[0].Cells[1].Value.ToString();
            flag = 1;
        }
        int flag = 0;
        int totalStock;
        int serial = 0;
        string product;
        int quantity, price, totalPrice;
        int N = 0;

        private void dgvOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }
        void Update()
        {
            int idNo = Convert.ToInt32(dgvOrder.SelectedRows[0].Cells[0].Value.ToString());
            //int inSell = Convert.ToInt32(productQuantitytextBox1.Text);
            //int availableStock = totalStock - inSell;
            int availableStock = totalStock - Convert.ToInt32(txtADD.Text);
        
            string query = "update ProductTable1 set ProductQuantity=" + availableStock + "where ProductId=" + idNo + "";
            DataTable dt = DataAccess.GetData(query);
            if (dt == null)
            {
                return;
            }
            //dgvOrder();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            //int sellQuantity = Convert.ToInt32(productQuantitytextBox1.Text);
            if (txtADD.Text == "")
            {
                MessageBox.Show("Enter Product Quantity First ");
            }
            else if (flag == 0)
            {
                MessageBox.Show(" Select Your Product First ");
            }
            else if (Convert.ToInt32(txtADD.Text) > totalStock)
            {
                MessageBox.Show("Product Out Of Stock");
            }
            else
            {
                serial = serial + 1;
                quantity = Convert.ToInt32(txtADD.Text);
                totalPrice = quantity * price;



                DataTable table = new DataTable();



                table.Columns.Add("Serial", typeof(Int32));
                table.Columns.Add("Product", typeof(string));
                table.Columns.Add("Quantity", typeof(Int32));
                table.Columns.Add("Price", typeof(Int32));
                table.Columns.Add("TotalPrice", typeof(Int32));
                //addOrderdataGridView1.Visible = true;
                table.Rows.Add(serial, product, quantity, price, totalPrice);
                dgvOrder.DataSource = table;




            }
            N = N + totalPrice;
            txtTotalAmount.Text = N.ToString();
            Update();
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }

}
