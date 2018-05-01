using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain;
using Session;

namespace TestDatabase
{
    public partial class Form1 : Form
    {
        Broker b;
        Person p;
        public Form1()
        {
            InitializeComponent();
            b = new Broker();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            p = new Person();
            p.StudentID1 = txtStdID.Text;
            p.LineAccount1 = txtUserID.Text;
            b.Insert(p);
        }
    }
}
