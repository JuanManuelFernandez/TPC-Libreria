using System;

namespace Libreria
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((bool)(Session["mailExitoso"] = true))
            {

            }
            if (Session["usuario"]!=null)
            {
                Label1.Visible = true;
            }
        }
    }
}