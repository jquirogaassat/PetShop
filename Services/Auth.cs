using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using Models;
using System.Data;

namespace Services
{
    public class Auth
    {
        private static string connectionString = @"Data Source=DESKTOP-BC0EAIO\SQLEXPRESS; Initial Catalog=PetShopDB; Integrated Security=True";
        private string _tableUsuarios = "Usuarios";
        private static string _databaseName = "PetShopDB";
        private string pathBackUp = ConfigurationSettings.AppSettings["pathBKP"].ToString();


        public bool? LoginUser(string username, string password)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                Usuario usuarioLogueado = new Usuario();

                conn.Open();

                string existUser = $"select WebMaster from {_tableUsuarios} where UPPER(Usuario) = UPPER('{username}') and Contraseña = '{password}'";
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

        public bool ComprobarIntegridad()
        {
            string query;


            query = $"select * from {_tableUsuarios}";
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

            query = $"select * from {_tableUsuarios}";
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
                    CargarDVH(idDVH, dvhCalculo);

                i++;
            }


        }

        public int RealizarBackup()
        {

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            try
            {
                string query = $"BACKUP DATABASE {_databaseName} TO DISK = '{pathBackUp}{_databaseName}.bak'";
                SqlCommand cmd = new SqlCommand(query, conn);

                var result = cmd.ExecuteNonQuery();
                return result;

            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public int RealizarRestore()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            try
            {
                string query = $"USE master; ALTER DATABASE {_databaseName} SET SINGLE_USER WITH ROLLBACK IMMEDIATE; RESTORE DATABASE {_databaseName} FROM  DISK = '{pathBackUp}{_databaseName}.bak' WITH REPLACE";
                SqlCommand cmd = new SqlCommand(query, conn);

                var result = cmd.ExecuteNonQuery();
                return result;

            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public void CargarDVH(int id, int dvh)
        {
            string query = @"update " + _tableUsuarios + " set dvh = " + dvh + " where IDUsuario = " + id;
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
        }

        public List<Bitacora> Listar()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            try
            {
                var query = "select * from bitacora b order by b.bitacora_fecha desc";

                DataSet mDs = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                List<Bitacora> mRegistros = new List<Bitacora>();

                var cmd = new SqlCommand(query, conn);
                DataTable table = new DataTable();
                table.Load(cmd.ExecuteReader());
                mDs.Tables.Add(table);



                if (mDs.Tables.Count > 0 && mDs.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow mDr in mDs.Tables[0].Rows)
                    {   
                        Bitacora mBitacora = new Bitacora(int.Parse(mDr["bitacora_id"].ToString()), mDr["cuenta_usuario"].ToString(),mDr["bitacora_criticidad"].ToString(),mDr["descripcion"].ToString(),DateTime.Parse(mDr["bitacora_fecha"].ToString()));
    
                        mRegistros.Add(mBitacora);
                    }
                }
                return mRegistros;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

        }

        public int InsertarBitacora(string cuenta, string criticidad, string descripcion) 
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            try
            {
                var query = $"INSERT INTO BITACORA VALUES ('{cuenta}', '{criticidad}', GETDATE(), '{descripcion}')";

                SqlCommand cmd = new SqlCommand(query, conn);
                var result = cmd.ExecuteNonQuery();
                return result;
            }
            catch (Exception)
            {
                return 0;
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public Usuario ObtenerUsuarioEncriptado(string usuario, string contraseña)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            Usuario usuarioLogueado = new Usuario();
            usuarioLogueado.TieneAccesso = false;
            try
            {
                DataSet mDs = new DataSet();


                var query = $"SELECT * FROM {_tableUsuarios} WHERE UPPER(Usuario) = UPPER('{usuario}')";
                SqlCommand cmd = new SqlCommand(query, conn);
                DataTable table = new DataTable();
                table.Load(cmd.ExecuteReader());
                mDs.Tables.Add(table);

                if (mDs.Tables.Count > 0 && mDs.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow mDr in mDs.Tables[0].Rows)
                    {
                        usuarioLogueado = new Usuario(mDr["Contraseña"].ToString(), bool.Parse(mDr["WebMaster"].ToString()));

                        
                    }
                }


                if (Seguridad.Encrypt(contraseña) == usuarioLogueado.Contraseña)
                    usuarioLogueado.TieneAccesso = true;
                    

                return usuarioLogueado;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }


}
