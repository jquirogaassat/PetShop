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
            var user = auth.LoginUser(txtBoxLogin.Text.ToLower().Trim(), txtBoxPassword.Text.Trim());

            if (user == null)
                lblUserNotExist.Visible = true;

            if(user == true)
            {
                Session["WebMaster"] = "true";
                Response.Redirect("Logued.aspx");
            }

            if (user == false)
            {
                Session["WebMaster"] = "false";
                Response.Redirect("Logued.aspx");
            }

            
        }

        protected void Unnamed5_Click(object sender, EventArgs e)
        {

        }
    }
}