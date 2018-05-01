using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Domain;

namespace Session
{
    public class Broker
    {
        SqlConnection conn;
        //SqlConnectionStringBuilder connStringBuilder;
        

        void ConnectTo()
        {
            /*System.Data.SqlClient.SqlConnection Sqlconn = new System.Data.SqlClient.SqlConnection();
            Sqlconn.ConnectionString = "Server=tcp:aposbotserver.database.windows.net,1433;Initial Catalog=AposBOT;Persist Security Info=False;User ID=chatpont;Password=Apos!bot;MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;";
            conn = new SqlConnection(Sqlconn.ToString());
            */

            System.Data.SqlClient.SqlConnection _SqlConnection = new System.Data.SqlClient.SqlConnection();
            _SqlConnection.ConnectionString = "Server=tcp:aposbotserver.database.windows.net,1433;Initial Catalog=AposBOT;Persist Security Info=False;User ID=chatpont;Password=Apos!bot;MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;";
            System.Data.SqlClient.SqlCommand _SqlCommand = new System.Data.SqlClient.SqlCommand("SELECT * FROM Authen", _SqlConnection);
            System.Data.SqlClient.SqlDataAdapter _SqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter();
            _SqlDataAdapter.SelectCommand = _SqlCommand;
            /*DataTable _DataTable = new DataTable();
            _DataTable.Locale = System.Globalization.CultureInfo.InvariantCulture;
            _SqlDataAdapter.Fill(_DataTable);
            GridView1.DataSource = _DataTable;
            GridView1.DataBind();*/
        }
        public Broker()
        {
            ConnectTo();
        }
        public void Insert(Person p)
        {
            try
            {
                string cmdText = "INSERT INTO dbo.Authen(StudentID,AccountLine) VALUES(@StudentID,@AccountLine)";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@StudentID", p.StudentID1);
                cmd.Parameters.AddWithValue("@AccountLine", p.LineAccount1);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if(conn != null)
                {
                    conn.Close();
                }
            }
        }
    }
}
