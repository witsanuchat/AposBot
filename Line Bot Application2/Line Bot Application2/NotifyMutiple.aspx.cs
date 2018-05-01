using LineMessagingAPISDK.Models;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Line_Bot_Application2
{
    public partial class NotifyMutiple : System.Web.UI.Page
    {

        //LineMessagingAPISDK.Models.MulticastMessage testsent = new LineMessagingAPISDK.Models.MulticastMessage();

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected  void bt_sentall(object sender, EventArgs e)
        {
            
            Controllers.MulticastMessage cast = new Controllers.MulticastMessage();
            cast.sentalluser("dasfasf");
            

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}