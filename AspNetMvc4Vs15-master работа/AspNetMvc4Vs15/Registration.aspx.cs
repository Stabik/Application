using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AspNetMvc4Vs15
{
    public partial class Registration : System.Web.UI.Page
    {
        private SqlConnection sqlConnection = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> db = new Dictionary<string, string>();
            SqlCommand getUserCmd = new SqlCommand("SELECT [Login], [Password] FROM [Users]", sqlConnection);
            SqlDataReader sqlReader = null;
            try
            {
                sqlReader = getUserCmd.ExecuteReader();
                while (sqlReader.Read())
                {
                    db.Add(Convert.ToString(sqlReader["Login"]), Convert.ToString(sqlReader["Password"]));

                }
            }
            catch { }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }
            if (!db.Keys.Contains(TextBox1.Text))
            {
                SqlCommand regUser = new SqlCommand("INSERT INTO [Users] (Login,Password)VALUES(@Login, @Password)", sqlConnection);
                regUser.Parameters.AddWithValue("Login", TextBox1.Text);
                regUser.Parameters.AddWithValue("Password", TextBox2.Text);
                regUser.ExecuteNonQuery();
                Response.Redirect("Login.aspx");
            }
            else
            {
                string s = "alert('Такой логин уже есть!');";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "MessageBox", s, true);
            }
            
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();


        }
    }
}