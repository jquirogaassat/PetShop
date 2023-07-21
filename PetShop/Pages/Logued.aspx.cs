using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PetShop.Pages
{
    public partial class Logued : System.Web.UI.Page
    {
        Auth auth = new Auth();
        string userLogued = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isWebMaster = Boolean.Parse(Session["WebMaster"]?.ToString());
            userLogued = Session["Username"]?.ToString();

            if (isWebMaster)
            {
                lblWebMaster.Text = "WebMaster";

                bool integrity = auth.ComprobarIntegridad();

                if (integrity) lblDBBroken.Text = "La base de datos ha sido cargada exitosamente";

                if (!integrity)
                    lblDBBroken.Text = "La base de datos ha sido comprometida";

                updateDataGrid();
            }


            if (!isWebMaster)
            {
                lblDBBroken.Visible = false;
                btnBackup.Visible = false;
                btnRestore.Visible = false;
                btnCorrectDB.Visible = false;

                lblWebMaster.Text = "Común";


            }




        }





        protected void btnBackUp(object sender, EventArgs e)
        {
            string backUpMessage = "<script>alert('Backup generado correctamente') </script>";

            int responseBackUp = auth.RealizarBackup();

            if (responseBackUp >= 0)
            {
                auth.InsertarBitacora(userLogued, "ALTA", "No se realizó el backup");
                backUpMessage = "<script>alert('El BackUp no se pudo realizar por favor intentelo más tarde ') </script>";
            }
            else
            {

                auth.InsertarBitacora(userLogued, "ALTA", "Se realizó el backup");
                Response.Write(backUpMessage);
            }
            updateDataGrid();

        }

        protected void btnRestoreDB(object sender, EventArgs e)
        {
            string backUpMessage = "<script>alert('Restore generado correctamente') </script>";

            int responseBackUp = auth.RealizarRestore();

            if (responseBackUp >= 0)
            {
                auth.InsertarBitacora(userLogued, "ALTA", "No se realizó el restore");
                backUpMessage = "<script>alert('El Restore no se pudo realizar por favor intentelo más tarde ') </script>";
            }
            else
            {

                auth.InsertarBitacora(userLogued, "ALTA", "Se realizó el restore");
                updateDataGrid();
                Response.Write(backUpMessage);
            }

        }



        protected void btnCorrectAction(object sender, EventArgs e)
        {
            auth.ArreglarDigitos();
            lblDBBroken.Text = "La base de datos ha sido arreglada exitosamente";

            auth.InsertarBitacora(userLogued, "ALTA", "Se recalcularon los digitos verificadores de la BD");

            updateDataGrid();

        }

        private void updateDataGrid()
        {
            var mBitacora = auth.Listar();
            GridView1.DataSource = mBitacora;
            GridView1.DataBind();
        }
    }
}


