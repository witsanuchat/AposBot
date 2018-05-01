using System;

public class Class1
{
	public Class1()
	{
        String connStr = "Server=tcp:aposbotserver.database.windows.net,1433;Initial Catalog=AposBOT;Persist Security Info=False;User ID=chatpont;Password=P.Apos!bot;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        SqlConnection conn = new SqlConnection(connStr);

        conn.Open();
        
        string query = "Select StudentID From Authen Where AuthenID = 1";
        SqlCommand cmd = new SqlCommand(query);
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.Connection = conn;

        var StuID = query;
        cmd.ExecuteNonQuery();

        conn.Close();

    }
}
