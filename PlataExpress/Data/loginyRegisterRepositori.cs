using System.Threading.Tasks;
using PlataExpress.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace PlataExpress.Data
{
    public class loginyRegisterRepositori
    {
        private readonly string _connectionString;

        public loginyRegisterRepositori(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DineroConnection");
        }

        public async Task<bool> ExisteUsuarioOCorreoAsync(string usuario, string correo)
        {
            var sql = @"SELECT COUNT(*) 
                        FROM Usuarios
                        WHERE Usuario = @Usuario OR Correo = @Correo";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Usuario", usuario);
                cmd.Parameters.AddWithValue("@Correo", correo);

                await conn.OpenAsync();
                int cantidad = (int)await cmd.ExecuteScalarAsync();

                return cantidad > 0;
            }
        }

        public async Task RegistrarUsuarioAsync(RegisterUsuarios model)
        {
            var sql = @"INSERT INTO Usuarios
                        (Nombres, Apellidos, Correo, Usuario, Clave, Telefono, Dni, Rol, FechaRegistro)
                        VALUES
                        (@Nombres, @Apellidos, @Correo, @Usuario, @Clave, @Telefono, @Dni, @Rol, @FechaRegistro)";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Nombres", model.Nombres);
                cmd.Parameters.AddWithValue("@Apellidos", model.Apellidos);
                cmd.Parameters.AddWithValue("@Correo", model.Correo);
                cmd.Parameters.AddWithValue("@Usuario", model.Usuario);
                cmd.Parameters.AddWithValue("@Clave", model.Clave);
                cmd.Parameters.AddWithValue("@Telefono", model.Telefono);
                cmd.Parameters.AddWithValue("@Dni", model.DNI);
                cmd.Parameters.AddWithValue("@Rol", model.Rol);
                cmd.Parameters.AddWithValue("@FechaRegistro", model.FechaRegistro);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<RegisterUsuarios?> ValidarLoginAsync(string usuarioOcorreo, string clave)
        {
            var sql = @"SELECT TOP 1 IdUsuario, Nombres, Apellidos, Correo, Usuario, Clave, Telefono, Dni, Rol, FechaRegistro
                        FROM Usuarios
                        WHERE (Usuario = @Dato OR Correo = @Dato) AND Clave = @Clave";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Dato", usuarioOcorreo);
                cmd.Parameters.AddWithValue("@Clave", clave);

                await conn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new RegisterUsuarios
                        {
                            IdUsuario = reader.GetInt32(0),
                            Nombres = reader.GetString(1),
                            Apellidos = reader.GetString(2),
                            Correo = reader.GetString(3),
                            Usuario = reader.GetString(4),
                            Clave = reader.GetString(5),
                            Telefono = reader.GetString(6),
                            DNI = reader.GetString(7),
                            Rol = reader.GetString(8),
                            FechaRegistro = reader.GetDateTime(9)
                        };
                    }
                }
            }

            return null;
        }
    }
}