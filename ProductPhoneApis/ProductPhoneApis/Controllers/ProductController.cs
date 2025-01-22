using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductPhoneApis.Models;
using System.Collections;
using System.Data.SqlClient;

namespace ProductPhoneApis.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Product")]
    public class ProductController : Controller
    {
        private readonly IConfiguration _configuration;
        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Rol TraerPermisos(int ID)
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

        [HttpPost]
        [Route("TraerProducto")]
        public dynamic TraerUserios(TraerProductoForm form)
        {
            bool succeess = false;
            string message = "Error";
            List<Producto> productos = new List<Producto>() { };
            SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionString"]);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("Traer_Productos", sqlConnection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID_Producto", form.ID_Producto);
            cmd.Parameters.AddWithValue("@Nombre", form.Nombre);
            cmd.Parameters.AddWithValue("@ID_Color", form.ID_Color);
            cmd.Parameters.AddWithValue("@Color", form.Color);
            cmd.Parameters.AddWithValue("@Precio1", form.Precio1);
            cmd.Parameters.AddWithValue("@Precio2", form.Precio2);
            cmd.Parameters.AddWithValue("@Fecha_Ultima_Mod_1", form.Fecha_Ultima_Mod_1);
            cmd.Parameters.AddWithValue("@Fecha_Ultima_Mod_2", form.Fecha_Ultima_Mod_2);
            cmd.Parameters.AddWithValue("@Cantidad1", form.Cantidad1);
            cmd.Parameters.AddWithValue("@Cantidad2", form.Cantidad2);


            SqlDataReader sqlDataReader = cmd.ExecuteReader();

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
                        Precio = double.Parse( sqlDataReader["Precio"].ToString()),
                        Fecha_Ultima_Mod = DateTime.Parse(sqlDataReader["Fecha_Ultima_Mod"].ToString()),
                        Cantidad = int.Parse(sqlDataReader["Cantidad"].ToString()),
                        //image =  sqlDataReader["Imagen"].ToString()
                    };

                    productos.Add(producto);
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
                result = productos

            };

        }

        [HttpPost]
        [Route("InsertProducto")]
        public dynamic InsertarProducto(string Nombre, int rol)
        {
            bool succeess = false;
            string message = "Error";
            Rol permision = TraerPermisos(rol);
            if (permision.Crear==true)
            {
                SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionString"]);
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("Insertar_Producto", sqlConnection);
                SqlTransaction cmdTransaction = null;
                try
                {
                    cmdTransaction = sqlConnection.BeginTransaction();
                    cmd.Transaction = cmdTransaction;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nombre", Nombre);
                    cmd.Parameters.AddWithValue("@Fecha_Ultima_Mod", DateTime.Now);

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
        [Route("UpdateProducto")]
        public dynamic UpdateProducto(int ID, string Nombre, int rol)
        {
            bool succeess = false;
            string message = "Error";
            Rol permision = TraerPermisos(rol);
            if (permision.Modificar == true)
            {
                SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionString"]);
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("Update_Producto", sqlConnection);
                SqlTransaction cmdTransaction = null;
                try
                {
                    cmdTransaction = sqlConnection.BeginTransaction();
                    cmd.Transaction = cmdTransaction;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_Producto", ID);
                    cmd.Parameters.AddWithValue("@Nombre", Nombre);
                    cmd.Parameters.AddWithValue("@Fecha_Ultima_Mod", DateTime.Now);

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

        [Authorize(Roles ="1")]
        [HttpPost]
        [Route("DeleteProducto")]
        public dynamic DeleteProducto(int ID_Producto, int ID_Color, int rol)
        {
            bool succeess = false;
            string message = "Error";
            Rol permision = TraerPermisos(rol);
            if (permision.Eliminar == true)
            {
                SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionString"]);
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("Delete_Producto", sqlConnection);
                SqlTransaction cmdTransaction = null;
                try
                {
                    cmdTransaction = sqlConnection.BeginTransaction();
                    cmd.Transaction = cmdTransaction;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_Producto", ID_Producto);
                    cmd.Parameters.AddWithValue("@ID_Color", ID_Color);
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
        [Route("InsertProductoColor")]
        public dynamic InsertarProductoColor(Producto Form, int rol)
        {
            bool succeess = false;
            string message = "Error";
            Rol permision = TraerPermisos(rol);
            if (permision.Crear == true)
            {
                SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionString"]);
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("Insertar_Producto_Color", sqlConnection);
                SqlTransaction cmdTransaction = null;
                try
                {
                    cmdTransaction = sqlConnection.BeginTransaction();
                    cmd.Transaction = cmdTransaction;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_Producto", Form.ID_Producto);
                    cmd.Parameters.AddWithValue("@ID_Color", Form.ID_Color);
                    cmd.Parameters.AddWithValue("@Precio", Form.Precio);
                    cmd.Parameters.AddWithValue("@Fecha_Ultima_Mod", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Cantidad", Form.ID_Producto);
                    cmd.Parameters.AddWithValue("@Imagen", Form.image);

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
        [Route("UpdateProductoColor")]
        public dynamic UpdateProductoColor(Producto Form, int rol)
        {
            bool succeess = false;
            string message = "Error";
            Rol permision = TraerPermisos(rol);
            if (permision.Modificar == true)
            {
                SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionString"]);
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("Update_Producto_Color", sqlConnection);
                SqlTransaction cmdTransaction = null;
                try
                {
                    cmdTransaction = sqlConnection.BeginTransaction();
                    cmd.Transaction = cmdTransaction;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_Producto", Form.ID_Producto);
                    cmd.Parameters.AddWithValue("@ID_Color", Form.ID_Color);
                    cmd.Parameters.AddWithValue("@Precio", Form.Precio);
                    cmd.Parameters.AddWithValue("@Fecha_Ultima_Mod", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Cantidad", Form.ID_Producto);
                    cmd.Parameters.AddWithValue("@Imagen", Form.image);

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
