using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts_Keeper.ContKeepClasses
{
    class contactClass
    {
        public int ContactID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ContactNo { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        //selecting data from DB
        public DataTable Select()
        {
            //step 1 DB connections 
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                //step 2 Writing SQl Query
                string sql = "SELECT * FROM tbl_contact";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch(Exception ex)
            { 
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        //inserting Data into DB
        public bool Insert(contactClass c)
        {
            // creating a defult return type and setting to  false
            bool isSuccess = false;

            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //step 2 : create sql query to insert data
                string sql = "INSERT INTO tbl_contact (FirstName , LastName , ContactNo, Address , Gender ) VALUES (@FirstName , @LastName ,@ContactNo , @Address , @Gender )";
                SqlCommand cmd = new SqlCommand(sql, conn);
                //create parameters to add data
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);

                //connection Open Here
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                // if the query run succ then the values of rows will be greater than zero 
                if(rows>0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        //method update data 
        public bool Update(contactClass c)
        {
            // create default return 
            bool isSuccess = true;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //sql to update data in DB                                                                                                            when the ContactID is inserted
                string sql = "UPDATE tbl_contact SET FirstName=@FirstName, LastName=@LastName, ContactNo=@ContactNo , Address=@Address, Gender=@Gender WHERE ContactID=@ContactID";
                //create sql commad
                SqlCommand cmd =new SqlCommand(sql, conn);
                //create parameters to add values
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);
               
                //connection Open Here
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                // if the query run succ then the values of rows will be greater than zero 
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        // Method to Delete Data from DB
        public bool Delete(contactClass c)
        {
            // create default return value 
            bool isSuccess = false;
            // sql connections
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //sql to delete data 
                string sql = "DELETE FROM tbl_contact WHERE ContactID = @ContactID";

                // create sql commands 
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ContactID ", c.ContactID);
                //open connection
                conn.Open();
                //
                int rows = cmd.ExecuteNonQuery();
                // if the query run succ then the values of rows will be greater than zero else set the value to 0
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch(Exception ex)
            {
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
    }
}
