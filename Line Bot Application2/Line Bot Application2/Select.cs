using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Line_Bot_Application2.Controllers;
using System.Data;

namespace Line_Bot_Application2
{
    public class Select
    {
        public string selectUserID()
        {
            String connStr = "Server=tcp:aposbotserver.database.windows.net,1433;Initial Catalog=AposBOT;Persist Security Info=False;User ID=chatpont;Password=P.Apos!bot;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            SqlConnection conn = new SqlConnection(connStr);

            conn.Open();

            string query = "Select StudentID From Authen Where AuthenID = 1";
            SqlCommand cmd = new SqlCommand(query);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = conn;

            SqlDataReader reader =  cmd.ExecuteReader();
            string studentID = "";
            while (reader.Read())
            {
                // get the results of each column
                studentID = (string)reader["StudentID"];
                
            }

            conn.Close();
            return studentID;
        }

        public string selectGrade(string term)
        {
            Select select = new Select();
            select.getStuID();
            String connStr = "Server=tcp:aposbotserver.database.windows.net,1433;Initial Catalog=AposBOT;Persist Security Info=False;User ID=chatpont;Password=P.Apos!bot;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            SqlConnection conn = new SqlConnection(connStr);

            conn.Open();

            string query = "Select SEM_GPA From GPA Where STUDENT_ID = '"+Globals.StuIDs+"' AND EDU_TERM = '"+term+"'";
            SqlCommand cmd = new SqlCommand(query);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = conn;

            SqlDataReader reader = cmd.ExecuteReader();
            string GPAs = "";
            while (reader.Read())
            {
                // get the results of each column
                GPAs = (string)reader["SEM_GPA"];

            }
            if (GPAs=="")
            {
                GPAs = "ไม่พบข้อมูล GPA ของปีการศึกษาที่ "+term;
                return GPAs;
            }

            conn.Close();
            return "GPA ของปีการศึกษาที่ "+term+ " คือ "+GPAs;
        }
        public string GetsumGPA()
        {
            Select select = new Select();
            select.getStuID();
            String connStr = "Server=tcp:aposbotserver.database.windows.net,1433;Initial Catalog=AposBOT;Persist Security Info=False;User ID=chatpont;Password=P.Apos!bot;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            SqlConnection conn = new SqlConnection(connStr);

            conn.Open();

            string query = "Select CUM_GPA From GPA Where STUDENT_ID = '"+Globals.StuIDs+"'";
            SqlCommand cmd = new SqlCommand(query);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = conn;

            SqlDataReader reader = cmd.ExecuteReader();
            string GPAsum = "";
            while (reader.Read())
            {
                // get the results of each column
                GPAsum = (string)reader["CUM_GPA"];

            }
            conn.Close();
            return "GPA รวมทุกภาคการศึกษาของคุณคือ "+GPAsum;
            
        }
        public string selectName()
        {
            Select select = new Select();
            select.getStuID();
            String connStr = "Server=tcp:aposbotserver.database.windows.net,1433;Initial Catalog=AposBOT;Persist Security Info=False;User ID=chatpont;Password=P.Apos!bot;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            string query = "Select STUD_NAME_ENG From STUDENT WHERE STUDENT_ID = " + Globals.StuIDs;
            SqlCommand cmd = new SqlCommand(query);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = conn;

            SqlDataReader reader = cmd.ExecuteReader();
            string NameStud = "";
            while (reader.Read())
            {
                NameStud = (string)reader["STUD_NAME_ENG"];
            }

            conn.Close();

            return NameStud;
        }
        public void getStuID()
        {
            String connStr = "Server=tcp:aposbotserver.database.windows.net,1433;Initial Catalog=AposBOT;Persist Security Info=False;User ID=chatpont;Password=P.Apos!bot;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            SqlConnection conn = new SqlConnection(connStr);

            conn.Open();

            string query = "Select StudentID From Authen Where Acountline = '" + Globals.UserIDLine + "'";
            SqlCommand cmd = new SqlCommand(query);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = conn;

            SqlDataReader reader = cmd.ExecuteReader();
            //string studentID = "";
            while (reader.Read())
            {
                // get the results of each column
                Globals.StuIDs = (string)reader["StudentID"];

            }
            
        }
        public string GetSchedule(string Day)
        {
            Select select = new Select();
            select.getStuID();

            String connStr = "Server=tcp:aposbotserver.database.windows.net,1433;Initial Catalog=AposBOT;Persist Security Info=False;User ID=chatpont;Password=P.Apos!bot;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            string query = "Select DISTINCT DATE1,TIME_START1,DATE2,TIME_START2,SUBJECT_ID From Schedule WHERE STUDENT_ID = '" + Globals.StuIDs+"' AND (DATE1 = '"+Day+ "' OR DATE2 = '" + Day + "') order by TIME_START2";
            SqlCommand cmd = new SqlCommand(query);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = conn;
            
            SqlDataReader reader = cmd.ExecuteReader();
            //reader.getv
            string DATE11 = "", DATE12 = "", DATE21 = "";
            string DATE22 = "", DATE31 = "", DATE32 = "";
            string Time11 = "", Time12 = "";
            string Time21 = "", Time22 = "";
            string Time31 = "", Time32 = "";
            string subID1 = "";
            string subID2 = "";
            string subID3 = "";
            string subName1 = "";
            string subName2 = "";
            string subName3 = "";
           //if(reader.HasRows)
            if (reader.Read())
            {
                //Console.WriteLine
                DATE11 = (string)reader["DATE1"];
                DATE12 = (string)reader["DATE2"];
                subID1 = (string)reader["SUBJECT_ID"];


                if (DATE11 == Day)
                {
                    Time11 = (string)reader["TIME_START1"];
                } else if (DATE12 == Day)
                {
                    Time12 = (string)reader["TIME_START2"];
                }
                //reader.NextResult();
            }
            

            if (reader.Read())
            { 
                DATE21 = (string)reader["DATE1"];
                DATE22 = (string)reader["DATE2"];
                subID2 = (string)reader["SUBJECT_ID"];

                if (DATE21 == Day)
                {
                    Time21 = (string)reader["TIME_START1"];
                }
                else if (DATE22 == Day)
                {
                    Time22 = (string)reader["TIME_START2"];
                }
                
            }
            if (reader.Read())
            { 
                
                DATE31 = (string)reader["DATE1"];
                DATE32 = (string)reader["DATE2"];
                subID3 = (string)reader["SUBJECT_ID"];


                if (DATE31 == Day)
                {
                    Time31 = (string)reader["TIME_START1"];
                }
                else if (DATE32 == Day)
                {
                    
                    Time32 = (string)reader["TIME_START2"];
                }
                reader.NextResult();
            }
            



            switch (subID1)
            {
                case "101": subName1 = "HCI"; break;
                case "102": subName1 = "V&V"; break;
                case "103": subName1 = "PM"; break;
                case "104": subName1 = "COOP"; break;
                case "105": subName1 = "SA"; break;
                case "106": subName1 = "SOA"; break;
                case "107": subName1 = "Project1"; break;
                case "108": subName1 = "SPI"; break;
            }
            switch (subID2)
            {
                case "101": subName2 = "HCI"; break;
                case "102": subName2 = "V&V"; break;
                case "103": subName2 = "PM"; break;
                case "104": subName2 = "COOP"; break;
                case "105": subName2 = "SA"; break;
                case "106": subName2 = "SOA"; break;
                case "107": subName2 = "Project1"; break;
                case "108": subName2 = "SPI"; break;
            }
            switch (subID3)
            {
                case "101": subName3 = "HCI"; break;
                case "102": subName3 = "V&V"; break;
                case "103": subName3 = "PM"; break;
                case "104": subName3 = "COOP"; break;
                case "105": subName3 = "SA"; break;
                case "106": subName3 = "SOA"; break;
                case "107": subName3 = "Project1"; break;
                case "108": subName3 = "SPI"; break;
            }

            conn.Close();
            if(subName1 == "")
            {
                return "วันคุณไม่มีเรียน";
            }
            if(subName2 == "")
            {
                return "วันนี้คุณมีเรียนวิชา " + subName1 + " เวลา " + Time11 + Time12+" น. ";
            }else if(subName3 == "")
            {
                return "วันนี้คุณมีเรียนวิชา " + subName1 + " เวลา " + Time11 + Time12 + " น.  และวิชา " + subName2 + " เวลา "  + Time21 + Time22 + " น. ";
            }else         
            return "วันนี้คุณมีเรียนวิชา "+subName1+" เวลา "+ Time11 + Time12 + " น.  วิชา " + subName2+" เวลา "+Time21 +Time22 + " น. วิชา " + subName3 + " และเวลา "+Time31 + Time32 + " น. ";
           
        }
        public string GetRegisteration(string term)
        {
            Select select = new Select();
            select.getStuID();

            String connStr = "Server=tcp:aposbotserver.database.windows.net,1433;Initial Catalog=AposBOT;Persist Security Info=False;User ID=chatpont;Password=P.Apos!bot;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();

            string query = "select Subjectid,SECTION,SUBJECTNAME,CREDIT,CREDIT_TYPE from RegisFix where Studentid ='" + Globals.StuIDs + "' AND term_regis = '" + term + "'";
            SqlCommand cmd = new SqlCommand(query);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = conn;
            //DataSet ds = new DataSet(connStr);
            SqlDataReader reader = cmd.ExecuteReader();
            string gap = "5555";
            int i = 8;
            string  subname = "", section = "",allregis = "";
            while (i > 0){  
                if (reader.Read())
                {
                    
                    subname = (string)reader["SUBJECTNAME"];
                    section = (string)reader["SECTION"];
                   
                    
                    gap = " วิชา " + subname + " Sec " + section + " ";
                   
                }
                if(i == 1)
                {
                    reader.NextResult();
                }
               // reader.NextResult();
                i--;
                allregis += gap;
            }
            
            conn.Close();
            return "คุณได้ลงทะเบียนวิชา " + allregis;
        }
        private void methodToCall(DateTime time)
        {

        }
        }
}