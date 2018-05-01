using System;
using System.IO;

using System.Data.SqlClient;
using Microsoft.Azure.WebJobs;

namespace PSULineBOTWebJob
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage([QueueTrigger("queue")] string message, TextWriter log)
        {
            Console.WriteLine("This should run every time there is a message in the queue");
            Console.WriteLine(message);
            log.WriteLine(message);
        }
        public static void FiveSecondTask([TimerTrigger("*/10 *  * * * *")] TimerInfo timer)
        {
            Console.WriteLine("This should run every 10 seconds");
            String connStr = "Server=tcp:aposbotserver.database.windows.net,1433;Initial Catalog=AposBOT;Persist Security Info=False;User ID=chatpont;Password=P.Apos!bot;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection conn = new SqlConnection(connStr);

            conn.Open();

            string query = "Select MESSAGE From Notifyrealtime where convert(varchar(10), DATESENT, 102) = convert(varchar(10), getdate(), 102)";
            SqlCommand cmd = new SqlCommand(query);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = conn;

            SqlDataReader reader = cmd.ExecuteReader();
           // MulticastMessage mesall = new MulticastMessage();
            //mesall.sentalluser("ทดสอบ By WebJob");
            while (reader.Read())
            {
                
                Console.WriteLine(reader["MESSAGE"].ToString());
            }

        }
    }
}
