using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Student_CRUD
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["StudentsConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
        
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ExecuteQuery("INSERT INTO Student VALUES(@Roll, @Name, @Mob, @Addr, @Gen)");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            ExecuteQuery("UPDATE Student SET Name=@Name, Mobile=@Mob, Address=@Addr, Gender=@Gen WHERE RollNo=@Roll");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            ExecuteQuery("DELETE FROM Student WHERE RollNo=@Roll");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Student", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }

        private void ExecuteQuery(string query)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Roll", TextBox1.Text);
                cmd.Parameters.AddWithValue("@Name", TextBox2.Text);
                cmd.Parameters.AddWithValue("@Mob", TextBox3.Text);
                cmd.Parameters.AddWithValue("@Addr", TextBox4.Text);
                cmd.Parameters.AddWithValue("@Gen", DropDownList1.SelectedValue);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Button4_Click(null, null); // Refresh grid
            }

        }
    }
}
