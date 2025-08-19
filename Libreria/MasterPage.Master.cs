using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Libreria
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAccount_click (object sender, EventArgs e)
        {
            Response.Redirect("Cuenta.aspx");
        }
        protected void btnInicio_click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}