using Contacts_Keeper.ContKeepClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Contacts_Keeper
{
    public partial class Econtact : Form
    {
        public Econtact()
        {
            InitializeComponent();
        }
        contactClass c = new contactClass();
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Get values from input form
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo = txtBoxContactNumber.Text;
            c.Address = txtBoxAddress.Text;
            c.Gender = cmbGender.Text;
            //insert Data into DB using method Insert
            bool success = c.Insert(c);
            if(success == true)
            {
                MessageBox.Show("New Contact Inserted ");
                //call method clear
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to Add new Contact. Try Again");
            }
            //load data on Data GRidview
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void Load_contacts_Click(object sender, EventArgs e)
        {
            //load data on Data GRidview
            DataTable dt = c.Select();
            dgvContactList.DataSource = dt;
        }

        private void Econtact_Load(object sender, EventArgs e)
        {

        }
        // method to clear fields
       public void Clear()
        {
            txtboxContactID.Text = "";
            txtboxFirstName.Text = "";
            txtboxLastName.Text = "";
            txtBoxContactNumber.Text = "";
            txtBoxAddress.Text = "";
            cmbGender.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //get data from textboxes
            c.ContactID = int.Parse(txtboxContactID.Text);
            c.FirstName = txtboxFirstName.Text;
            c.LastName = txtboxLastName.Text;
            c.ContactNo = txtBoxContactNumber.Text;
            c.Address = txtBoxAddress.Text;
            c.Gender = cmbGender.Text;
            // update data 
            bool success = c.Update(c);
            if(success == true)
            {
                MessageBox.Show("Contact has been successfully Updated.");
                //load data on Data GRidview
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                //call clear method
                Clear();
            }
            else if (success== false)
            {
                MessageBox.Show("failed to update Contact. Try Again.");
            }
        }

        private void dgvContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // get the data from the grid view and load it into the textboxes
            // click and select the row using mouse 
            int rowIndex = e.RowIndex;
            txtboxContactID.Text = dgvContactList.Rows[rowIndex].Cells[0].Value.ToString();
            txtboxFirstName.Text = dgvContactList.Rows[rowIndex].Cells[1].Value.ToString();
            txtboxLastName.Text = dgvContactList.Rows[rowIndex].Cells[2].Value.ToString();
            txtBoxContactNumber.Text = dgvContactList.Rows[rowIndex].Cells[3].Value.ToString();
            txtBoxAddress.Text = dgvContactList.Rows[rowIndex].Cells[4].Value.ToString();
            cmbGender.Text = dgvContactList.Rows[rowIndex].Cells[5].Value.ToString();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //get the contact ID 
         //   c.ContactID = int.Parse(txtboxContactID.Text);
           c.ContactID = Convert.ToInt32(txtboxContactID.Text);
            bool success = c.Delete(c);
            if(success == true)
            {
                MessageBox.Show("Contact Succesfully Deleted");
                // refresh the data grid view

                //load data on Data GRidview
                DataTable dt = c.Select();
                dgvContactList.DataSource = dt;
                //call clear method
                Clear();
            }
            else
            {
                MessageBox.Show("Failed to Delete Contact. ,Try Again");
            }
        }
        static string myconnstr = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            //get the value from textboxes
            string keyword = txtboxSearch.Text;
            SqlConnection conn = new SqlConnection(myconnstr);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tbl_contact WHERE FirstName LIKE '%" + keyword + "%' OR LastName LIKE '%" + keyword + "%'OR Address LIKE '%" + keyword + "%'OR ContactNo LIKE '%" + keyword + "%'OR Gender LIKE '%" + keyword + "%'OR ContactID LIKE '%" + keyword + "%'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dgvContactList.DataSource = dt;
            
           
            bool isSuccess = false;
            if (isSuccess ==true )
            {
                
            }
            else
            {

            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void dgvContactList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
