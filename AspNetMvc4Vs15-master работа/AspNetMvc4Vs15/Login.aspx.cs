using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;

namespace AspNetMvc4Vs15
{
    public partial class Login : System.Web.UI.Page
    {
        private SqlConnection sqlConnection = null;
        protected   void Page_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);
              sqlConnection.Open();
        }

        protected  void Button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> db = new Dictionary<string, string>();
            SqlCommand getUserCmd = new SqlCommand("SELECT [Login],[Password] FROM [Users]",sqlConnection);
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
            try
            {
                if (TextBox2.Text == db[TextBox1.Text])
                {
                    HttpCookie login = new HttpCookie("login", TextBox1.Text);
                    Response.Cookies.Add(login);
                    Response.Redirect("Home");//home
                    
                }
            }
            catch
            {
                string s = "alert('Такого логина не существует!');";
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