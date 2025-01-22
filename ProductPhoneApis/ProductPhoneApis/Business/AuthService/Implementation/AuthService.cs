using ProductPhoneApis.Models;
using ProductPhoneApis.Business.AuthService.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Data.SqlClient;


namespace ProductPhoneApis.Business.AuthService.Implementation
{
    using BCrypt.Net;
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration configuration)
        {

            _configuration = configuration;
        }
        public List<Usuarios> TraerUser(string form)
        {

            List<Usuarios> usuarios = new List<Usuarios>() { };
            SqlConnection sqlConnection = new SqlConnection(_configuration["ConnectionString"]);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand("Login_User", sqlConnection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", form);


            SqlDataReader sqlDataReader = cmd.ExecuteReader();

            try
            {

                while (sqlDataReader.Read())
                {
                    Usuarios usuario = new Usuarios
                    {
                        ID_Usuarios = int.Parse(sqlDataReader["ID_Usuario"].ToString()),
                        Username = sqlDataReader["Username"].ToString(),
                        Nombre = sqlDataReader["Nombre"].ToString(),
                        Email = sqlDataReader["Email"].ToString(),
                        Contrase = sqlDataReader["Contrase"].ToString(),
                        Fecha_Ultima_Mod =DateTime.Parse( sqlDataReader["Fecha_Ultima_Mod"].ToString()),
                        ID_Rol = int.Parse(sqlDataReader["ID_Rol"].ToString()),
                        Rol = sqlDataReader["Rol"].ToString()
                    };

                    usuarios.Add(usuario);
                }
            }
            catch (Exception e)
            {

            }

            sqlConnection.Close();

            return usuarios;

        }

        public async Task<TokenReturn> Login(string Username, string password)
        {

            TokenReturn tokenReturn = null;
            Usuarios user = null;
            List<Usuarios> usuarios = TraerUser(Username);
            bool verified = false;

            foreach (Usuarios usuario in usuarios)
            {
                if (BCrypt.Verify(password, usuario.Contrase))
                {
                    verified = true;
                    user = usuario;
                    tokenReturn = new TokenReturn();
                    tokenReturn.rol = usuario.ID_Rol;
                }
            }

            if (!verified)
            {
                return tokenReturn;
            }


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:SecretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.ID_Rol.ToString())
                }),
                IssuedAt = DateTime.UtcNow,
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            tokenReturn.token = tokenHandler.WriteToken(token);

            return tokenReturn;
        }
    }
}
