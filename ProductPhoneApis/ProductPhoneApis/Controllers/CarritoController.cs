using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductPhoneApis.Models;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProductPhoneApis.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Carrito")]
    public class CarritoController : Controller
    {
        private readonly IConfiguration _configuration;
        public CarritoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("TraerCarritos")]
        public dynamic TraerCarritos(string Username)
        {
            bool succeess = false;
            string message = "Error";
            List<Carrito> carritos = new List<Carrito>() { };
            SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionString"]);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("Traer_carritos", sqlConnection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", Username);
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            try
            {
                succeess = true;
                message = "OK";
                while (sqlDataReader.Read())
                {
                    Carrito carrito = new Carrito
                    {
                        ID_Carrito = int.Parse(sqlDataReader["ID_Carrito"].ToString()),
                        Estado = sqlDataReader["Estado"].ToString(),
                        Fecha_Creacion= (DateTime)sqlDataReader["Fecha_Creacion"],
                        Fecha_Ultima_Mod = (DateTime)sqlDataReader["Fecha_Ultima_Mod"]
                    };

                    carritos.Add(carrito);
                }
            }
            catch (Exception e)
            {
                message = message + " " + e.Message;
            }

            sqlConnection.Close();

            
            foreach (Carrito carrito in carritos)
            {
                sqlConnection.Open();
                cmd = new SqlCommand("Traer_ProductosCarrito", sqlConnection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_Carrito", carrito.ID_Carrito);
                sqlDataReader = cmd.ExecuteReader();
                try
                {
                    succeess = true;
                    message = "OK";
                    while (sqlDataReader.Read())
                    {
                        Producto producto = new Producto
                        {
                            ID_Producto = int.Parse(sqlDataReader["ID_Producto"].ToString()),
                            Nombre = sqlDataReader["Nombre"].ToString(),
                            ID_Color = int.Parse(sqlDataReader["ID_Color"].ToString()),
                            Color = sqlDataReader["Color"].ToString(),
                            Precio= double.Parse(sqlDataReader["Precio"].ToString()),
                            //image = sqlDataReader["imagen"].ToString()
                        };

                        carrito.Productos_Carrito.Add(producto);
                    }
                }
                catch (Exception e)
                {
                    message = message + " " + e.Message;
                }
            }

            return new
            {
                success = succeess,
                message = message,
                result = carritos

            };
        }

        [HttpPost]
        [Route("InsertarCarritos")]
        public dynamic InsertarCarritos(Carrito form)
        {
            bool succeess = false;
            string message = "Error";
            SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionString"]);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("Insert_Carrito", sqlConnection);
            SqlTransaction cmdTransaction = null;
            try
            {
                cmdTransaction = sqlConnection.BeginTransaction();
                cmd.Transaction = cmdTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_Usuario", form.ID_Usuario);
                cmd.Parameters.AddWithValue("@Fecha_Creacion", DateTime.Now);
                cmd.Parameters.AddWithValue("@Fecha_Ultima_Mod", DateTime.Now);
                cmd.Parameters.AddWithValue("@Id_Estado", 2);
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

        [HttpPost]
        [Route("UpdateCarritos")]
        public dynamic UpdateCarritos(Carrito form)
        {
            bool succeess = false;
            string message = "Error";
            SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionString"]);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("Update_Carrito", sqlConnection);
            SqlTransaction cmdTransaction = null;
            try
            {
                cmdTransaction = sqlConnection.BeginTransaction();
                cmd.Transaction = cmdTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_Carrito", form.ID_Carrito);
                cmd.Parameters.AddWithValue("@ID_Usuario", form.ID_Usuario);
                cmd.Parameters.AddWithValue("@Fecha_Ultima_Mod", DateTime.Now);
                cmd.Parameters.AddWithValue("@Id_Estado", form.ID_Estado_Carro);
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

        [Authorize(Roles = "1")]
        [HttpPost]
        [Route("DeleteCarritos")]
        public dynamic DeleteCarritos(int ID)
        {
            bool succeess = false;
            string message = "Error";
            SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionString"]);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("Delete_Carrito", sqlConnection);
            SqlTransaction cmdTransaction = null;
            try
            {
                cmdTransaction = sqlConnection.BeginTransaction();
                cmd.Transaction = cmdTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_Carrito", ID);
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

        [HttpPost]
        [Route("UpdateCarritosDetalle")]
        public dynamic UpdateCarritosDetalle(int ID_Carrito,int ID_Producto_Color,long Cantidad)
        {
            bool succeess = false;
            string message = "Error";
            SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionString"]);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("Update_CarritoDetalle", sqlConnection);
            SqlTransaction cmdTransaction = null;
            try
            {
                cmdTransaction = sqlConnection.BeginTransaction();
                cmd.Transaction = cmdTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_Carrito", ID_Carrito);
                cmd.Parameters.AddWithValue("@ID_Producto_Color", ID_Producto_Color);
                cmd.Parameters.AddWithValue("@Cantidad", Cantidad);
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

        [HttpPost]
        [Route("InsertCarritosDetalle")]
        public dynamic InsertCarritosDetalle(int ID_Carrito, int ID_Producto_Color, int ID_Color, long Cantidad, double Precio)
        {
            bool succeess = false;
            string message = "Error";
            SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionString"]);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("Insert_CarritoDetalle", sqlConnection);
            SqlTransaction cmdTransaction = null;
            try
            {
                cmdTransaction = sqlConnection.BeginTransaction();
                cmd.Transaction = cmdTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_Carrito", ID_Carrito);
                cmd.Parameters.AddWithValue("@ID_Producto_Color", ID_Producto_Color);
                cmd.Parameters.AddWithValue("@ID_Color", ID_Color);
                cmd.Parameters.AddWithValue("@Cantidad", Cantidad);
                cmd.Parameters.AddWithValue("@Precio", Precio);
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

        [HttpPost]
        [Route("DeleteCarritosDetalle")]
        public dynamic DeleteCarritosDetalle(int ID_Carrito, int ID_Producto_Color)
        {
            bool succeess = false;
            string message = "Error";
            SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionString"]);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("Delete_Carrito_Detalle", sqlConnection);
            SqlTransaction cmdTransaction = null;
            try
            {
                cmdTransaction = sqlConnection.BeginTransaction();
                cmd.Transaction = cmdTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID_Carrito", ID_Carrito);
                cmd.Parameters.AddWithValue("@ID_Producto_Color", ID_Producto_Color);
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
