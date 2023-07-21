using Services;
using System;

namespace PetShop.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["WebMaster"] = "";
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Auth auth = new Auth();


            var usuarioEncriptado = auth.ObtenerUsuarioEncriptado(txtBoxLogin.Text.ToUpper().Trim(), txtBoxPassword.Text.Trim());

            

            if (usuarioEncriptado.TieneAccesso)
            {
                if (usuarioEncriptado.WebMaster)
                {
                    Session["WebMaster"] = "true";
                    Session["Username"] = txtBoxLogin.Text.ToUpper().Trim();
                    auth.InsertarBitacora(txtBoxLogin.Text.ToUpper().Trim(), "BAJO", "Ingresó al sistema");
                    Response.Redirect("Logued.aspx");
                }

                if (!usuarioEncriptado.WebMaster)
                {
                    Session["WebMaster"] = "false";
                    Session["Username"] = txtBoxLogin.Text.ToUpper().Trim();
                    auth.InsertarBitacora(txtBoxLogin.Text.ToUpper().Trim(), "BAJO", "Ingresó al sistema");
                    Response.Redirect("Logued.aspx");
                }
            }

            if(!usuarioEncriptado.TieneAccesso)
                lblUserNotExist.Visible = true; 

        }

        protected void Unnamed5_Click(object sender, EventArgs e)
        {

        }
    }
}