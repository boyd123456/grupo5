using Microsoft.Data.SqlClient;
using PlataExpress.Models;

namespace PlataExpress.Data
{
    public class RemesaRepository
    {
        private readonly string _connectionString;

        public RemesaRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DineroConnection");
        }

        public async Task<bool> EnviarRemesaAsync(Remesa remesa)
        {
            try
            {
                var sql = @"INSERT INTO Remesas
                            (IdUsuario, TipoOperacion, Nombre, Agencia, Monto, Comision, FechaEnvio, Estado)
                            VALUES
                            (@IdUsuario, @TipoOperacion, @Nombre, @Agencia, @Monto, @Comision, @FechaEnvio, @Estado)";

                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add("@IdUsuario", System.Data.SqlDbType.Int).Value = remesa.IdUsuario;
                    cmd.Parameters.Add("@TipoOperacion", System.Data.SqlDbType.VarChar).Value = remesa.TipoOperacion;
                    cmd.Parameters.Add("@Nombre", System.Data.SqlDbType.VarChar).Value = remesa.Nombre;
                    cmd.Parameters.Add("@Agencia", System.Data.SqlDbType.VarChar).Value = remesa.Agencia;
                    cmd.Parameters.Add("@Monto", System.Data.SqlDbType.Decimal).Value = remesa.Monto;
                    cmd.Parameters.Add("@Comision", System.Data.SqlDbType.Decimal).Value = remesa.Comision;
                    cmd.Parameters.Add("@FechaEnvio", System.Data.SqlDbType.DateTime).Value = remesa.FechaEnvio;
                    cmd.Parameters.Add("@Estado", System.Data.SqlDbType.VarChar).Value = remesa.Estado;

                    await conn.OpenAsync();
                    int filas = await cmd.ExecuteNonQueryAsync();

                    return filas > 0;
                }
            }
            catch (Exception ex)
            {
                // 🔥 PARA DEBUG (míralo en consola)
                Console.WriteLine("ERROR AL GUARDAR REMESA: " + ex.Message);
                return false;
            }
        }

        public async Task<List<Remesa>> ObtenerRemesasPorUsuario(int idUsuario)
        {
            var lista = new List<Remesa>();

            var sql = @"SELECT IdRemesa, IdUsuario, TipoOperacion, Nombre, Agencia, Monto, Comision, FechaEnvio, Estado
                        FROM Remesas
                        WHERE IdUsuario = @IdUsuario";

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@IdUsuario", System.Data.SqlDbType.Int).Value = idUsuario;

                await conn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new Remesa
                        {
                            IdRemesa = reader.GetInt32(0),
                            IdUsuario = reader.GetInt32(1),
                            TipoOperacion = reader.GetString(2),
                            Nombre = reader.GetString(3),
                            Agencia = reader.GetString(4),
                            Monto = reader.GetDecimal(5),
                            Comision = reader.GetDecimal(6),
                            FechaEnvio = reader.GetDateTime(7),
                            Estado = reader.GetString(8)
                        });
                    }
                }
            }

            return lista;
        }
        // 🔢 Total de envíos
        public async Task<int> ObtenerTotalEnvios(int idUsuario)
        {
            var sql = "SELECT COUNT(*) FROM Remesas WHERE IdUsuario = @IdUsuario";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

            await conn.OpenAsync();
            return (int)await cmd.ExecuteScalarAsync();
        }

        // 💰 Total enviado
        public async Task<decimal> ObtenerTotalMonto(int idUsuario)
        {
            var sql = "SELECT ISNULL(SUM(Monto),0) FROM Remesas WHERE IdUsuario = @IdUsuario";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

            await conn.OpenAsync();
            return (decimal)await cmd.ExecuteScalarAsync();
        }

        // ⏳ En proceso
        public async Task<int> ObtenerEnProceso(int idUsuario)
        {
            var sql = "SELECT COUNT(*) FROM Remesas WHERE IdUsuario = @IdUsuario AND Estado = 'Enviado'";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

            await conn.OpenAsync();
            return (int)await cmd.ExecuteScalarAsync();
        }
        // 📄 Últimos envíos
        public async Task<List<Remesa>> ObtenerUltimosEnvios(int idUsuario)
        {
            var lista = new List<Remesa>();

            var sql = @"SELECT TOP 5 Nombre, Agencia, Monto, Estado
                FROM Remesas
                WHERE IdUsuario = @IdUsuario
                ORDER BY FechaEnvio DESC";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);

            await conn.OpenAsync();
            var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new Remesa
                {
                    Nombre = reader["Nombre"].ToString(),
                    Agencia = reader["Agencia"].ToString(),
                    Monto = Convert.ToDecimal(reader["Monto"]),
                    Estado = reader["Estado"].ToString()
                });
            }

            return lista;
        }

    }
}

