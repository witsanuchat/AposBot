using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Line_Bot_Application2
{
    public class ControllMultiCastMs
    {
        public string AccountLine { get; set; }

        public List<ControllMultiCastMs> LoadAccount()
        {
            //String connStr = "Server=tcp:aposbotserver.database.windows.net,1433;Initial Catalog=AposBOT;Persist Security Info=False;User ID=chatpont;Password=P.Apos!bot;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var listOfAccount = new List<ControllMultiCastMs>();
            String connStr = "Server=tcp:aposbotserver.database.windows.net,1433;Initial Catalog=AposBOT;Persist Security Info=False;User ID=chatpont;Password=P.Apos!bot;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            SqlConnection conn = new SqlConnection(connStr);

            conn.Open();

            string query = "Select AcountLine From Authen";
            SqlCommand cmd = new SqlCommand(query);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = conn;

            SqlDataReader reader = cmd.ExecuteReader();
            
            while (reader.Read())
                        {
                            var Account = new ControllMultiCastMs();
                            Account.AccountLine = reader["AcountLine"].ToString();


                            listOfAccount.Add(Account);
                        }
                    
                
            

            return listOfAccount;
        }
    }
}