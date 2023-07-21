using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data;

namespace Services
{
    public class Auth
    {
        public string connectionString = @"Data Source=DESKTOP-BC0EAIO\SQLEXPRESS; Initial Catalog=PetShopDB; Integrated Security=True";
        private string _TablaUsuario = "Usuarios";


        public bool? LoginUser(string username, string password)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                Usuario usuarioLogueado = new Usuario();

                conn.Open();

                string existUser = $"select WebMaster from {_TablaUsuario} where Usuario = '{username}' and Contraseña = '{password}'";
                SqlCommand cmd = new SqlCommand(existUser, conn);
                var responseQuery = cmd.ExecuteScalar();

                if (responseQuery != null)
                {
                    usuarioLogueado.WebMaster = (bool)responseQuery;
                    return usuarioLogueado.WebMaster;
                }

                return null;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public int CalcularDVH(DataTable dt, int id = 0)
        {
            int cantidadColumnas = dt.Columns.Count;
            int a = 0;
            string str0 = "";
            int sumaASCII = 0;
            while (a < cantidadColumnas - 1)
            {
                str0 = str0 + (dt.Rows[id][a]).ToString();
                a++;
            }

            for (int i = 0; i < Encoding.ASCII.GetBytes(str0.ToString()).Count(); i++)
            {
                sumaASCII += (Encoding.ASCII.GetBytes(str0.ToString())[i]) * i;
            }
            return sumaASCII;
        }


        //public List<BE.BEtabla> Consulta()
        //{
        //    SqlParameter[] parameters = new SqlParameter[] { };
        //    DataTable dt = sqlHelper.ExecuteReader("dvvConsulta", parameters);
        //    List<BE.BEtabla> tablas = new List<BE.BEtabla>();
        //    Mappers.Tabla mapper = new Mappers.Tabla();
        //    foreach (DataRow row in dt.Rows)
        //    {
        //        tablas.Add(mapper.Map(row));
        //    }

        //    return tablas;
        //}

        public bool ComprobarIntegridad()
        {
            string query;


            query = $"select * from {_TablaUsuario}";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            DataTable tabla = new DataTable();
            var dr = cmd.ExecuteReader();
            tabla.Load(dr);
            int i = 0;
            int filas = tabla.Rows.Count;


            while (i < filas)
            {
                int dvhCalculo = CalcularDVH(tabla, i);
                int dvhDb = (int)tabla.Rows[i]["dvh"];
                if (dvhCalculo != dvhDb)
                {
                    return false;
                }
                i++;
            }

            return true;

        }

        public void ArreglarDigitos()
        {


            string query;


            query = $"select * from {_TablaUsuario}";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            DataTable tabla = new DataTable();
            var dr = cmd.ExecuteReader();
            tabla.Load(dr);
            int i = 0;
            int filas = tabla.Rows.Count;
            while (i < filas)
            {
                int idDVH = (int)tabla.Rows[i][0];
                int dvhCalculo = CalcularDVH(tabla, i);
                int DVHdb = (int)tabla.Rows[i]["dvh"];

                if (dvhCalculo != DVHdb)
                {
                    CargarDVH(idDVH, dvhCalculo);
                }
                i++;
            }


        }

        public void CargarDVH(int id, int dvh)
        {
            string query = @"update " + _TablaUsuario + " set dvh = " + dvh + " where IDUsuario = " + id;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);

            var response = cmd.ExecuteReader();
            //return sqlHelper.ExecuteQuery(query);
        }

    }

    
}
 