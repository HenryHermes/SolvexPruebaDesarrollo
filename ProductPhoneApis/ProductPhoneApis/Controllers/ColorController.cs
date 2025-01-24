using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductPhoneApis.Models;
using System.Data.SqlClient;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;

namespace ProductPhoneApis.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Color")]
    public class ColorController : Controller
    {
        private readonly IConfiguration _configuration;
        public ColorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

    

        private Rol TraerPermisos(int ID)
        {
            SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionString"]);
            sqlConnection.Open();
            Rol rol = new Rol();
            SqlCommand cmd = new SqlCommand("TraerRol", sqlConnection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID_Rol", ID);
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            try
            {
                
                
                Rol Permisos = new Rol
                {
                    Ver = bool.Parse(sqlDataReader["Ver"].ToString()),
                    Modificar = bool.Parse(sqlDataReader["Modificar"].ToString()),
                    Crear = bool.Parse(sqlDataReader["Crear"].ToString()),
                    Eliminar = bool.Parse(sqlDataReader["Eliminar"].ToString())
                };

                rol = Permisos;
                 
            }
            catch (Exception e)
            {
                
            }
            sqlConnection.Close();
            return rol;

        }

        [HttpGet]
        [Route("TraerColor")]
        public dynamic TraerColores()
        {
            bool succeess = false;
            string message = "Error";
            List<Colors> Colores = new List<Colors>() { };
            SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionString"]);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("Traer_Color", sqlConnection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            try
            {
                succeess = true;
                message = "OK";
                while (sqlDataReader.Read())
                {
                    Colors color = new Colors
                    {
                        ID_Color = int.Parse(sqlDataReader["ID_Color"].ToString()),
                        Nombre = sqlDataReader["Nombre"].ToString()
                    };

                    Colores.Add(color);
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
                result = Colores

            };
        }

        
        [HttpPost]
        [Route("InsertarColor")]
        public dynamic InsertarColor(string Nombre)
        {
            string token = Request.Headers["Authorization"];

            if (token.StartsWith("Bearer"))
            {
                token = token.Substring("Bearer ".Length).Trim();
            }
            var handler = new JwtSecurityTokenHandler();

            JwtSecurityToken jwt = handler.ReadJwtToken(token);
            int role = int.Parse(jwt.Claims.ElementAt(1).Value);

            bool succeess = false;
            string message = "Error";
            Rol permisos = TraerPermisos(role);
            if (permisos.Crear==true)
            {
                SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionString"]);
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("Insertar_Color", sqlConnection);
                SqlTransaction cmdTransaction = null;
                try
                {
                    cmdTransaction = sqlConnection.BeginTransaction();
                    cmd.Transaction = cmdTransaction;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", Nombre);
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
        [Route("UpdateColor")]
        public dynamic UpdateColor(Colors color)
        {
            string token = Request.Headers["Authorization"];

            if (token.StartsWith("Bearer"))
            {
                token = token.Substring("Bearer ".Length).Trim();
            }
            var handler = new JwtSecurityTokenHandler();

            JwtSecurityToken jwt = handler.ReadJwtToken(token);
            int role = int.Parse(jwt.Claims.ElementAt(1).Value);

            bool succeess = false;
            string message = "Error";
            Rol permisos = TraerPermisos(role);
            if (permisos.Modificar == true)
            {
                SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionString"]);
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("Update_Color", sqlConnection);
                SqlTransaction cmdTransaction = null;
                try
                {
                    cmdTransaction = sqlConnection.BeginTransaction();
                    cmd.Transaction = cmdTransaction;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_Color", color.ID_Color);
                    cmd.Parameters.AddWithValue("@Nombre", color.Nombre);
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

        

    }
}
