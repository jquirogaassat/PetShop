using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PetShop.Pages
{
    public partial class Logued : System.Web.UI.Page
    {
        Auth auth = new Auth();
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isWebMaster = Boolean.Parse(Session["WebMaster"].ToString());

            if (isWebMaster) lblWebMaster.Text = "WebMaster";
            if (!isWebMaster) lblWebMaster.Text = "Común";


            bool integrity = auth.ComprobarIntegridad();

            if (integrity) lblDBBroken.Text = "La base de datos ha sido cargada exitosamente";

            if (!integrity) { 
                lblDBBroken.Text = "La base de datos ha sido comprometida";
         
            }
        }

        protected void btnCorrect(object sender, EventArgs e)
        {
            auth.ArreglarDigitos();
           lblDBBroken.Text = "La base de datos ha sido arreglada exitosamente";
        }
    }
}