using Line_Bot_Application2.Controllers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Line_Bot_Application2
{
   
    public partial class WebAuthen : System.Web.UI.Page
    {
        
        ServiceAuthen.AuthenticationSoapClient authen = new ServiceAuthen.AuthenticationSoapClient();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string StuIDs = TextBox1.Text;
            string pass = Password.Text;

            try
            {
                var StuID = authen.Authenticate(StuIDs, pass);
                

                if (StuID == true)
                {

                    String connStr = "Server=tcp:aposbotserver.database.windows.net,1433;Initial Catalog=AposBOT;Persist Security Info=False;User ID=chatpont;Password=P.Apos!bot;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

                    SqlConnection conn = new SqlConnection(connStr);

                    conn.Open();
                    string StuIDAuthen = "12";
                    //string UserLine = "11";
                    //UserLine = ;
                    StuIDAuthen = TextBox1.Text;

                    //TextBox4.Text = StuIDAuthen;
                    //TextBox5.Text = UserLine;
                    //"INSERT INTO [dbo].[Authen]([StudentID], [AcountLine]) VALUES('5730213071', '858956465989')";"INSERT INTO [dbo].[Authen]([StudentID], [AcountLine]) VALUES('"+StuIDAuthen+"','"+Globals.UserIDLine+"')";
                    string query = "INSERT INTO [dbo].[Authen]([StudentID], [AcountLine]) VALUES('" + StuIDAuthen + "','" + Globals.UserIDLine + "')";
                    string query2 = "UPDATE [dbo].[Authen] SET AcountLine =" + Globals.UserIDLine + "WHERE StudentID = '"+StuIDAuthen+"'";
                    SqlCommand cmd = new SqlCommand(query);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = conn;

                    cmd.ExecuteNonQuery();
                    
                    {

                    }
                    conn.Close();
                    Label4.Text = "Login Complate";

                    TextBox1.Text = "";
                    Password.Text = "";
                }
                else if (StuID == false)
                {
                    TextBox1.Text = "";
                    Password.Text = "";
                    Label4.Text = "StudentID or Password Not Coreect";
                }

                /*foreach (var item in StuID)
                {
                   Console.WriteLine(item.ToString());
                    TextBox3.Text = item;

                }*/

            }
            catch (Exception)
            {
                String connStr = "Server=tcp:aposbotserver.database.windows.net,1433;Initial Catalog=AposBOT;Persist Security Info=False;User ID=chatpont;Password=P.Apos!bot;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

                SqlConnection conn = new SqlConnection(connStr);

                conn.Open();
                string StuIDAuthen = "12";
                //string UserLine = "11";
                //UserLine = ;
                StuIDAuthen = TextBox1.Text;

                //TextBox4.Text = StuIDAuthen;
                //TextBox5.Text = UserLine;
                //"INSERT INTO [dbo].[Authen]([StudentID], [AcountLine]) VALUES('5730213071', '858956465989')";"INSERT INTO [dbo].[Authen]([StudentID], [AcountLine]) VALUES('"+StuIDAuthen+"','"+Globals.UserIDLine+"')";
                string query = "INSERT INTO [dbo].[Authen]([StudentID], [AcountLine]) VALUES('" + StuIDAuthen + "','" + Globals.UserIDLine + "')";
                string query2 = "UPDATE [dbo].[Authen] SET AcountLine ='" + Globals.UserIDLine + "'WHERE StudentID = '" + StuIDAuthen + "'";
                SqlCommand cmd = new SqlCommand(query2);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = conn;
                //DateTime myUtcDateTime = DateTime.Now;
                //DateTime myConvertedDateTime = TimeZoneInfo.ConvertTime(myUtcDateTime, TimeZoneInfo.FindSystemTimeZoneById("Indian Standard Time"));
                cmd.ExecuteNonQuery();

                {

                }
                conn.Close();
                Label4.Text = "ระบบได้เปลี่ยนแปลงข้อมูลให้เรียบร้อยแล้ว"+ DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
            }
        }

        protected void TextBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}