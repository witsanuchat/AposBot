using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Person
    {
        int AuthenID;
        string AccountLine;
        string StudentID;

        public int AuthenID1
        {
            get
            {
                return AuthenID;
            }

            set
            {
                AuthenID = value;
            }
        }

        public string LineAccount1
        {
            get
            {
                return AccountLine;
            }

            set
            {
                AccountLine = value;
            }
        }

        public string StudentID1
        {
            get
            {
                return StudentID;
            }

            set
            {
                StudentID = value;
            }
        }
    }
}
