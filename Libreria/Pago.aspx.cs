using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Libreria
{
    public partial class Pago : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TxtNumeroTarjeta.Attributes.Add("placeholder", "0000-0000-0000-0000");
            TxtTelefono.Attributes.Add("placeholder", "11-1111-1111");
        }
    }
}