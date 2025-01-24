
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductPhoneApis.Models;
using System.Data.SqlClient;

namespace ProductPhoneApis.Controllers
{
    [Authorize(Roles = "1")]
    [ApiController]
    [Route("Ususario")]
    public class UserController : Controller
    {
        
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("TraerUsuario")]
        public dynamic TraerUserios(TraerUsuarioForm form)
        {
            bool succeess = false;
            string message = "Error";
            List<Usuarios> usuarios = new List<Usuarios>() { };
            SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionString"]);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("Traer_Users", sqlConnection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID_Usuario", form.ID_Usuario);
            cmd.Parameters.AddWithValue("@Username", form.Username);
            cmd.Parameters.AddWithValue("@Nombre", form.Nombre);
            cmd.Parameters.AddWithValue("@Email", form.Email);
            cmd.Parameters.AddWithValue("@ID_Rol", form.ID_Rol);
            cmd.Parameters.AddWithValue("@Rol", form.Rol);


            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            try
            {
                succeess = true;
                message = "OK";
                while (sqlDataReader.Read())
                {
                    Usuarios usuario = new Usuarios
                    {
                        ID_Usuarios = int.Parse(sqlDataReader["ID_Usuario"].ToString()),
                        Username = sqlDataReader["Username"].ToString(),
                        Nombre = sqlDataReader["Nombre"].ToString(),
                        Email = sqlDataReader["Email"].ToString(),
                        Contrase = sqlDataReader["Contrase"].ToString(),
                        Fecha_Ultima_Mod = DateTime.Parse(sqlDataReader["Fecha_Ultima_Mod"].ToString()),
                        ID_Rol = int.Parse(sqlDataReader["ID_Rol"].ToString()),
                        Rol = sqlDataReader["Rol"].ToString()
                    };

                    usuarios.Add(usuario);
                }
            }
            catch (Exception e)
            {
                message = message + " " + e.Message;
            }

            sqlConnection.Close();

            return new
            {
                success = succeess,
                message = message,
                result = usuarios

            };

        }

        [HttpPost]
        [Route("InsertarUsuario")]
        public dynamic InsertarUsuario(Usuarios form)
        {
            bool succeess = false;
            string message = "Error";
            bool BuenNombre = true;
            bool BuenEmail = false;
            bool BuenaContra = false;
            int Mayusculas = 0;
            int Minusculas = 0;
            int numeros = 0;
            int caracteresEspeciales = 0;
            for (int i = 0; i < form.Username.Length; i++)
            {
                if ((form.Username[i] > 32 && form.Username[i]<48) || (form.Username[i] > 57 && form.Username[i] < 65) || (form.Username[i] > 90 && form.Username[i] < 97) || (form.Username[i] > 122 && form.Username[i] < 128))
                {
                    BuenNombre = false;
                }
            }
            for (int i = 0; i < form.Email.Length; i++)
            {
                if (form.Email[i] == '@')
                {
                    BuenEmail = true;
                }
            }
            for (int i = 0; i < form.Contrase.Length; i++)
            {
                if (form.Contrase[i] > 64 && form.Contrase[i] < 91)
                {
                    Mayusculas++;
                }
                else if (form.Contrase[i] > 96 && form.Contrase[i] < 123)
                {
                    Minusculas++;
                }
                else if (form.Contrase[i] > 47 && form.Contrase[i] < 58)
                {
                    numeros++;
                }
                else if (form.Contrase[i] > 32 && form.Contrase[i] < 128)
                {
                    caracteresEspeciales++;
                }
                
            }
            if (Mayusculas > 0 && Minusculas > 0 && numeros > 0 && caracteresEspeciales > 0 && form.Contrase.Length > 7)
            {
                BuenaContra = true;
            }
            if (BuenaContra == true && BuenEmail == true && BuenNombre == true)
            {
                SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionString"]);
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("Insertar_Usuario", sqlConnection);
                SqlTransaction cmdTransaction = null;
                try
                {
                    cmdTransaction = sqlConnection.BeginTransaction();
                    cmd.Transaction = cmdTransaction;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", form.Username);
                    cmd.Parameters.AddWithValue("@Nombre", form.Nombre);
                    cmd.Parameters.AddWithValue("@Email", form.Email);
                    cmd.Parameters.AddWithValue("@Contrase", BCrypt.Net.BCrypt.HashPassword(form.Contrase));
                    cmd.Parameters.AddWithValue("@Fecha_Ultima_Mod", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ID_Rol", form.ID_Rol);
                    cmd.ExecuteNonQuery();
                    cmdTransaction.Commit();
                    message = "OK";
                    succeess = true;
                }
                catch (Exception e)
                {
                    cmdTransaction.Rollback();
                    message = message + " " + e.Message;
                }

                sqlConnection.Close();
            }


            return new
            {
                success = succeess,
                message = message,
                result = ""
            };
        }

        [HttpPost]
        [Route("UpdateUsuario")]
        public dynamic UpdateUsuario(Usuarios form)
        {
            bool succeess = false;
            string message = "Error";
            bool BuenNombre = true;
            bool BuenEmail = false;
            bool BuenaContra = false;
            int Mayusculas = 0;
            int Minusculas = 0;
            int numeros = 0;
            int caracteresEspeciales = 0;
            for (int i = 0; i < form.Username.Length; i++)
            {
                if ((form.Username[i] > 32 && form.Username[i] < 48) || (form.Username[i] > 57 && form.Username[i] < 65) || (form.Username[i] > 90 && form.Username[i] < 97) || (form.Username[i] > 122 && form.Username[i] < 128))
                {
                    BuenNombre = false;
                }
            }
            for (int i = 0; i < form.Email.Length; i++)
            {
                if (form.Email[i] == '@')
                {
                    BuenEmail = true;
                }
            }
            for (int i = 0; i < form.Contrase.Length; i++)
            {
                if (form.Contrase[i] > 64 && form.Contrase[i] < 91)
                {
                    Mayusculas++;
                }
                else if (form.Contrase[i] > 96 && form.Contrase[i] < 123)
                {
                    Minusculas++;
                }
                else if (form.Contrase[i] > 47 && form.Contrase[i] < 58)
                {
                    numeros++;
                }
                else if (form.Contrase[i] > 32 && form.Contrase[i] < 128)
                {
                    caracteresEspeciales++;
                }

            }
            if (Mayusculas > 0 && Minusculas > 0 && numeros > 0 && caracteresEspeciales > 0 && form.Contrase.Length > 7)
            {
                BuenaContra = true;
            }
            if (BuenaContra == true && BuenEmail == true && BuenNombre == true)
            {
                SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionString"]);
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("Update_Usuario", sqlConnection);
                SqlTransaction cmdTransaction = null;
                try
                {
                    cmdTransaction = sqlConnection.BeginTransaction();
                    cmd.Transaction = cmdTransaction;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_Usuario", form.ID_Usuarios);
                    cmd.Parameters.AddWithValue("@Username", form.Username);
                    cmd.Parameters.AddWithValue("@Nombre", form.Nombre);
                    cmd.Parameters.AddWithValue("@Email", form.Email);
                    cmd.Parameters.AddWithValue("@Constrase", BCrypt.Net.BCrypt.HashPassword(form.Contrase));
                    cmd.Parameters.AddWithValue("@Fecha_Ultima_Mod", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Rol", form.Rol);
                    cmd.ExecuteNonQuery();
                    cmdTransaction.Commit();
                    message = "OK";
                    succeess = true;
                }
                catch (Exception e)
                {
                    cmdTransaction.Rollback();
                    message = message + " " + e.Message;
                }

                sqlConnection.Close();
            }


            return new
            {
                success = succeess,
                message = message,
                result = ""
            };
        }

        [HttpPost]
        [Route("DeleteUser")]
        public dynamic deleteProductos(int ID)
        {
            bool succeess = false;
            string message = "Error";
            SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionString"]);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("Delete_User", sqlConnection);
            SqlTransaction cmdTransaction = null;
            try
            {
                cmdTransaction = sqlConnection.BeginTransaction();
                cmd.Transaction = cmdTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_Usuario", ID);
                cmd.ExecuteNonQuery();
                cmdTransaction.Commit();
                message = "OK";
                succeess = true;
            }
            catch (Exception e)
            {
                cmdTransaction.Rollback();
                message = message + " " + e.Message;
            }

            sqlConnection.Close();

            return new
            {
                success = succeess,
                message = message,
                result = ""
            };

        }
    }
}
